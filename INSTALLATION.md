# Chronomètre Installation Guide

## Overview
Chronomètre is a simple time tracker application with global hotkeys. This guide will help you install and configure the application.

## System Requirements
- Windows 10 or later
- .NET 8.0 Runtime (will be installed automatically if needed)
- Administrator privileges for installation

## Installation Methods

### Method 1: Automated Installer (Recommended)
1. **Download the installer files:**
   - `install.bat` - Main installer
   - `install.ps1` - PowerShell installer script
   - `installer-build/` - Application files directory

2. **Run the installer:**
   - Right-click on `install.bat`
   - Select "Run as administrator"
   - Follow the on-screen instructions

3. **What the installer does:**
   - Installs the application to `C:\Program Files\Chronometre\`
   - Creates desktop shortcut
   - Creates Start Menu shortcut
   - Registers uninstaller in Add/Remove Programs
   - Sets up proper file permissions

### Method 2: Manual Installation
1. **Copy application files:**
   - Copy the entire `installer-build` folder to `C:\Program Files\Chronometre\`

2. **Create shortcuts:**
   - Create desktop shortcut to `Chronometre.exe`
   - Create Start Menu shortcut to `Chronometre.exe`

3. **Set permissions:**
   - Ensure the application has proper read/write permissions

## Features After Installation

### Global Hotkeys
- **Ctrl+Alt+F1** - Start timer
- **Ctrl+Alt+F2** - Stop timer
- **Ctrl+Alt+F3** - Add note
- **Ctrl+Alt+F4** - Show overlay
- **Ctrl+Alt+F5** - Hide overlay

### System Tray Integration
- The application runs in the system tray
- Right-click the tray icon for options
- Left-click to show/hide the main interface

### Logging
- Automatic log file creation
- Logs saved to Desktop by default
- Fallback to Documents folder if Desktop is not accessible
- Log file: `Chrono-log.txt`

## Configuration

### Log File Location
The application automatically determines the best location for the log file:
1. **Desktop** (preferred)
2. **Documents folder** (fallback)
3. **AppData folder** (final fallback)

### Hotkey Customization
Hotkeys are currently fixed but can be modified in the source code if needed.

## Troubleshooting

### Application Won't Start
1. **Check .NET Runtime:**
   - Ensure .NET 8.0 Runtime is installed
   - Download from Microsoft if needed

2. **Check Permissions:**
   - Ensure the application has proper permissions
   - Try running as administrator

3. **Check Antivirus:**
   - Some antivirus software may block the application
   - Add exception if needed

### Hotkeys Not Working
1. **Check Global Hotkeys:**
   - Ensure no other application is using the same hotkeys
   - Try different hotkey combinations

2. **Check Application State:**
   - Ensure the application is running
   - Check system tray for the application icon

### Log File Issues
1. **Check File Permissions:**
   - Ensure the application can write to the log location
   - Check if the log file is read-only

2. **Check Disk Space:**
   - Ensure sufficient disk space for log files

## Uninstallation

### Method 1: Using the Uninstaller
1. **Run the uninstaller:**
   - Right-click on `uninstall.bat`
   - Select "Run as administrator"
   - Follow the on-screen instructions

### Method 2: Using Add/Remove Programs
1. **Open Add/Remove Programs:**
   - Go to Settings > Apps > Apps & features
   - Find "Chronomètre Time Tracker"
   - Click "Uninstall"

### Method 3: Manual Uninstallation
1. **Remove application files:**
   - Delete the installation folder
   - Remove shortcuts from Desktop and Start Menu

## Support

### Log Files
- Check `Chrono-log.txt` for application logs
- Check Windows Event Viewer for system errors

### Common Issues
- **Application crashes:** Check .NET Runtime installation
- **Hotkeys not working:** Check for conflicting applications
- **Log file not created:** Check file permissions

### Contact
For support or bug reports, please contact the development team.

## Version Information
- **Version:** 1.1.0.0
- **Build Date:** $(Get-Date -Format "yyyy-MM-dd")
- **Target Framework:** .NET 8.0
- **Platform:** Windows x64

---

**Note:** This application requires administrator privileges for installation and proper system integration.
