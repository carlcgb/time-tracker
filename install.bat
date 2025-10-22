@echo off
echo Chronometre Installer
echo ====================
echo.

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Running as administrator...
) else (
    echo This installer requires administrator privileges.
    echo Please run this file as administrator.
    pause
    exit /b 1
)

echo.
echo Installing Chronometre...
echo.

REM Run the PowerShell installer
powershell.exe -ExecutionPolicy Bypass -File "%~dp0install.ps1"

echo.
echo Installation complete!
echo.
pause
