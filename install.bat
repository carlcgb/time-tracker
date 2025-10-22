@echo off
echo Chronometre Installer
echo ====================
echo.

REM Check if PowerShell is available
powershell -Command "Get-Host" >nul 2>&1
if %errorLevel% == 0 (
    echo Running PowerShell installer...
    echo.
    powershell.exe -ExecutionPolicy Bypass -File "%~dp0install.ps1" %*
) else (
    echo PowerShell not available. Please use install.ps1 directly.
    echo.
    pause
    exit /b 1
)

echo.
echo Installation completed!
pause
