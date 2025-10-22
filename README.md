# Chronomètre - Time Tracker

A lightweight, no-frills time tracking application for Windows built with C# .NET 8 and Windows Forms. Features global hotkeys, system tray integration, visual overlay indicator, and automatic logging.

## Features

- **Global Hotkeys** - Control the timer from anywhere using keyboard shortcuts
- **System Tray Integration** - Runs in the background with a system tray icon
- **Visual Overlay Indicator** - Red/orange circular indicator showing timer state
- **Auto-Hide Overlay** - Configurable auto-hide with peek functionality
- **Session Logging** - Automatic logging to desktop with daily totals
- **Session Notes** - Add notes when starting or during sessions
- **Single Instance** - Prevents multiple instances from running
- **Portable** - Single executable file, no installation required

## Requirements

- Windows 10/11 (64-bit)
- .NET 8.0 Runtime (included in self-contained build)

## Quick Start

1. Download the latest release from the [Releases](../../releases) page
2. Run `Chronometre.exe`
3. The application will appear in your system tray
4. Use the global hotkeys to control the timer

## Global Hotkeys

| Hotkey | Action |
|--------|--------|
| `Ctrl+Alt+F1` | Start new session |
| `Ctrl+Alt+F2` | Pause/Resume current session |
| `Ctrl+Alt+F3` | Stop current session |
| `Ctrl+Alt+F4` | Add note to current session |
| `Ctrl+Alt+F5` | Peek overlay indicator |

**Note:** These hotkeys are designed to avoid conflicts with PowerToys and other common applications.

## Usage

### Starting a Session
1. Press `Ctrl+Alt+F1` or right-click the tray icon and select "Start"
2. Add optional notes in the dialog that appears
3. The timer starts and a red overlay indicator appears

### During a Session
- **Pause/Resume**: Press `Ctrl+Alt+F2` or use the tray menu
- **Add Notes**: Press `Ctrl+Alt+F4` or use the tray menu
- **Stop Session**: Press `Ctrl+Alt+F3` or use the tray menu

### Visual Overlay
- **Red Circle**: Timer is running
- **Orange Circle**: Timer is paused
- **Hidden**: Timer is stopped
- **Auto-Hide**: Configurable (5, 10, 30 minutes, or never)
- **Peek**: Press `Ctrl+Alt+F5` to temporarily show hidden overlay

### Logging
- Log file is automatically created on your desktop as `Chrono-log.txt`
- Each session is logged with timestamp, duration, and notes
- Daily totals are automatically calculated
- Log format: `YYYY-MM-DD HH:mm:ss zzz Duration=HH:mm:ss DayTotal=HH:mm:ss State=Stopped Notes="..." AppVersion=X.Y.Z`

## Configuration

### Tray Menu Options
- **Overlay Settings**:
  - Show Elapsed Time in Indicator (checkbox)
  - Auto-Hide After (5, 10, 30 minutes, Never)
  - Peek Now (same as hotkey)

### Settings File
Settings are automatically saved to `settings.json` in the application directory:
```json
{
  "LogFolder": null,
  "HotkeyStartEnabled": true,
  "HotkeyPauseEnabled": true,
  "HotkeyStopEnabled": true,
  "HotkeyNoteEnabled": true,
  "HotkeyPeekEnabled": true,
  "ShowElapsedInIndicator": true,
  "OverlayAutoHideMinutes": 10
}
```

## Building from Source

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code (optional)

### Build Commands

#### Windows (PowerShell)
```powershell
# Build and publish
.\build.ps1

# Or manually
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -p:IncludeNativeLibrariesForSelfExtract=true
```

#### Windows (Command Prompt)
```cmd
# Build and publish
build.bat

# Or manually
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -p:IncludeNativeLibrariesForSelfExtract=true
```

### Project Structure
```
Chronometre/
├── Forms/                    # UI Forms
│   ├── NoteDialog.cs        # Add note dialog
│   ├── OverlayIndicatorForm.cs  # Visual overlay
│   └── StartDialog.cs       # Start session dialog
├── Services/                # Core services
│   ├── GlobalHotkeyManager.cs  # Global hotkey handling
│   ├── LogWriter.cs         # Session logging
│   ├── OverlayAutoHideController.cs  # Overlay management
│   ├── Settings.cs          # Settings management
│   └── TimerService.cs      # Timer logic
├── Program.cs               # Application entry point
├── Chronometre.csproj       # Project file
├── timer-icon.ico          # Application icon
├── build.ps1               # PowerShell build script
├── build.bat               # Batch build script
└── README.md               # This file
```

## Troubleshooting

### Hotkeys Not Working
- Ensure no other applications are using the same hotkey combinations
- Try running as administrator if hotkeys still don't work
- Check Windows PowerToys settings for conflicts

### Tray Icon Not Showing
- The application should appear in the system tray (notification area)
- If not visible, check if the tray icon is hidden in Windows settings
- Try right-clicking the taskbar and selecting "Show hidden icons"

### Log File Issues
- The log file is created on your desktop as `Chrono-log.txt`
- If desktop is not accessible, it falls back to Documents folder
- Check file permissions if logging fails

### Overlay Not Showing
- The overlay appears in the top-right corner by default
- Check if "Show Elapsed Time in Indicator" is enabled in tray menu
- Try the "Peek Now" option to test overlay visibility

## Development

### Running Tests
```bash
dotnet test
```

### Debug Mode
```bash
dotnet run
```

### Code Style
- Follow C# naming conventions
- Use nullable reference types
- Document public APIs with XML comments

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Changelog

### Version 1.1.0
- **Fixed hotkey persistence** - Hotkeys now work continuously, not just once
- **Improved global hotkey system** - Uses low-level keyboard hook for better reliability
- **Enhanced logging** - Log file now saves to Desktop as requested
- **Better error handling** - Improved debugging and error recovery
- **Code cleanup** - Removed unused hotkey implementations

### Version 1.0.0
- Initial release
- Global hotkey support
- System tray integration
- Visual overlay indicator
- Session logging with daily totals
- Auto-hide overlay functionality
- Session notes support
- Single instance enforcement

## Support

If you encounter any issues or have questions:
1. Check the [Troubleshooting](#troubleshooting) section
2. Search existing [Issues](../../issues)
3. Create a new issue with detailed information

## Acknowledgments

- Built with .NET 8 and Windows Forms
- Icons from System.Drawing.SystemIcons
- Global hotkey implementation using Windows API