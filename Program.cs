using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Chronometre.Services;
using Chronometre.Forms;

namespace Chronometre
{
    internal static class Program
    {
        private static readonly string MutexName = "Chronometre_SingleInstance_Mutex";
        private static Mutex? _mutex;
        private static NotifyIcon? _trayIcon;
        private static TimerService? _timerService;
        private static ReliableHotkeyManager? _hotkeyManager;
        private static LogWriter? _logWriter;
        private static Settings? _settings;
        private static ApplicationContext? _applicationContext;
        private static OverlayIndicatorForm? _overlay;
        private static OverlayAutoHideController? _overlayController;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Single instance check
            _mutex = new Mutex(true, MutexName, out bool isNewInstance);
            if (!isNewInstance)
            {
                MessageBox.Show("Chronomètre is already running.", "Chronomètre", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                InitializeApplication();
                _applicationContext = new ApplicationContext();
                Application.Run(_applicationContext);
            }
            finally
            {
                Cleanup();
            }
        }

        private static void InitializeApplication()
        {
            // Load settings
            _settings = Settings.Load();

            // Initialize services
            _timerService = new TimerService();
            _logWriter = new LogWriter();
            _hotkeyManager = new ReliableHotkeyManager();

            // Initialize overlay
            _overlay = new OverlayIndicatorForm();
            _overlayController = new OverlayAutoHideController(_overlay);
            _overlayController.AutoHideMinutes = _settings.OverlayAutoHideMinutes;
            _overlayController.ShowElapsed = _settings.ShowElapsedInIndicator;

            // Setup tray icon
            SetupTrayIcon();

            // Register hotkeys
            RegisterHotkeys();

            // Subscribe to timer events
            _timerService.StateChanged += OnTimerStateChanged;
            _timerService.ElapsedUpdated += OnTimerElapsedUpdated;

            // Show initial state
            UpdateTrayTooltip();
        }

