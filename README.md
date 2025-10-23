# ⏱️ Chronomètre - Professional Time Tracker

> **The ultimate time tracking solution for Windows developers and professionals**

![Version](https://img.shields.io/badge/Version-1.1.3.0-blue?style=for-the-badge&logo=windows)
![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet)
![Platform](https://img.shields.io/badge/Platform-Windows-green?style=for-the-badge&logo=windows)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge&logo=opensourceinitiative)
![Downloads](https://img.shields.io/badge/Downloads-Ready-brightgreen?style=for-the-badge)

**Chronomètre** is a powerful, lightweight time tracking application designed for Windows users who need reliable time tracking with global hotkeys. Whether you're a developer, freelancer, or professional, Chronomètre provides the perfect solution for tracking your work sessions efficiently.

## 🎯 **Why Choose Chronomètre?**

- ⚡ **Lightning Fast** - Start tracking in seconds with global hotkeys
- 🎯 **Always Available** - Works from any application, anywhere
- 📊 **Smart Logging** - Automatic time tracking with intelligent file placement
- 🖥️ **Minimal Footprint** - Runs quietly in the system tray
- 🔧 **Professional Installation** - Proper Windows integration with uninstaller
- 🚀 **Zero Configuration** - Works out of the box with sensible defaults

## 🚀 Features

- **Global Hotkeys** - Work from any application
- **System Tray Integration** - Minimal desktop footprint
- **Automatic Logging** - Time tracking with notes
- **Visual Overlay** - Quick status display
- **Easy Installation** - Professional Windows installer
- **No Internet Required** - Fully offline operation

## ⌨️ Global Hotkeys

| Hotkey | Action |
|--------|--------|
| `Ctrl+Alt+F1` | Start Timer |
| `Ctrl+Alt+F2` | Pause/Resume Timer |
| `Ctrl+Alt+F3` | Stop & Save Session |
| `Ctrl+Alt+F4` | Add Note |
| `Ctrl+Alt+F5` | Show/Hide Overlay |

## 🎉 **Latest Release: v1.1.3**

### 🆕 **What's New in v1.1.3**
- 🐛 **Critical Logging Fix** - Resolved "Access denied" errors when writing to log file
- 🔧 **Robust Path Detection** - Multi-method path resolution with write permission testing
- 🛡️ **Enhanced Error Handling** - Comprehensive error recovery and debugging
- 📁 **Automatic Directory Creation** - Creates required directories automatically
- 🔄 **Fallback Mechanisms** - Graceful degradation to safe locations
- 📊 **Improved Reliability** - Sessions now properly save to log file
- 🎯 **Better User Experience** - No more logging failures or data loss
- 🚀 **Portable Executable** - Single-file distribution with embedded dependencies
- 📝 **Smart Logging** - Creates log file in same directory as executable
- 🎯 **Reliable Hotkeys** - All hotkeys work consistently across applications

### 📦 **Download & Install**

#### **🚀 Portable Version (Recommended)**
1. **Download** the latest release: `Chronometre-v1.1.3-Portable.zip`
2. **Extract** the ZIP file to any folder
3. **Run** `Chronometre.exe` directly
4. **Log file** will be created automatically in the same folder
5. **No installation required** - just copy and run!

#### **📋 System Requirements**
- **OS**: Windows 10 or later
- **Architecture**: x64
- **Runtime**: .NET 8.0 (included in portable version)
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

### What's Included

- **Single Executable**: `Chronometre.exe` with all dependencies
- **Automatic Logging**: Creates `Chrono-log.txt` in same directory
- **No Installation**: Copy to any folder and run
- **Portable**: Works from USB drives, network shares, etc.

## 🎯 Usage

### Starting the Application

1. **Launch from Desktop** - Double-click the Chronomètre shortcut
2. **Launch from Start Menu** - Search for "Chronomètre" in Start Menu
3. **System Tray** - Look for the Chronomètre icon in the system tray

### Basic Workflow

1. **Start Timer** - Press `Ctrl+Alt+F1` to begin tracking time
2. **Add Notes** - Press `Ctrl+Alt+F4` to add notes during work
3. **Pause/Resume** - Press `Ctrl+Alt+F2` to pause or resume tracking
4. **Stop & Save** - Press `Ctrl+Alt+F3` to stop and save session
5. **View Logs** - Check `Chrono-log.txt` in the same directory as the executable

### System Tray

- **Right-click** the tray icon for options
- **Left-click** to show/hide the main interface
- **Double-click** to start/stop timer quickly

## 📊 Logging

### Automatic Log Creation

The application automatically creates log files using robust path detection:

1. **Executable Directory** - `Chrono-log.txt` in same folder as executable (preferred)
2. **Assembly Location** - Based on application location (fallback)
3. **Current Directory** - Working directory (fallback)
4. **Documents Folder** - `Chrono-log.txt` in Documents (final fallback)

The application tests write permissions before using any path to ensure reliable logging.

### Log Format

```
# Chronomètre Time Tracker Log - Chrono-log.txt
# Format: Date	StartTime	EndTime	Duration	Notes
# All sessions are aggregated in this single file

2025-10-23	14:30:15	15:45:30	01:15:15	Working on project documentation
2025-10-23	16:00:00	17:30:00	01:30:00	Code review and testing
```

## 🛠️ Configuration

### Settings

The application stores settings in:
- **Settings File**: `%APPDATA%\Chronometre\settings.json`
- **Log File**: Desktop (or Documents/AppData as fallback)

### Customization

Currently, hotkeys are fixed but can be modified in the source code if needed.

## 🔧 Troubleshooting

### Common Issues

#### Application Won't Start
- **Check .NET Runtime**: Ensure .NET 8.0 Runtime is installed
- **Check Permissions**: Try running as administrator
- **Check Antivirus**: Add exception if blocked

#### Hotkeys Not Working
- **Check Conflicts**: Ensure no other app uses the same hotkeys
- **Check Application State**: Ensure Chronomètre is running
- **Check System Tray**: Look for the Chronomètre icon

#### Log File Issues
- **Check Permissions**: Ensure write access to log location
- **Check Disk Space**: Ensure sufficient disk space
- **Check File Locks**: Close any applications that might lock the file

### Getting Help

1. **Check Log Files** - Look for error messages in `Chrono-log.txt`
2. **Check Windows Event Viewer** - Look for system errors
3. **Check System Requirements** - Ensure Windows 10+ and .NET 8.0

## 🗑️ Uninstallation

### Method 1: Using Uninstaller
1. **Run `uninstall.bat`** as administrator
2. **Follow the prompts** to remove the application

### Method 2: Add/Remove Programs
1. **Open Settings** > Apps > Apps & features
2. **Find "Chronomètre Time Tracker"**
3. **Click "Uninstall"**

### Method 3: Manual Removal
1. **Delete installation folder**: `C:\Program Files\Chronometre\`
2. **Remove shortcuts** from Desktop and Start Menu
3. **Remove registry entries** (advanced users only)

## 🏗️ Development

### Building from Source

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/chronometre.git
   cd chronometre
   ```

2. **Build the application**:
   ```bash
   dotnet build -c Release
   ```

3. **Create installer**:
   ```bash
   dotnet publish -c Release -r win-x64 -p:PublishSingleFile=false -o installer-build
   ```

### Project Structure

```
Chronometre/
├── Forms/                 # Windows Forms
│   ├── StartDialog.cs    # Timer start dialog
│   ├── NoteDialog.cs     # Note input dialog
│   └── OverlayIndicatorForm.cs  # Status overlay
├── Services/             # Core services
│   ├── TimerService.cs   # Timer logic
│   ├── LogWriter.cs      # Logging functionality
│   ├── ReliableHotkeyManager.cs  # Hotkey management
│   ├── Settings.cs       # Configuration
│   └── OverlayAutoHideController.cs  # Overlay management
├── Program.cs            # Application entry point
├── Chronometre.csproj    # Project file
└── timer-icon.ico        # Application icon
```

## 📋 System Requirements

- **Operating System**: Windows 10 or later
- **.NET Runtime**: .NET 8.0 (included in installer)
- **Architecture**: x64
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Commit your changes**: `git commit -m 'Add amazing feature'`
4. **Push to the branch**: `git push origin feature/amazing-feature`
5. **Open a Pull Request**

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/chronometre/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/chronometre/discussions)
- **Email**: carl@vrille.io

## 🎉 Acknowledgments

- Built with [.NET 8.0](https://dotnet.microsoft.com/)
- Windows Forms for the user interface
- Global hotkey implementation using Windows API
- System tray integration for minimal footprint

- Built with .NET 8 and Windows Forms
- Icons from System.Drawing.SystemIcons
- Global hotkey implementation using Windows API

With ❤️ from CGB
