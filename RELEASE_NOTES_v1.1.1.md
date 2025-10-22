# 🚀 Chronomètre v1.1.1 Release Notes

**Release Date:** Octobre 22, 2025  
**Version:** 1.1.1.0  
**Type:** Bug Fixes & Improvements

---

## 🎯 **What's New in v1.1.1**

### 🐛 **Critical Bug Fixes**

#### **✅ Duration Calculation Fixed**
- **Issue**: Log entries showed `00:00:00` duration for all sessions
- **Fix**: Improved `CalculateElapsedTime()` method to properly calculate session duration
- **Result**: Log now displays actual session time (e.g., `01:15:30` instead of `00:00:00`)

#### **✅ Start Hotkey Reliability Fixed**
- **Issue**: `Ctrl+Alt+F1` (Start Timer) only worked once, then stopped responding
- **Fix**: Modified `OnStartHotkey()` to start timer directly without showing dialog
- **Result**: Start hotkey now works continuously like other hotkeys

### 🔧 **Technical Improvements**

#### **Portable Executable Optimization**
- **Log File Location**: Now creates `Chrono-log.txt` in the same directory as the executable
- **Path Resolution**: Uses `AppContext.BaseDirectory` for single-file app compatibility
- **Simplified Distribution**: No complex installer needed - just copy and run

#### **Hotkey System Enhancement**
- **Direct Timer Start**: Hotkey bypasses dialog for immediate timer start
- **Consistent Behavior**: All hotkeys now work reliably and continuously
- **Debug Logging**: Enhanced debugging for hotkey troubleshooting

---

## 📋 **Complete Feature Set**

### ⌨️ **Global Hotkeys**
| Hotkey | Action | Status |
|--------|--------|--------|
| `Ctrl+Alt+F1` | Start Timer | ✅ **Fixed - Works Continuously** |
| `Ctrl+Alt+F2` | Pause/Resume Timer | ✅ Working |
| `Ctrl+Alt+F3` | Stop & Save Session | ✅ Working |
| `Ctrl+Alt+F4` | Add Note | ✅ Working |
| `Ctrl+Alt+F5` | Show/Hide Overlay | ✅ Working |

### 📊 **Logging System**
- **Automatic Log Creation**: Creates `Chrono-log.txt` next to executable
- **Accurate Duration**: Shows real session time instead of `00:00:00`
- **Session Tracking**: Records start time, end time, duration, and notes
- **Daily Totals**: Calculates total time per day

### 🖥️ **User Interface**
- **System Tray Integration**: Minimal desktop footprint
- **Visual Overlay**: Quick status display with auto-hide
- **Context Menu**: Right-click tray icon for options
- **Balloon Notifications**: User feedback for actions

---

## 🚀 **Installation & Usage**

### **Portable Version (Recommended)**
1. **Download**: `Chronometre-v1.1.1-Portable.zip`
2. **Extract**: Unzip to any folder
3. **Run**: Double-click `Chronometre.exe`
4. **Log File**: Automatically created as `Chrono-log.txt` in same folder

### **System Requirements**
- **OS**: Windows 10 or later
- **Architecture**: x64
- **Runtime**: .NET 8.0 (included)
- **Memory**: 50MB RAM
- **Disk Space**: 100MB

---

## 🔧 **What Was Fixed**

### **Duration Calculation Issue**
```csharp
// Before: Always returned TimeSpan.Zero when idle
if (_currentState == TimerState.Idle)
    return TimeSpan.Zero;

// After: Calculates final duration when idle
if (_currentState == TimerState.Idle)
{
    if (_startTime != DateTime.MinValue)
    {
        var endTime = DateTime.Now;
        var finalElapsed = endTime - _startTime - _pausedDuration;
        return finalElapsed < TimeSpan.Zero ? TimeSpan.Zero : finalElapsed;
    }
    return TimeSpan.Zero;
}
```

### **Start Hotkey Issue**
```csharp
// Before: Showed dialog, breaking hotkey flow
OnStartClicked(null, EventArgs.Empty);

// After: Direct timer start for hotkey usage
if (_timerService?.CurrentState == TimerState.Idle)
{
    _timerService.Start("Hotkey start");
}
```

---

## 📈 **Performance & Reliability**

