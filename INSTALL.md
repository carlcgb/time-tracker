# Installation Guide

## Quick Installation

### Option 1: Download Pre-built Executable (Recommended)
1. Go to the [Releases](../../releases) page
2. Download the latest `Chronometre.exe` file
3. Place it in any folder on your computer
4. Double-click to run

### Option 2: Build from Source
1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/chronometre.git
   cd chronometre
   ```
3. Build the application:
   ```bash
   # Windows PowerShell
   .\build.ps1
   
   # Windows Command Prompt
   build.bat
   
   # Or manually
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -p:IncludeNativeLibrariesForSelfExtract=true
   ```
4. Find the executable in `bin\Release\net8.0-windows\win-x64\publish\Chronometre.exe`

## System Requirements

- **Operating System**: Windows 10/11 (64-bit)
- **Runtime**: .NET 8.0 (included in self-contained build)
- **Memory**: 50 MB available RAM
- **Disk Space**: 70 MB for the executable

## First Run

1. **Run the application** - Double-click `Chronometre.exe`
2. **Check system tray** - Look for the timer icon in your system tray (notification area)
3. **Test hotkeys** - Press `Ctrl+Alt+F1` to start a session
4. **Check log file** - A `Chrono-log.txt` file will be created on your desktop

## Configuration

### Tray Menu
Right-click the system tray icon to access:
- **Start** - Begin a new session
- **Pause/Resume** - Pause or resume current session
- **Stop** - End current session
- **Add Note** - Add notes to current session
- **Overlay Settings** - Configure visual overlay
- **Open Log Folder** - Open the folder containing the log file
- **Exit** - Close the application

### Settings File
Settings are automatically saved to `settings.json` in the application directory. You can edit this file to customize:
- Log file location
- Hotkey enable/disable flags
- Overlay settings

## Troubleshooting

### Application Won't Start
- Ensure you have Windows 10/11 (64-bit)
- Check if .NET 8.0 Runtime is installed (for non-self-contained builds)
- Try running as administrator

### Hotkeys Not Working
- Ensure no other applications are using the same hotkey combinations
- Try running as administrator
- Check Windows PowerToys settings for conflicts

### Tray Icon Not Visible
- Check if the tray icon is hidden in Windows notification area settings
- Right-click the taskbar and select "Show hidden icons"
- Look for a timer icon in the system tray

### Log File Issues
- The log file is created on your desktop as `Chrono-log.txt`
- If desktop is not accessible, it falls back to Documents folder
- Check file permissions if logging fails

## Uninstallation

1. **Close the application** - Right-click the tray icon and select "Exit"
2. **Delete the executable** - Remove `Chronometre.exe` from your computer
3. **Delete settings** (optional) - Remove `settings.json` if you want to clear all settings
4. **Delete log file** (optional) - Remove `Chrono-log.txt` if you want to clear the log

## Support

If you encounter any issues:
1. Check the [Troubleshooting](#troubleshooting) section
2. Search existing [Issues](../../issues)
3. Create a new issue with detailed information

## Security Note

This application:
- Does not require internet access
- Does not collect or transmit personal data
- Stores all data locally on your computer
- Uses only standard Windows APIs for hotkeys and system tray integration
