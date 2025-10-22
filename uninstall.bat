@echo off
echo Chronometre Uninstaller
echo ======================
echo.

REM Check if PowerShell is available
powershell -Command "Get-Host" >nul 2>&1
if %errorLevel% == 0 (
    echo Running PowerShell uninstaller...
    echo.
    powershell.exe -ExecutionPolicy Bypass -File "%~dp0install.ps1" -Uninstall %*
) else (
    echo PowerShell not available. Please use install.ps1 -Uninstall directly.
    echo.
    pause
    exit /b 1
)

echo.
echo Uninstallation completed!
pause
