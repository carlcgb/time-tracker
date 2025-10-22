# Chronomètre Installer Script
# This script installs the Chronomètre time tracker application

param(
    [string]$InstallPath = "$env:ProgramFiles\Chronometre",
    [switch]$Uninstall
)

# Check if running as administrator
function Test-Administrator {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

# Create installation directory
function Install-Application {
    Write-Host "Installing Chronomètre..." -ForegroundColor Green
    
    # Create installation directory
    if (!(Test-Path $InstallPath)) {
        New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null
        Write-Host "Created installation directory: $InstallPath" -ForegroundColor Yellow
    }
    
    # Copy application files
    $sourcePath = Join-Path $PSScriptRoot "installer-build"
    if (Test-Path $sourcePath) {
        Copy-Item -Path "$sourcePath\*" -Destination $InstallPath -Recurse -Force
        Write-Host "Copied application files to: $InstallPath" -ForegroundColor Yellow
    } else {
        Write-Error "Source directory not found: $sourcePath"
        return
    }
    
    # Create desktop shortcut
    $desktopPath = [Environment]::GetFolderPath("Desktop")
    $shortcutPath = Join-Path $desktopPath "Chronomètre.lnk"
    $targetPath = Join-Path $InstallPath "Chronometre.exe"
    
    $WshShell = New-Object -comObject WScript.Shell
    $Shortcut = $WshShell.CreateShortcut($shortcutPath)
    $Shortcut.TargetPath = $targetPath
    $Shortcut.WorkingDirectory = $InstallPath
    $Shortcut.Description = "Chronomètre Time Tracker"
    $Shortcut.Save()
    Write-Host "Created desktop shortcut" -ForegroundColor Yellow
    
    # Create Start Menu shortcut
    $startMenuPath = [Environment]::GetFolderPath("StartMenu")
    $startMenuShortcut = Join-Path $startMenuPath "Programs\Chronomètre.lnk"
    $startMenuDir = Split-Path $startMenuShortcut -Parent
    if (!(Test-Path $startMenuDir)) {
        New-Item -ItemType Directory -Path $startMenuDir -Force | Out-Null
    }
    
    $StartMenuShortcut = $WshShell.CreateShortcut($startMenuShortcut)
    $StartMenuShortcut.TargetPath = $targetPath
    $StartMenuShortcut.WorkingDirectory = $InstallPath
    $StartMenuShortcut.Description = "Chronomètre Time Tracker"
    $StartMenuShortcut.Save()
    Write-Host "Created Start Menu shortcut" -ForegroundColor Yellow
    
    # Create uninstaller
    $uninstallerPath = Join-Path $InstallPath "uninstall.ps1"
    $uninstallerContent = @"
# Chronomètre Uninstaller
Write-Host "Uninstalling Chronomètre..." -ForegroundColor Red

# Remove desktop shortcut
`$desktopPath = [Environment]::GetFolderPath("Desktop")
`$shortcutPath = Join-Path `$desktopPath "Chronomètre.lnk"
if (Test-Path `$shortcutPath) {
    Remove-Item `$shortcutPath -Force
    Write-Host "Removed desktop shortcut" -ForegroundColor Yellow
}

# Remove Start Menu shortcut
`$startMenuPath = [Environment]::GetFolderPath("StartMenu")
`$startMenuShortcut = Join-Path `$startMenuPath "Programs\Chronomètre.lnk"
if (Test-Path `$startMenuShortcut) {
    Remove-Item `$startMenuShortcut -Force
    Write-Host "Removed Start Menu shortcut" -ForegroundColor Yellow
}

# Remove application directory
if (Test-Path "$InstallPath") {
    Remove-Item -Path "$InstallPath" -Recurse -Force
    Write-Host "Removed application directory" -ForegroundColor Yellow
}

Write-Host "Chronomètre has been uninstalled." -ForegroundColor Green
"@
    
    Set-Content -Path $uninstallerPath -Value $uninstallerContent
    Write-Host "Created uninstaller: $uninstallerPath" -ForegroundColor Yellow
    
    # Register uninstaller in Add/Remove Programs
    $registryPath = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Chronometre"
    New-Item -Path $registryPath -Force | Out-Null
    Set-ItemProperty -Path $registryPath -Name "DisplayName" -Value "Chronomètre Time Tracker"
    Set-ItemProperty -Path $registryPath -Name "DisplayVersion" -Value "1.1.0.0"
    Set-ItemProperty -Path $registryPath -Name "Publisher" -Value "Dev-NTIC"
    Set-ItemProperty -Path $registryPath -Name "InstallLocation" -Value $InstallPath
    Set-ItemProperty -Path $registryPath -Name "UninstallString" -Value "powershell.exe -ExecutionPolicy Bypass -File `"$uninstallerPath`""
    Set-ItemProperty -Path $registryPath -Name "NoModify" -Value 1
    Set-ItemProperty -Path $registryPath -Name "NoRepair" -Value 1
    Write-Host "Registered uninstaller in Add/Remove Programs" -ForegroundColor Yellow
    
    Write-Host "`nChronomètre has been successfully installed!" -ForegroundColor Green
    Write-Host "Installation location: $InstallPath" -ForegroundColor Cyan
    Write-Host "Desktop shortcut created" -ForegroundColor Cyan
    Write-Host "Start Menu shortcut created" -ForegroundColor Cyan
    Write-Host "`nYou can now run Chronomètre from the desktop shortcut or Start Menu." -ForegroundColor Green
}

# Uninstall application
function Uninstall-Application {
    Write-Host "Uninstalling Chronomètre..." -ForegroundColor Red
    
    # Remove desktop shortcut
    $desktopPath = [Environment]::GetFolderPath("Desktop")
    $shortcutPath = Join-Path $desktopPath "Chronomètre.lnk"
    if (Test-Path $shortcutPath) {
        Remove-Item $shortcutPath -Force
        Write-Host "Removed desktop shortcut" -ForegroundColor Yellow
    }
    
    # Remove Start Menu shortcut
    $startMenuPath = [Environment]::GetFolderPath("StartMenu")
    $startMenuShortcut = Join-Path $startMenuPath "Programs\Chronomètre.lnk"
    if (Test-Path $startMenuShortcut) {
        Remove-Item $startMenuShortcut -Force
        Write-Host "Removed Start Menu shortcut" -ForegroundColor Yellow
    }
    
    # Remove application directory
    if (Test-Path $InstallPath) {
        Remove-Item -Path $InstallPath -Recurse -Force
        Write-Host "Removed application directory" -ForegroundColor Yellow
    }
    
    # Remove from Add/Remove Programs
    $registryPath = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Chronometre"
    if (Test-Path $registryPath) {
        Remove-Item -Path $registryPath -Recurse -Force
        Write-Host "Removed from Add/Remove Programs" -ForegroundColor Yellow
    }
    
    Write-Host "Chronomètre has been uninstalled." -ForegroundColor Green
}

# Main execution
if ($Uninstall) {
    if (!(Test-Administrator)) {
        Write-Error "Administrator privileges required for uninstallation."
        exit 1
    }
    Uninstall-Application
} else {
    if (!(Test-Administrator)) {
        Write-Error "Administrator privileges required for installation."
        Write-Host "Please run PowerShell as Administrator and try again." -ForegroundColor Red
        exit 1
    }
    Install-Application
}
