# Chronomètre Installer
# Version: 1.1.0.0

param(
    [string]$InstallPath = "$env:ProgramFiles\Chronometre",
    [switch]$Force
)

# Check if running as administrator
if (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Error "This installer requires administrator privileges. Please run as administrator."
    exit 1
}

Write-Host "Installing Chronomètre v1.1.0.0..." -ForegroundColor Green

# Create installation directory
if (Test-Path $InstallPath) {
    if ($Force) {
        Write-Host "Removing existing installation..." -ForegroundColor Yellow
        Remove-Item $InstallPath -Recurse -Force
    } else {
        Write-Error "Installation directory already exists. Use -Force to overwrite."
        exit 1
    }
}

New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null
Write-Host "Created installation directory: $InstallPath" -ForegroundColor Green

# Copy application files
Write-Host "Copying application files..." -ForegroundColor Green
Copy-Item ".\Chronometre\*" -Destination $InstallPath -Recurse -Force

# Create desktop shortcut
$DesktopPath = [Environment]::GetFolderPath("Desktop")
$ShortcutPath = Join-Path $DesktopPath "Chronomètre.lnk"
$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut($ShortcutPath)
$Shortcut.TargetPath = Join-Path $InstallPath "Chronometre.exe"
$Shortcut.WorkingDirectory = $InstallPath
$Shortcut.Description = "Chronomètre Time Tracker"
$Shortcut.Save()
Write-Host "Created desktop shortcut" -ForegroundColor Green

# Create start menu shortcut
$StartMenuPath = [Environment]::GetFolderPath("StartMenu")
$StartMenuShortcutPath = Join-Path $StartMenuPath "Chronomètre.lnk"
$StartMenuShortcut = $WshShell.CreateShortcut($StartMenuShortcutPath)
$StartMenuShortcut.TargetPath = Join-Path $InstallPath "Chronometre.exe"
$StartMenuShortcut.WorkingDirectory = $InstallPath
$StartMenuShortcut.Description = "Chronomètre Time Tracker"
$StartMenuShortcut.Save()
Write-Host "Created start menu shortcut" -ForegroundColor Green

# Register in Add/Remove Programs
$RegistryPath = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Chronometre"
New-Item -Path $RegistryPath -Force | Out-Null
Set-ItemProperty -Path $RegistryPath -Name "DisplayName" -Value "Chronomètre"
Set-ItemProperty -Path $RegistryPath -Name "DisplayVersion" -Value "1.1.0.0"
Set-ItemProperty -Path $RegistryPath -Name "Publisher" -Value "Dev-NTIC"
Set-ItemProperty -Path $RegistryPath -Name "InstallLocation" -Value $InstallPath
Set-ItemProperty -Path $RegistryPath -Name "DisplayIcon" -Value (Join-Path $InstallPath "Chronometre.exe")
Write-Host "Registered in Add/Remove Programs" -ForegroundColor Green

Write-Host "
Chronomètre v1.1.0.0 has been successfully installed!" -ForegroundColor Green
Write-Host "Installation path: $InstallPath" -ForegroundColor Cyan
Write-Host "Desktop shortcut created" -ForegroundColor Cyan
Write-Host "Start menu shortcut created" -ForegroundColor Cyan
Write-Host "
You can now run Chronomètre from the desktop or start menu." -ForegroundColor Yellow