        private static void SetupTrayIcon()
        {
            try
            {
                // Use system icon for now to avoid embedded resource issues
                Icon? customIcon = null;
                try
                {
                    // Try to load from embedded resources
                    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    var resourceName = "Chronometre.timer-icon.ico";
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream != null)
                        {
                            customIcon = new Icon(stream);
                            System.Diagnostics.Debug.WriteLine("Loaded icon from embedded resources");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load embedded icon: {ex.Message}");
                }

                // Fallback to file system
                if (customIcon == null)
                {
                    try
                    {
                        var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "timer-icon.ico");
                        if (File.Exists(iconPath))
                        {
                            customIcon = new Icon(iconPath);
                            System.Diagnostics.Debug.WriteLine("Loaded icon from file system");
                        }
                        else
                        {
                            // Try in the project root directory
                            var projectIconPath = Path.Combine(Directory.GetCurrentDirectory(), "timer-icon.ico");
                            if (File.Exists(projectIconPath))
                            {
                                customIcon = new Icon(projectIconPath);
                                System.Diagnostics.Debug.WriteLine("Loaded icon from project directory");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to load custom icon from file: {ex.Message}");
                    }
                }

                _trayIcon = new NotifyIcon
                {
                    Icon = customIcon ?? System.Drawing.SystemIcons.Application,
                    Text = "Chronomètre - Idle",
                    Visible = true,
                    ContextMenuStrip = CreateContextMenu()
                };

                _trayIcon.DoubleClick += OnTrayIconDoubleClick;
                
                // Force the icon to be visible
                _trayIcon.Visible = true;
                
                // Show a notification that the app is running
                _trayIcon.ShowBalloonTip(3000, "Chronomètre", "Time tracker is running in the system tray", ToolTipIcon.Info);
                
                // Additional attempt to ensure visibility
                System.Threading.Thread.Sleep(100);
                _trayIcon.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create tray icon: {ex.Message}", "Chronomètre Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static ContextMenuStrip CreateContextMenu()
        {
            var menu = new ContextMenuStrip();
            
            var startItem = new ToolStripMenuItem("Start", null, OnStartClicked)
            {
                ShortcutKeys = Keys.Control | Keys.Alt | Keys.F1
            };
            menu.Items.Add(startItem);

            var pauseItem = new ToolStripMenuItem("Pause/Resume", null, OnPauseClicked)
            {
                ShortcutKeys = Keys.Control | Keys.Alt | Keys.F2
            };
            menu.Items.Add(pauseItem);

            var stopItem = new ToolStripMenuItem("Stop & Save", null, OnStopClicked)
            {
                ShortcutKeys = Keys.Control | Keys.Alt | Keys.F3
            };
            menu.Items.Add(stopItem);

            var addNoteItem = new ToolStripMenuItem("Add Note...", null, OnAddNoteClicked)
            {
                ShortcutKeys = Keys.Control | Keys.Alt | Keys.F4,
                Enabled = false
            };
            menu.Items.Add(addNoteItem);

            menu.Items.Add(new ToolStripSeparator());

            // Overlay submenu
            var overlayMenu = new ToolStripMenuItem("Overlay");
            
            var showElapsedItem = new ToolStripMenuItem("Show Elapsed Time in Indicator", null, OnToggleShowElapsed)
            {
                Checked = _settings?.ShowElapsedInIndicator ?? true
            };
            overlayMenu.DropDownItems.Add(showElapsedItem);
            
            overlayMenu.DropDownItems.Add(new ToolStripSeparator());
            
            var autoHideMenu = new ToolStripMenuItem("Auto-Hide After");
            var autoHide5 = new ToolStripMenuItem("5 minutes", null, (s, e) => SetAutoHideMinutes(5)) { Checked = _settings?.OverlayAutoHideMinutes == 5 };
            var autoHide10 = new ToolStripMenuItem("10 minutes", null, (s, e) => SetAutoHideMinutes(10)) { Checked = _settings?.OverlayAutoHideMinutes == 10 };
            var autoHide30 = new ToolStripMenuItem("30 minutes", null, (s, e) => SetAutoHideMinutes(30)) { Checked = _settings?.OverlayAutoHideMinutes == 30 };
            var autoHideNever = new ToolStripMenuItem("Never", null, (s, e) => SetAutoHideMinutes(0)) { Checked = _settings?.OverlayAutoHideMinutes == 0 };
            
            autoHideMenu.DropDownItems.Add(autoHide5);
            autoHideMenu.DropDownItems.Add(autoHide10);
            autoHideMenu.DropDownItems.Add(autoHide30);
            autoHideMenu.DropDownItems.Add(autoHideNever);
            overlayMenu.DropDownItems.Add(autoHideMenu);
            
            overlayMenu.DropDownItems.Add(new ToolStripSeparator());
            
            var peekItem = new ToolStripMenuItem("Peek Now", null, OnPeekClicked)
            {
                ShortcutKeys = Keys.Control | Keys.Alt | Keys.F5
            };
            overlayMenu.DropDownItems.Add(peekItem);
            
            menu.Items.Add(overlayMenu);

            menu.Items.Add(new ToolStripSeparator());

            var openLogItem = new ToolStripMenuItem("Open Log File", null, OnOpenLogFolderClicked);
            menu.Items.Add(openLogItem);

            menu.Items.Add(new ToolStripSeparator());

            var exitItem = new ToolStripMenuItem("Exit", null, OnExitClicked);
            menu.Items.Add(exitItem);

            return menu;
        }

        private static void RegisterHotkeys()
        {
            if (_hotkeyManager == null) return;

            // Register hotkeys using the new keyboard hook system
            _hotkeyManager.RegisterHotkey(Keys.Control | Keys.Alt | Keys.F1, OnStartHotkey);
            _hotkeyManager.RegisterHotkey(Keys.Control | Keys.Alt | Keys.F2, OnPauseHotkey);
            _hotkeyManager.RegisterHotkey(Keys.Control | Keys.Alt | Keys.F3, OnStopHotkey);
            _hotkeyManager.RegisterHotkey(Keys.Control | Keys.Alt | Keys.F4, OnAddNoteHotkey);
            _hotkeyManager.RegisterHotkey(Keys.Control | Keys.Alt | Keys.F5, OnPeekHotkey);

            _trayIcon?.ShowBalloonTip(3000, "Hotkeys Registered", "Hotkeys registered! Try Ctrl+Alt+F1 to start.", ToolTipIcon.Info);
            
            System.Diagnostics.Debug.WriteLine("Hotkey registration completed using keyboard hook");
        }

        private static void OnTimerStateChanged(object? sender, TimerStateChangedEventArgs e)
        {
            UpdateTrayTooltip();
            UpdateContextMenu();
            
            // Update overlay
            if (_overlayController != null && _timerService != null)
            {
                _overlayController.OnStateChanged(e.NewState, _timerService.ElapsedTime);
            }
        }

        private static void UpdateTrayTooltip()
        {
            if (_trayIcon == null || _timerService == null) return;

            var state = _timerService.CurrentState;
            var text = state switch
            {
                TimerState.Idle => "Chronomètre - Idle",
                TimerState.Running => $"Chronomètre - Running ({_timerService.ElapsedTime:hh\\:mm\\:ss})",
                TimerState.Paused => $"Chronomètre - Paused ({_timerService.ElapsedTime:hh\\:mm\\:ss})",
                _ => "Chronomètre - Unknown"
            };

            _trayIcon.Text = text;
        }

        private static void UpdateContextMenu()
        {
            if (_trayIcon?.ContextMenuStrip == null || _timerService == null) return;

            var menu = _trayIcon.ContextMenuStrip;
            var state = _timerService.CurrentState;

            // Update Start item
            var startItem = menu.Items[0] as ToolStripMenuItem;
            startItem!.Enabled = state == TimerState.Idle;

            // Update Pause/Resume item
            var pauseItem = menu.Items[1] as ToolStripMenuItem;
            pauseItem!.Enabled = state == TimerState.Running || state == TimerState.Paused;
            pauseItem.Text = state == TimerState.Running ? "Pause" : "Resume";

            // Update Stop item
            var stopItem = menu.Items[2] as ToolStripMenuItem;
            stopItem!.Enabled = state == TimerState.Running || state == TimerState.Paused;

            // Update Add Note item
            var addNoteItem = menu.Items[3] as ToolStripMenuItem;
            addNoteItem!.Enabled = state == TimerState.Running || state == TimerState.Paused;
        }

        private static void OnTrayIconDoubleClick(object? sender, EventArgs e)
        {
            // Double-click to start if idle, or show current status
            if (_timerService?.CurrentState == TimerState.Idle)
            {
                OnStartClicked(sender, e);
            }
        }

        private static void OnStartClicked(object? sender, EventArgs e)
        {
            if (_timerService?.CurrentState != TimerState.Idle) return;

            using var dialog = new StartDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _timerService.Start(dialog.Notes);
            }
        }

        private static void OnPauseClicked(object? sender, EventArgs e)
        {
            if (_timerService == null) return;

            if (_timerService.CurrentState == TimerState.Running)
            {
                _timerService.Pause();
            }
            else if (_timerService.CurrentState == TimerState.Paused)
            {
                _timerService.Resume();
            }
        }

        private static void OnStopClicked(object? sender, EventArgs e)
        {
            if (_timerService?.CurrentState == TimerState.Idle) return;

            _timerService?.Stop();
            if (_logWriter != null && _timerService != null)
            {
                System.Diagnostics.Debug.WriteLine("Writing session to log");
                _logWriter.WriteSession(_timerService.GetSessionData());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"LogWriter or TimerService is null. LogWriter: {_logWriter != null}, TimerService: {_timerService != null}");
            }
        }

        private static void OnAddNoteClicked(object? sender, EventArgs e)
        {
            if (_timerService?.CurrentState != TimerState.Running && 
                _timerService?.CurrentState != TimerState.Paused) return;

            using var dialog = new NoteDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _timerService?.AddNote(dialog.Note);
            }
        }

