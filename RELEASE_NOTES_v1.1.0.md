# Chronomètre v1.1.0 Release Notes

## 🎉 What's New

### ✨ Major Features
- **Professional Windows Installer** - Easy installation with proper Windows integration
- **System Tray Integration** - Minimal desktop footprint with full functionality
- **Global Hotkeys** - Work from any application with reliable hotkey detection
- **Automatic Logging** - Smart log file creation with fallback locations
- **Visual Overlay** - Quick status display with auto-hide functionality

### 🔧 Technical Improvements
- **Reliable Hotkey Manager** - Fixed "hotkeys only work once" issue
- **Smart Logging System** - Automatic path selection (Desktop → Documents → AppData)
- **Embedded Resources** - Icon and resources properly embedded in executable
- **Error Handling** - Comprehensive error handling and user feedback
- **Single Instance** - Prevents multiple instances from running

### 🚀 Installation & Distribution
- **Windows Installer** - Professional installation with shortcuts and registry integration
- **Uninstaller** - Clean removal with Add/Remove Programs integration
- **Documentation** - Comprehensive installation and usage guides
- **Release Package** - Ready-to-distribute installer package

## 📋 Features

### Global Hotkeys
- **Ctrl+Alt+F1** - Start Timer
- **Ctrl+Alt+F2** - Stop Timer  
- **Ctrl+Alt+F3** - Add Note
- **Ctrl+Alt+F4** - Show Overlay
- **Ctrl+Alt+F5** - Hide Overlay

### System Integration
- **Desktop Shortcut** - Easy access from desktop
- **Start Menu Integration** - Available in Windows Start Menu
- **System Tray** - Minimal footprint with full functionality
- **Add/Remove Programs** - Professional uninstallation

### Logging & Tracking
- **Automatic Log Creation** - Smart path selection for log files
- **Session Tracking** - Start/stop times with duration calculation
- **Note Support** - Add notes during work sessions
- **Visual Feedback** - Overlay display for current status

## 🛠️ Technical Details

### System Requirements
- **Operating System**: Windows 10 or later
- **.NET Runtime**: .NET 8.0 (included in installer)
- **Architecture**: x64
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

### Installation
- **Installer**: `install.bat` (requires administrator privileges)
- **Uninstaller**: `uninstall.bat` or Add/Remove Programs
- **Installation Path**: `C:\Program Files\Chronometre\`
- **Log File**: Desktop (with fallback to Documents/AppData)

### File Structure
```
Chronometre-v1.1.0/
├── Chronometre.exe          # Main application
├── [.NET Dependencies]      # All required .NET libraries
├── install.bat              # Windows installer
├── install.ps1             # PowerShell installer script
├── uninstall.bat            # Uninstaller
├── INSTALLATION.md          # Installation guide
├── README.md                # User documentation
└── LICENSE                  # MIT License
```

## 🔧 Bug Fixes

### Resolved Issues
- **Hotkeys Only Work Once** - Fixed with reliable hotkey manager
- **Application Crashes** - Resolved with proper error handling
- **Icon Not Displaying** - Fixed with embedded resources
- **Log File Permissions** - Smart path selection with fallbacks
- **Portable Version Issues** - Replaced with professional installer

### Performance Improvements
- **Faster Startup** - Optimized application initialization
- **Lower Memory Usage** - Efficient resource management
- **Reliable Hotkeys** - Improved hotkey detection and handling
- **Better Error Handling** - Comprehensive error management

## 📚 Documentation

### User Guides
- **README.md** - Complete user documentation
- **INSTALLATION.md** - Detailed installation guide
- **Release Notes** - This file with all changes

### Developer Information
- **Source Code** - Available on GitHub
- **Build Instructions** - Included in repository
- **Contributing** - Guidelines for contributions

## 🚀 Getting Started

### Quick Installation
1. **Download** the release package
2. **Extract** to a folder
3. **Right-click** `install.bat` and select "Run as administrator"
4. **Follow** the installation prompts
5. **Launch** from desktop shortcut or Start Menu

### First Use
1. **Look for** the Chronomètre icon in the system tray
2. **Right-click** for options or **left-click** to show interface
3. **Press** `Ctrl+Alt+F1` to start your first timer session
4. **Check** your Desktop for the `Chrono-log.txt` file

## 🔄 Migration from Previous Versions

### From v1.0.x
- **Uninstall** previous version first
- **Install** new version using the installer
- **Settings** will be preserved in AppData
- **Log files** will continue in the same location

### From Portable Versions
- **Remove** old portable files
- **Install** using the new installer
- **Log files** will be moved to Desktop (preferred location)

## 🐛 Known Issues

### Current Limitations
- **Hotkey Conflicts** - Some applications may conflict with global hotkeys
- **Antivirus Software** - May flag the application (add exception if needed)
- **Windows Updates** - May require reinstallation after major Windows updates

### Workarounds
- **Hotkey Conflicts** - Try different hotkey combinations in source code
- **Antivirus Issues** - Add Chronomètre to antivirus exceptions
- **Update Issues** - Reinstall using the latest release

## 📞 Support

### Getting Help
- **GitHub Issues** - Report bugs and request features
- **GitHub Discussions** - Ask questions and get help
- **Email Support** - Contact support@dev-ntic.com

### Troubleshooting
- **Check Log Files** - Look for error messages in `Chrono-log.txt`
- **Check System Tray** - Ensure the application is running
- **Check Permissions** - Ensure proper file permissions
- **Check .NET Runtime** - Ensure .NET 8.0 is installed

## 🎯 Future Plans

### Upcoming Features
- **Custom Hotkeys** - User-configurable hotkey combinations
- **Multiple Timers** - Support for multiple concurrent timers
- **Export Options** - CSV/Excel export for log data
- **Themes** - Customizable appearance options
- **Cloud Sync** - Optional cloud synchronization

### Technical Improvements
- **Performance Optimization** - Further memory and CPU optimizations
- **Enhanced Logging** - More detailed logging options
- **Better Error Handling** - Improved error recovery
- **Accessibility** - Better accessibility support

---

**Chronomètre v1.1.0** - Simple, efficient time tracking for Windows developers and professionals.

*Released: January 22, 2025*
*Version: 1.1.0.0*
*Build: Release*
