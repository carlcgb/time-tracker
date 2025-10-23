# 🏗️ Chronomètre Development Guide

> **Complete guide for developers working on Chronomètre**

## 🚀 Getting Started

### Prerequisites

- **Visual Studio 2022** or **VS Code** with C# extension
- **.NET 8.0 SDK** (latest version)
- **Windows 10/11** for development and testing
- **Git** for version control

### Clone the Repository

```bash
git clone https://github.com/carlcgb/time-tracker.git
cd time-tracker
```

### Build the Application

```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release

# Run the application
dotnet run
```

## 📁 Project Structure

```
Chronometre/
├── Forms/                          # Windows Forms UI
│   ├── StartDialog.cs             # Timer start dialog
│   ├── NoteDialog.cs              # Note input dialog
│   └── OverlayIndicatorForm.cs    # Status overlay
├── Services/                       # Core business logic
│   ├── TimerService.cs            # Timer logic and state management
│   ├── LogWriter.cs               # Logging functionality
│   ├── ReliableHotkeyManager.cs   # Global hotkey management
│   ├── Settings.cs                # Configuration management
│   └── OverlayAutoHideController.cs # Overlay behavior
├── Program.cs                      # Application entry point
├── Chronometre.csproj             # Project configuration
├── timer-icon.ico                 # Application icon
├── README.md                      # Quick start guide
├── USER_GUIDE.md                  # Detailed user instructions
├── DEVELOPMENT.md                 # This file
└── RELEASE_NOTES_v1.1.3.md       # Release documentation
```

## 🏗️ Architecture Overview

### Core Components

#### **Program.cs** - Application Entry Point
- Single instance enforcement
- Service initialization
- System tray setup
- Hotkey registration
- Application lifecycle management

#### **TimerService.cs** - Timer Logic
- Session state management (Idle, Running, Paused)
- Duration calculation with pause handling
- Event notifications for state changes
- Session data collection

#### **LogWriter.cs** - Logging System
- Robust path detection with multiple fallback methods
- Write permission testing
- Automatic directory creation
- Error handling and retry logic
- Tab-separated log format

#### **ReliableHotkeyManager.cs** - Global Hotkeys
- Windows API integration for global hotkeys
- Keyboard hook management
- Hotkey conflict detection
- Cross-application hotkey support

#### **OverlayIndicatorForm.cs** - Visual Overlay
- Transparent overlay window
- Auto-hide functionality
- Fade in/out animations
- Status display (running/paused/elapsed time)

### Data Flow

```
User Action → Hotkey → TimerService → LogWriter → File System
     ↓
System Tray ← Overlay ← TimerService ← State Change
```

## 🔧 Development Setup

### Visual Studio 2022

1. **Open Solution**: Open `Chronometre.csproj` in Visual Studio
2. **Restore Packages**: NuGet packages will restore automatically
3. **Build**: Press F6 or Build → Build Solution
4. **Run**: Press F5 or Debug → Start Debugging

### VS Code

1. **Open Folder**: Open the project folder in VS Code
2. **Install Extensions**: C# extension for VS Code
3. **Restore**: Run `dotnet restore` in terminal
4. **Build**: Run `dotnet build` in terminal
5. **Run**: Run `dotnet run` in terminal

### Command Line Development

```bash
# Restore packages
dotnet restore

# Build debug version
dotnet build

# Build release version
dotnet build -c Release

# Run the application
dotnet run

# Create portable release
dotnet publish -c Release -r win-x64 --self-contained true -o release
```

## 🧪 Testing

### Manual Testing

1. **Hotkey Testing**: Test all hotkey combinations
2. **State Transitions**: Test all timer state changes
3. **Logging**: Verify log file creation and writing
4. **Overlay**: Test overlay display and auto-hide
5. **System Tray**: Test tray icon and menu functionality

### Test Scenarios

#### **Basic Functionality**
- Start timer with notes
- Add notes during session
- Pause and resume timer
- Stop timer and verify log entry
- Check log file format

#### **Edge Cases**
- Multiple rapid hotkey presses
- Long-running sessions
- System sleep/wake cycles
- Low disk space scenarios
- Permission denied scenarios

#### **Integration Testing**
- Hotkeys from different applications
- System tray icon states
- Overlay display timing
- Log file path resolution

## 📦 Building Releases

### Debug Build

```bash
dotnet build -c Debug
```

### Release Build

```bash
dotnet build -c Release
```

### Portable Release

```bash
dotnet publish -c Release -r win-x64 --self-contained true -o release
```

### Creating Release Package

```bash
# Create portable zip
Compress-Archive -Path "release\*" -DestinationPath "Chronometre-v1.1.3-Portable.zip"
```

## 🔧 Configuration

### Project Settings (Chronometre.csproj)

