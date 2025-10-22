# Chronomètre Build Script
# Builds the application for Windows x64 with all dependencies included

param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [switch]$Clean = $false,
    [switch]$Help = $false
)

if ($Help) {
    Write-Host "Chronomètre Build Script" -ForegroundColor Green
    Write-Host ""
    Write-Host "Usage: .\build.ps1 [options]" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Options:" -ForegroundColor Yellow
    Write-Host "  -Configuration <config>  Build configuration (Debug|Release) [Default: Release]" -ForegroundColor White
    Write-Host "  -Runtime <runtime>       Target runtime (win-x64|win-x86|win-arm64) [Default: win-x64]" -ForegroundColor White
    Write-Host "  -Clean                   Clean before building" -ForegroundColor White
    Write-Host "  -Help                    Show this help message" -ForegroundColor White
    Write-Host ""
    Write-Host "Examples:" -ForegroundColor Yellow
    Write-Host "  .\build.ps1                    # Build Release for win-x64" -ForegroundColor White
    Write-Host "  .\build.ps1 -Clean             # Clean and build" -ForegroundColor White
    Write-Host "  .\build.ps1 -Configuration Debug  # Build Debug version" -ForegroundColor White
    exit 0
}

Write-Host "Chronomètre Build Script" -ForegroundColor Green
Write-Host "=========================" -ForegroundColor Green
Write-Host ""

# Check if .NET 8 SDK is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "Using .NET SDK version: $dotnetVersion" -ForegroundColor Cyan
} catch {
    Write-Host "Error: .NET 8 SDK not found. Please install .NET 8 SDK from https://dotnet.microsoft.com/download" -ForegroundColor Red
    exit 1
}

# Clean if requested
if ($Clean) {
    Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
    dotnet clean -c $Configuration
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Clean failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "Clean completed." -ForegroundColor Green
    Write-Host ""
}

# Build and publish
Write-Host "Building Chronomètre..." -ForegroundColor Yellow
Write-Host "Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "Runtime: $Runtime" -ForegroundColor Cyan
Write-Host ""

$publishArgs = @(
    "publish"
    "-c", $Configuration
    "-r", $Runtime
    "--self-contained", "true"
    "-p:PublishSingleFile=true"
    "-p:PublishTrimmed=false"
    "-p:IncludeNativeLibrariesForSelfExtract=true"
    "-p:EnableCompressionInSingleFile=true"
)

Write-Host "Running: dotnet $($publishArgs -join ' ')" -ForegroundColor Gray
dotnet @publishArgs

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Build completed successfully!" -ForegroundColor Green
Write-Host ""

# Show output location
$outputPath = "bin\$Configuration\net8.0-windows\$Runtime\publish"
$exePath = Join-Path $outputPath "Chronometre.exe"

if (Test-Path $exePath) {
    $fileInfo = Get-Item $exePath
    Write-Host "Output: $exePath" -ForegroundColor Cyan
    Write-Host "Size: $([math]::Round($fileInfo.Length / 1MB, 2)) MB" -ForegroundColor Cyan
    Write-Host "Modified: $($fileInfo.LastWriteTime)" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "You can now run the application from the publish folder." -ForegroundColor Green
} else {
    Write-Host "Warning: Executable not found at expected location." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Build script completed." -ForegroundColor Green