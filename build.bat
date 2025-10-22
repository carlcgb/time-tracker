@echo off
echo Chronometre Build Script
echo ========================
echo.

REM Check if PowerShell is available
powershell -Command "Get-Host" >nul 2>&1
if %errorLevel% == 0 (
    echo Running PowerShell build script...
    echo.
    powershell.exe -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*
) else (
    echo PowerShell not available. Please use build.ps1 directly.
    echo.
    pause
    exit /b 1
)

echo.
echo Build completed!
pause
