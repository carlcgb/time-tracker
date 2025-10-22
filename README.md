# ⏱️ Chronomètre - Professional Time Tracker

> **The ultimate time tracking solution for Windows developers and professionals**

![Version](https://img.shields.io/badge/Version-1.1.0.0-blue?style=for-the-badge&logo=windows)
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
| `Ctrl+Alt+F2` | Stop Timer |
| `Ctrl+Alt+F3` | Add Note |
| `Ctrl+Alt+F4` | Show Overlay |
| `Ctrl+Alt+F5` | Hide Overlay |

## 🎉 **Latest Release: v1.1.0**

### 🆕 **What's New in v1.1.0**
- ✨ **Professional Windows Installer** - Easy installation with proper Windows integration
- 🎯 **System Tray Integration** - Minimal desktop footprint with full functionality  
- ⌨️ **Reliable Global Hotkeys** - Fixed "hotkeys only work once" issue
- 📊 **Smart Logging System** - Automatic path selection (Desktop → Documents → AppData)
- 🖼️ **Embedded Resources** - Icon and resources properly embedded
- 🛡️ **Enhanced Error Handling** - Comprehensive error management and user feedback
- 🚀 **Single Instance Protection** - Prevents multiple instances from running

### 📦 **Download & Install**

#### **🚀 Quick Installation (Recommended)**
1. **Download** the latest release: `Chronometre-v1.1.0-Windows-x64.zip`
2. **Extract** the ZIP file to a folder
3. **Right-click** `install.bat` and select **"Run as administrator"**
4. **Follow** the installation prompts
5. **Launch** from desktop shortcut or Start Menu

#### **📋 System Requirements**
- **OS**: Windows 10 or later
- **Architecture**: x64
- **Runtime**: .NET 8.0 (included in installer)
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

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
