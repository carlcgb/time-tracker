# Chronomètre Uninstaller
Write-Host "Uninstalling Chronomètre..." -ForegroundColor Yellow

# Remove shortcuts
$DesktopPath = [Environment]::GetFolderPath("Desktop")
$DesktopShortcut = Join-Path $DesktopPath "Chronomètre.lnk"
if (Test-Path $DesktopShortcut) {
    Remove-Item $DesktopShortcut -Force
    Write-Host "Removed desktop shortcut" -ForegroundColor Green
}

$StartMenuPath = [Environment]::GetFolderPath("StartMenu")
$StartMenuShortcut = Join-Path $StartMenuPath "Chronomètre.lnk"
if (Test-Path $StartMenuShortcut) {
    Remove-Item $StartMenuShortcut -Force
    Write-Host "Removed start menu shortcut" -ForegroundColor Green
}

# Remove from Add/Remove Programs
$RegistryPath = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Chronometre"
if (Test-Path $RegistryPath) {
    Remove-Item -Path $RegistryPath -Recurse -Force
    Write-Host "Removed from Add/Remove Programs" -ForegroundColor Green
}

# Remove installation directory
$InstallPath = "$env:ProgramFiles\Chronometre"
if (Test-Path $InstallPath) {
    Remove-Item $InstallPath -Recurse -Force
    Write-Host "Removed installation directory" -ForegroundColor Green
}

Write-Host "Chronomètre has been uninstalled." -ForegroundColor Green
