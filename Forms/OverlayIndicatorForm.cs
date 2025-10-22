using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chronometre.Forms
{
    public partial class OverlayIndicatorForm : Form
    {
        private bool _showElapsed = true;
        private bool _isAutoHidden = false;

        public bool ShowElapsed => _showElapsed;
        public bool IsAutoHidden => _isAutoHidden;
        private TimeSpan _currentElapsed = TimeSpan.Zero;
        private System.Windows.Forms.Timer _fadeTimer;
        private System.Windows.Forms.Timer _peekTimer;
        private double _opacity = 0.7;
        private const int FadeSteps = 15;
        private const int FadeInterval = 10; // 150ms total fade time

        // Win32 API for transparency and click-through
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOPMOST = 0x8;
        private const uint LWA_ALPHA = 0x2;

        public OverlayIndicatorForm()
        {
            InitializeComponent();
            SetupTransparency();
        }

        private void InitializeComponent()
        {
            // Form properties
            Text = "Chronomètre Overlay";
            Size = new Size(32, 32);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.Red;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Position in top-right corner
            var screen = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(screen.Right - Size.Width - 10, screen.Top + 10);

            // Setup fade timer
            _fadeTimer = new System.Windows.Forms.Timer { Interval = FadeInterval };
            _fadeTimer.Tick += OnFadeTimerTick;

            // Setup peek timer
            _peekTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _peekTimer.Tick += OnPeekTimerTick;
        }

        private void SetupTransparency()
        {
            var exStyle = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOPMOST);
            SetLayeredWindowAttributes(Handle, 0, (byte)(_opacity * 255), LWA_ALPHA);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupTransparency();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw circle background
            var rect = new Rectangle(2, 2, Width - 4, Height - 4);
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillEllipse(brush, rect);
            }

            // Draw elapsed time if enabled and visible
            if (_showElapsed && !_isAutoHidden && _currentElapsed != TimeSpan.Zero)
            {
                var text = _currentElapsed.ToString(@"hh\:mm\:ss");
                var font = new Font("Consolas", 8, FontStyle.Bold);
                var textSize = g.MeasureString(text, font);
                
                var textRect = new RectangleF(
                    (Width - textSize.Width) / 2,
                    (Height - textSize.Height) / 2,
                    textSize.Width,
                    textSize.Height
                );

                using (var textBrush = new SolidBrush(Color.White))
                {
                    g.DrawString(text, font, textBrush, textRect);
                }
            }
        }

        public void SetShowElapsed(bool show)
        {
            _showElapsed = show;
            if (Visible && !_isAutoHidden)
            {
                Invalidate();
            }
        }

        public void ShowRunningIndicator(TimeSpan elapsed)
        {
            _isAutoHidden = false;
            _currentElapsed = elapsed;
            BackColor = Color.Red;
            Show();
            FadeIn();
        }

        public void ShowPausedIndicator(TimeSpan elapsed)
        {
            _isAutoHidden = false;
            _currentElapsed = elapsed;
            BackColor = Color.Orange;
            Show();
            FadeIn();
        }

        public void HideIndicator()
        {
            _isAutoHidden = true;
            FadeOut();
        }

        public void Peek(int milliseconds = 3000)
        {
            if (!Visible)
            {
                Show();
            }
            
            _isAutoHidden = false;
            FadeIn();
            
            _peekTimer.Interval = milliseconds;
            _peekTimer.Start();
        }

        public void UpdateElapsed(TimeSpan elapsed)
        {
            _currentElapsed = elapsed;
            if (Visible && !_isAutoHidden && _showElapsed)
            {
                Invalidate();
            }
        }

        private void FadeIn()
        {
            _fadeTimer.Stop();
            _fadeTimer.Tag = "fadein";
            _fadeTimer.Start();
        }

        private void FadeOut()
        {
            _fadeTimer.Stop();
            _fadeTimer.Tag = "fadeout";
            _fadeTimer.Start();
        }

        private void OnFadeTimerTick(object? sender, EventArgs e)
        {
            var direction = _fadeTimer.Tag?.ToString();
            if (direction == "fadein")
            {
                _opacity = Math.Min(0.7, _opacity + (0.7 / FadeSteps));
                if (_opacity >= 0.7)
                {
                    _fadeTimer.Stop();
                }
            }
            else if (direction == "fadeout")
            {
                _opacity = Math.Max(0.0, _opacity - (0.7 / FadeSteps));
                if (_opacity <= 0.0)
                {
                    _fadeTimer.Stop();
                    Hide();
                }
            }

            if (Visible)
            {
                SetLayeredWindowAttributes(Handle, 0, (byte)(_opacity * 255), LWA_ALPHA);
            }
        }

        private void OnPeekTimerTick(object? sender, EventArgs e)
        {
            _peekTimer.Stop();
            if (_isAutoHidden)
            {
                FadeOut();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fadeTimer?.Dispose();
            _peekTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