```xml
<PropertyGroup>
  <OutputType>WinExe</OutputType>
  <TargetFramework>net8.0-windows</TargetFramework>
  <UseWindowsForms>true</UseWindowsForms>
  <AssemblyTitle>Chronomètre</AssemblyTitle>
  <AssemblyVersion>1.1.3.0</AssemblyVersion>
  <FileVersion>1.1.3.0</FileVersion>
  <SelfContained>true</SelfContained>
  <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  <ApplicationIcon>timer-icon.ico</ApplicationIcon>
</PropertyGroup>
```

### Build Scripts

#### **build.bat** - Windows Batch Script
```batch
@echo off
echo Building Chronomètre...
dotnet build -c Release
echo Build complete!
pause
```

#### **build.ps1** - PowerShell Script
```powershell
Write-Host "Building Chronomètre..." -ForegroundColor Green
dotnet build -c Release
Write-Host "Build complete!" -ForegroundColor Green
```

## 🐛 Debugging

### Debug Output

The application uses `System.Diagnostics.Debug.WriteLine()` for debug output:

```csharp
System.Diagnostics.Debug.WriteLine($"LogWriter initialized with path: {_logFilePath}");
```

### Common Debug Scenarios

#### **Hotkey Issues**
- Check if hotkeys are registered
- Verify no conflicts with other applications
- Test hotkey response in different applications

#### **Logging Issues**
- Check path resolution logic
- Verify write permissions
- Test fallback mechanisms

#### **Overlay Issues**
- Check overlay positioning
- Verify auto-hide timing
- Test fade animations

### Debug Tools

- **Visual Studio Debugger**: Step through code execution
- **Process Monitor**: Monitor file system access
- **Event Viewer**: Check Windows event logs
- **Debug Output**: Use DebugView to see debug messages

## 📝 Code Style

### Naming Conventions

- **Classes**: PascalCase (e.g., `TimerService`)
- **Methods**: PascalCase (e.g., `StartTimer()`)
- **Properties**: PascalCase (e.g., `CurrentState`)
- **Fields**: camelCase with underscore (e.g., `_currentState`)
- **Constants**: PascalCase (e.g., `MaxRetries`)

### Code Organization

- **Single Responsibility**: Each class has one clear purpose
- **Dependency Injection**: Services are injected where needed
- **Event-Driven**: Use events for loose coupling
- **Error Handling**: Comprehensive try-catch blocks
- **Resource Management**: Proper disposal of resources

### Documentation

- **XML Comments**: Document public APIs
- **Inline Comments**: Explain complex logic
- **README Updates**: Keep documentation current
- **Release Notes**: Document all changes

## 🚀 Deployment

### Release Process

1. **Update Version**: Update version in `.csproj` file
2. **Build Release**: Create release build
3. **Test Thoroughly**: Run comprehensive tests
4. **Create Package**: Build portable release
5. **Update Documentation**: Update README and guides
6. **Create Tag**: Create git tag for release
7. **Push Changes**: Push to GitHub
8. **Create Release**: Create GitHub release with assets

### Version Management

- **Semantic Versioning**: Major.Minor.Patch (e.g., 1.1.3)
- **Assembly Version**: Update in `.csproj` file
- **Git Tags**: Create tags for each release
- **Release Notes**: Document changes in release notes

## 🤝 Contributing

### Development Workflow

1. **Fork Repository**: Fork the repository on GitHub
2. **Create Branch**: Create feature branch from main
3. **Make Changes**: Implement your changes
4. **Test Thoroughly**: Test all functionality
5. **Update Documentation**: Update relevant documentation
6. **Commit Changes**: Commit with descriptive messages
7. **Push Branch**: Push branch to your fork
8. **Create PR**: Create pull request to main branch

### Pull Request Guidelines

- **Clear Description**: Describe what the PR does
- **Testing**: Include testing information
- **Documentation**: Update documentation if needed
- **Code Review**: Address review feedback
- **Merge**: Merge after approval

### Code Review Checklist

- [ ] Code follows style guidelines
- [ ] No breaking changes
- [ ] Tests pass
- [ ] Documentation updated
- [ ] Error handling included
- [ ] Resource cleanup
- [ ] Performance considerations

## 📚 Resources

### Documentation

- **[README.md](README.md)** - Quick start guide
- **[USER_GUIDE.md](USER_GUIDE.md)** - Detailed user instructions
- **[RELEASE_NOTES_v1.1.3.md](RELEASE_NOTES_v1.1.3.md)** - Release documentation

### External Resources

- **[.NET 8.0 Documentation](https://docs.microsoft.com/en-us/dotnet/)**
- **[Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)**
- **[Global Hotkeys in .NET](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys)**
- **[System Tray Integration](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon)**

### Tools

- **Visual Studio 2022**: Primary IDE
- **VS Code**: Alternative IDE
- **Git**: Version control
- **GitHub**: Repository hosting
- **.NET CLI**: Command line tools

---

**Happy coding!** 🚀

For questions or support, check the [GitHub Issues](https://github.com/carlcgb/time-tracker/issues) or contact [carl@vrille.io](mailto:carl@vrille.io).
