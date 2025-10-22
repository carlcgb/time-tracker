@echo off
echo Chronometre Uninstaller
echo ======================
echo.

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Running as administrator...
) else (
    echo This uninstaller requires administrator privileges.
    echo Please run this file as administrator.
    pause
    exit /b 1
)

echo.
echo Uninstalling Chronometre...
echo.

REM Run the PowerShell uninstaller
powershell.exe -ExecutionPolicy Bypass -File "%~dp0install.ps1" -Uninstall

echo.
echo Uninstallation complete!
echo.
pause
