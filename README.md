# Chronomètre - Time Tracker

A simple, efficient time tracking application with global hotkeys for Windows.

![Chronomètre](https://img.shields.io/badge/Version-1.1.0.0-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Platform](https://img.shields.io/badge/Platform-Windows-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

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
| `Ctrl+Alt+F2` | Stop Timer |
| `Ctrl+Alt+F3` | Add Note |
| `Ctrl+Alt+F4` | Show Overlay |
| `Ctrl+Alt+F5` | Hide Overlay |

## 📦 Installation

### Quick Install (Recommended)

1. **Download the latest release** from [GitHub Releases](https://github.com/yourusername/chronometre/releases)
2. **Extract the installer files** to a folder
3. **Right-click `install.bat`** and select "Run as administrator"
4. **Follow the installation prompts**

### What Gets Installed

- Application files to `C:\Program Files\Chronometre\`
- Desktop shortcut for easy access
- Start Menu shortcut
- Uninstaller in Add/Remove Programs
- Proper Windows integration

## 🎯 Usage

### Starting the Application

1. **Launch from Desktop** - Double-click the Chronomètre shortcut
2. **Launch from Start Menu** - Search for "Chronomètre" in Start Menu
3. **System Tray** - Look for the Chronomètre icon in the system tray

### Basic Workflow

1. **Start Timer** - Press `Ctrl+Alt+F1` to begin tracking time
2. **Add Notes** - Press `Ctrl+Alt+F3` to add notes during work
3. **Stop Timer** - Press `Ctrl+Alt+F2` to stop tracking
4. **View Logs** - Check `Chrono-log.txt` on your Desktop

### System Tray

- **Right-click** the tray icon for options
- **Left-click** to show/hide the main interface
- **Double-click** to start/stop timer quickly

## 📊 Logging

### Automatic Log Creation

The application automatically creates log files in the following priority order:

1. **Desktop** - `Chrono-log.txt` (preferred)
2. **Documents** - `Chrono-log.txt` (fallback)
3. **AppData** - `Chrono-log.txt` (final fallback)

### Log Format

```
[2025-01-22 14:30:15] Session Started
[2025-01-22 14:30:15] Note: Working on project documentation
[2025-01-22 15:45:30] Session Ended - Duration: 01:15:15
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
- **Email**: support@dev-ntic.com

## 🎉 Acknowledgments

- Built with [.NET 8.0](https://dotnet.microsoft.com/)
- Windows Forms for the user interface
- Global hotkey implementation using Windows API
- System tray integration for minimal footprint

- Built with .NET 8 and Windows Forms
- Icons from System.Drawing.SystemIcons
- Global hotkey implementation using Windows API

With ❤️ from CGB