        private static void OnOpenLogFolderClicked(object? sender, EventArgs e)
        {
            if (_logWriter == null) return;

            try
            {
                var logFilePath = _logWriter.LogFilePath;
                if (System.IO.File.Exists(logFilePath))
                {
                    // Open the log file directly with the default text editor
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = logFilePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    // If file doesn't exist, open the folder containing it
                    var logFolder = System.IO.Path.GetDirectoryName(logFilePath);
                    if (logFolder != null && System.IO.Directory.Exists(logFolder))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", logFolder);
                    }
                    else
                    {
                        // Show the log file path to the user
                        MessageBox.Show($"Log file location:\n{logFilePath}\n\nIf the folder doesn't exist, it will be created when you start your first session.", 
                            "Chronomètre Log Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open log file: {ex.Message}\n\nLog file location: {_logWriter.LogFilePath}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OnExitClicked(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void ShowLogLocationNotification()
        {
            try
            {
                if (_logWriter?.LogFilePath != null)
                {
                    var logLocation = Path.GetDirectoryName(_logWriter.LogFilePath);
                    var logFileName = Path.GetFileName(_logWriter.LogFilePath);
                    
                    _trayIcon?.ShowBalloonTip(
                        3000, // 3 seconds
                        "Chronomètre",
                        $"Log file saved to: {logLocation}\\{logFileName}",
                        ToolTipIcon.Info
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing log location notification: {ex.Message}");
            }
        }

        // Hotkey event handlers
        private static void OnStartHotkey()
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            System.Diagnostics.Debug.WriteLine($"[{timestamp}] === OnStartHotkey called ===");
            
            if (Application.OpenForms.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[{timestamp}] Forms open: {Application.OpenForms.Count}, bringing to front");
                // Bring existing dialogs to front
                var topForm = Application.OpenForms[Application.OpenForms.Count - 1];
                topForm.BringToFront();
                topForm.Activate();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[{timestamp}] No forms open, starting timer directly");
                // Start timer directly without dialog when using hotkey
                if (_timerService?.CurrentState == TimerState.Idle)
                {
                    _timerService.Start("Hotkey start");
                    System.Diagnostics.Debug.WriteLine($"[{timestamp}] Timer started via hotkey");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[{timestamp}] Timer not in idle state: {_timerService?.CurrentState}");
                }
            }
            System.Diagnostics.Debug.WriteLine($"[{timestamp}] === OnStartHotkey completed ===");
        }

        private static void OnPauseHotkey()
        {
            OnPauseClicked(null, EventArgs.Empty);
        }

        private static void OnStopHotkey()
        {
            OnStopClicked(null, EventArgs.Empty);
        }

        private static void OnAddNoteHotkey()
        {
            OnAddNoteClicked(null, EventArgs.Empty);
        }

        private static void OnPeekHotkey()
        {
            OnPeekClicked(null, EventArgs.Empty);
        }

        // Overlay event handlers
        private static void OnTimerElapsedUpdated(object? sender, TimeSpan elapsed)
        {
            _overlayController?.OnElapsedUpdated(elapsed);
        }

        private static void OnToggleShowElapsed(object? sender, EventArgs e)
        {
            if (_settings == null || _overlayController == null) return;
            
            _settings.ShowElapsedInIndicator = !_settings.ShowElapsedInIndicator;
            _overlayController.ShowElapsed = _settings.ShowElapsedInIndicator;
            _settings.Save();
            
            // Update menu item
            if (sender is ToolStripMenuItem item)
            {
                item.Checked = _settings.ShowElapsedInIndicator;
            }
        }

        private static void SetAutoHideMinutes(int minutes)
        {
            if (_settings == null || _overlayController == null) return;
            
            _settings.OverlayAutoHideMinutes = minutes;
            _overlayController.AutoHideMinutes = minutes;
            _settings.Save();
            
            // Update menu items
            UpdateAutoHideMenuItems();
        }

        private static void UpdateAutoHideMenuItems()
        {
            if (_trayIcon?.ContextMenuStrip == null || _settings == null) return;
            
            var overlayMenu = _trayIcon.ContextMenuStrip.Items["Overlay"] as ToolStripMenuItem;
            if (overlayMenu?.DropDownItems["Auto-Hide After"] is ToolStripMenuItem autoHideMenu)
            {
                foreach (ToolStripMenuItem item in autoHideMenu.DropDownItems)
                {
                    if (item.Text.Contains("minutes") || item.Text == "Never")
                    {
                        var minutes = item.Text == "Never" ? 0 : int.Parse(item.Text.Split(' ')[0]);
                        item.Checked = minutes == _settings.OverlayAutoHideMinutes;
                    }
                }
            }
        }

        private static void OnPeekClicked(object? sender, EventArgs e)
        {
            if (_timerService?.CurrentState == TimerState.Running || 
                _timerService?.CurrentState == TimerState.Paused)
            {
                _overlayController?.Peek();
            }
        }

        private static void Cleanup()
        {
            _hotkeyManager?.Dispose();
            _trayIcon?.Dispose();
            _overlayController?.Dispose();
            _overlay?.Dispose();
            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
        }
    }
}