### **Hotkey System**
- **Polling Interval**: 50ms for responsive key detection
- **Debounce Logic**: Prevents multiple triggers from single key press
- **Error Handling**: Comprehensive exception handling for stability
- **State Tracking**: Proper key state management

### **Logging Performance**
- **Efficient File I/O**: Optimized log writing operations
- **Memory Management**: Smart buffer management for large log files
- **Error Recovery**: Fallback mechanisms for file access issues

---

## 🧪 **Testing Results**

### **Hotkey Testing**
- ✅ **Start Hotkey**: Works continuously (previously failed after first use)
- ✅ **All Other Hotkeys**: Maintained existing functionality
- ✅ **Key Combinations**: All modifier keys (Ctrl+Alt) working correctly
- ✅ **Cross-Application**: Works from any Windows application

### **Duration Testing**
- ✅ **Short Sessions**: 1-5 minutes recorded correctly
- ✅ **Long Sessions**: 1+ hours recorded correctly
- ✅ **Multiple Sessions**: Each session shows accurate duration
- ✅ **Log Format**: Proper timestamp and duration formatting

### **Portable Executable**
- ✅ **Single File**: All dependencies embedded
- ✅ **Log Creation**: Automatic log file creation
- ✅ **Path Resolution**: Works in any directory
- ✅ **No Installation**: Copy and run anywhere

---

## 🐛 **Known Issues**

### **None Critical**
- **Form Warnings**: Some nullable reference warnings (cosmetic only)
- **Assembly Location**: Warning about single-file app compatibility (handled)

### **Resolved Issues**
- ✅ Duration calculation showing 00:00:00
- ✅ Start hotkey only working once
- ✅ Log file path issues
- ✅ Portable executable crashes

---

## 🔄 **Migration from v1.1.0**

### **For Existing Users**
- **Settings**: No migration needed - settings are compatible
- **Log Files**: Existing log files will continue to work
- **Hotkeys**: Same hotkey combinations, now more reliable
- **Installation**: Can replace existing installation

### **For New Users**
- **Download**: Get the portable version
- **Setup**: No installation required
- **Usage**: Start using hotkeys immediately

---

## 📞 **Support & Feedback**

### **Getting Help**
- **GitHub Issues**: [Report bugs or request features](https://github.com/carlcgb/time-tracker/issues)
- **Documentation**: Check README.md for detailed usage instructions
- **Log Files**: Check `Chrono-log.txt` for session history

### **System Requirements**
- **Windows 10+**: Required for global hotkeys
- **.NET 8.0**: Included in portable version
- **x64 Architecture**: Required for optimal performance

---

## 🎉 **What's Next**

### **Planned Features**
- **Custom Hotkeys**: User-configurable hotkey combinations
- **Export Options**: CSV, JSON export formats
- **Statistics Dashboard**: Visual time tracking analytics
- **Cloud Sync**: Optional cloud backup of time logs

### **Community Contributions**
- **Bug Reports**: Help us identify and fix issues
- **Feature Requests**: Suggest new functionality
- **Code Contributions**: Pull requests welcome

---

## 📄 **Technical Details**

### **Build Information**
- **Framework**: .NET 8.0
- **Target**: Windows x64
- **Packaging**: Single-file, self-contained
- **Dependencies**: All included in executable

### **File Structure**
```
portable/
├── Chronometre.exe          # Main application
├── Chrono-log.txt           # Time tracking log
└── [Supporting DLLs]        # .NET runtime libraries
```

---

## 🏆 **Quality Assurance**

### **Testing Coverage**
- ✅ **Hotkey Functionality**: All combinations tested
- ✅ **Duration Calculation**: Various session lengths tested
- ✅ **Log File Operations**: File creation and writing tested
- ✅ **Portable Distribution**: Multiple environments tested
- ✅ **Error Handling**: Exception scenarios tested

### **Code Quality**
- **Error Handling**: Comprehensive try-catch blocks
- **Debug Logging**: Detailed logging for troubleshooting
- **Memory Management**: Proper disposal of resources
- **Performance**: Optimized for minimal resource usage

---

**With ❤️ from CGB**

*Built with .NET 8.0 • Windows Forms • Global Hotkeys • Time Tracking Excellence*
