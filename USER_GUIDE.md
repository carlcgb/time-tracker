# 📖 Chronomètre User Guide

> **Complete guide to using Chronomètre time tracker**

## 🚀 Getting Started

### System Requirements

- **Operating System**: Windows 10 or later
- **Architecture**: x64
- **Runtime**: .NET 8.0 (included in portable version)
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

### Installation

#### Portable Version (Recommended)

1. **Download** the latest release: `Chronometre-v1.1.3-Portable.zip`
2. **Extract** the ZIP file to any folder
3. **Run** `Chronometre.exe` directly
4. **Log file** will be created automatically in the same folder
5. **No installation required** - just copy and run!

#### What's Included

- **Single Executable**: `Chronometre.exe` with all dependencies
- **Automatic Logging**: Creates `Chrono-log.txt` in same directory
- **No Installation**: Copy to any folder and run
- **Portable**: Works from USB drives, network shares, etc.

## ⌨️ Global Hotkeys

| Hotkey | Action | When Available |
|--------|--------|----------------|
| `Ctrl+Alt+F1` | Start Timer | When idle |
| `Ctrl+Alt+F2` | Pause/Resume Timer | When running or paused |
| `Ctrl+Alt+F3` | Stop & Save Session | When running or paused |
| `Ctrl+Alt+F4` | Add Note | When running or paused |
| `Ctrl+Alt+F5` | Show/Hide Overlay | When running or paused |

### Hotkey Tips

- **Global Access**: Hotkeys work from any application
- **No Conflicts**: Uses unique key combinations
- **Visual Feedback**: System tray icon shows current state
- **Reliable**: Tested across different Windows applications

## 🎯 Basic Workflow

### Starting a Session

1. **Press `Ctrl+Alt+F1`** - Opens start dialog
2. **Add initial notes** (optional) - Describe what you're working on
3. **Click "Start"** - Timer begins running
4. **System tray icon** shows running state with elapsed time

### During a Session

1. **Add Notes**: Press `Ctrl+Alt+F4` to add notes during work
2. **Pause/Resume**: Press `Ctrl+Alt+F2` to pause or resume
3. **Check Status**: Press `Ctrl+Alt+F5` to peek at overlay
4. **System Tray**: Right-click for menu options

### Ending a Session

1. **Press `Ctrl+Alt+F3`** - Stops timer and shows confirmation
2. **Review Session**: Check start time, duration, and notes
3. **Confirm Save**: Session is automatically saved to log
4. **View Log**: Check `Chrono-log.txt` for session history

## 🖥️ System Tray Interface

### Tray Icon States

- **Gray (Idle)**: No active session
- **Green (Running)**: Timer is active
- **Yellow (Paused)**: Timer is paused
- **Tooltip**: Shows current state and elapsed time

### Right-Click Menu

- **Start** - Begin new session
- **Pause/Resume** - Toggle pause state
- **Stop & Save** - End current session
- **Add Note** - Add note to current session
- **Overlay** - Overlay settings and controls
- **Open Log File** - View session history
- **Exit** - Close application

### Overlay Controls

- **Show Elapsed Time** - Toggle elapsed time display
- **Auto-Hide Settings** - 5min, 10min, 30min, or Never
- **Peek Now** - Temporarily show overlay

## 📊 Logging System

### Automatic Log Creation

The application uses robust path detection to create log files:

1. **Executable Directory** - `Chrono-log.txt` in same folder as executable (preferred)
2. **Assembly Location** - Based on application location (fallback)
3. **Current Directory** - Working directory (fallback)
4. **Documents Folder** - `Chrono-log.txt` in Documents (final fallback)

The application tests write permissions before using any path to ensure reliable logging.

### Log File Format

```
# Chronomètre Time Tracker Log - Chrono-log.txt
# Format: Date	StartTime	EndTime	Duration	Notes
# All sessions are aggregated in this single file

2025-10-23	14:30:15	15:45:30	01:15:15	Working on project documentation
2025-10-23	16:00:00	17:30:00	01:30:00	Code review and testing
```

### Log File Structure

- **Header**: File description and format information
- **Sessions**: Tab-separated values for each session
- **Date**: Session date (YYYY-MM-DD)
- **Start Time**: Session start time (HH:MM:SS)
- **End Time**: Session end time (HH:MM:SS) or N/A if still running
- **Duration**: Total session time (HH:MM:SS)
- **Notes**: Session notes separated by semicolons

### Daily Totals

The application automatically calculates daily totals and includes them in the log for easy tracking of daily work hours.

## 🛠️ Configuration

### Settings File

Settings are stored in: `%APPDATA%\Chronometre\settings.json`

### Available Settings

- **LastUsedLogFolder**: Last used log file location
- **StartHotkeyEnabled**: Enable/disable start hotkey
- **PauseHotkeyEnabled**: Enable/disable pause hotkey
- **StopHotkeyEnabled**: Enable/disable stop hotkey
- **AddNoteHotkeyEnabled**: Enable/disable add note hotkey
- **StartMinimized**: Start application minimized
- **ShowElapsedInIndicator**: Show elapsed time in overlay
- **OverlayAutoHideMinutes**: Auto-hide overlay after X minutes

### Customization

Currently, hotkeys are fixed but can be modified in the source code if needed. Future versions may include configurable hotkeys.

## 🔧 Troubleshooting

### Common Issues

#### Application Won't Start

**Symptoms**: Application doesn't start or crashes immediately

**Solutions**:
- **Check .NET Runtime**: Ensure .NET 8.0 Runtime is installed (included in portable version)
- **Check Permissions**: Try running as administrator
- **Check Antivirus**: Add exception if blocked
- **Check System Requirements**: Ensure Windows 10+ and x64 architecture

#### Hotkeys Not Working

**Symptoms**: Hotkeys don't respond or work inconsistently

**Solutions**:
- **Check Conflicts**: Ensure no other app uses the same hotkeys
- **Check Application State**: Ensure Chronomètre is running
- **Check System Tray**: Look for the Chronomètre icon
- **Restart Application**: Close and restart Chronomètre
- **Check Focus**: Some applications may block global hotkeys

#### Log File Issues

**Symptoms**: Sessions not saving or log file not created

**Solutions**:
- **Check Permissions**: Ensure write access to log location
- **Check Disk Space**: Ensure sufficient disk space
- **Check File Locks**: Close any applications that might lock the file
- **Check Path**: Verify log file location in system tray menu
- **Manual Creation**: Try creating the log file manually

#### Overlay Not Showing

**Symptoms**: Overlay doesn't appear or disappears immediately

**Solutions**:
- **Check Settings**: Verify overlay settings in system tray menu
- **Check Timer State**: Overlay only shows when timer is active
- **Check Auto-Hide**: Overlay may be set to auto-hide quickly
- **Restart Application**: Close and restart Chronomètre

### Advanced Troubleshooting

#### Debug Information

1. **Check Log Files**: Look for error messages in `Chrono-log.txt`
2. **Check Windows Event Viewer**: Look for system errors
3. **Check System Requirements**: Ensure Windows 10+ and .NET 8.0
4. **Check Error Logs**: Look for `Chronometre_error.log` in temp directory

#### Performance Issues

- **Memory Usage**: Application uses ~50MB RAM
- **CPU Usage**: Minimal CPU usage when idle
- **Disk Usage**: Log files are small (few KB per session)
- **Network**: No network usage (fully offline)

#### Compatibility Issues

- **Windows Version**: Requires Windows 10 or later
- **Architecture**: x64 only (32-bit not supported)
- **.NET Version**: .NET 8.0 required (included in portable version)
- **Antivirus**: Some antivirus software may block global hotkeys

## 📈 Tips & Best Practices

### Efficient Time Tracking

1. **Start with Notes**: Always add initial notes when starting a session
2. **Regular Breaks**: Use pause/resume for breaks instead of stopping
3. **Daily Review**: Check daily totals in the log file
4. **Consistent Naming**: Use consistent note formats for easy searching

### Workflow Integration

1. **Project-Based**: Create separate folders for different projects
2. **Daily Logs**: Review logs daily to track progress
3. **Export Data**: Copy log data to spreadsheets for analysis
4. **Backup Logs**: Regularly backup log files

### Productivity Tips

1. **Global Hotkeys**: Learn the hotkeys for faster operation
2. **System Tray**: Use system tray for quick status checks
3. **Overlay**: Use overlay for quick time checks
4. **Notes**: Add detailed notes for better session tracking

## 🔄 Updates & Maintenance

### Updating the Application

1. **Download Latest**: Get the newest version from GitHub
2. **Backup Logs**: Save your current log files
3. **Replace Executable**: Copy new executable to same folder
4. **Restart Application**: Close and restart Chronomètre

### Log File Management

1. **Regular Backups**: Backup log files regularly
2. **Archive Old Logs**: Move old logs to archive folders
3. **Export Data**: Export to CSV for analysis
4. **Clean Up**: Remove old log files if needed

### System Maintenance

1. **Regular Updates**: Keep Windows and .NET updated
2. **Antivirus**: Ensure Chronomètre is whitelisted
3. **Permissions**: Maintain write permissions to log folders
4. **Disk Space**: Ensure sufficient disk space for logs

## 📞 Support & Resources

### Getting Help

1. **GitHub Issues**: [Report bugs or request features](https://github.com/carlcgb/time-tracker/issues)
2. **Documentation**: Check this guide and README.md
3. **Log Files**: Check `Chrono-log.txt` for session history
4. **Error Logs**: Check temp directory for error logs

### Community

- **GitHub Discussions**: [Community discussions](https://github.com/carlcgb/time-tracker/discussions)
- **Feature Requests**: [Request new features](https://github.com/carlcgb/time-tracker/issues)
- **Bug Reports**: [Report issues](https://github.com/carlcgb/time-tracker/issues)

### Contact

- **Email**: carl@vrille.io
- **GitHub**: [carlcgb](https://github.com/carlcgb)
- **Project**: [time-tracker](https://github.com/carlcgb/time-tracker)

---

**Need more help?** Check the [README.md](README.md) for quick start instructions or [Release Notes](RELEASE_NOTES_v1.1.3.md) for latest updates.
