@echo off
REM Chronomètre Build Script
REM Builds the application for Windows x64 with all dependencies included

setlocal enabledelayedexpansion

REM Default values
set CONFIGURATION=Release
set RUNTIME=win-x64
set CLEAN=false

REM Parse command line arguments
:parse_args
if "%~1"=="" goto :build
if "%~1"=="-c" (
    set CONFIGURATION=%~2
    shift
    shift
    goto :parse_args
)
if "%~1"=="-r" (
    set RUNTIME=%~2
    shift
    shift
    goto :parse_args
)
if "%~1"=="-clean" (
    set CLEAN=true
    shift
    goto :parse_args
)
if "%~1"=="-h" goto :help
if "%~1"=="--help" goto :help
if "%~1"=="-?" goto :help
shift
goto :parse_args

:help
echo Chronomètre Build Script
echo.
echo Usage: build.bat [options]
echo.
echo Options:
echo   -c ^<config^>     Build configuration (Debug^|Release) [Default: Release]
echo   -r ^<runtime^>    Target runtime (win-x64^|win-x86^|win-arm64) [Default: win-x64]
echo   -clean           Clean before building
echo   -h, --help, -?   Show this help message
echo.
echo Examples:
echo   build.bat                    # Build Release for win-x64
echo   build.bat -clean             # Clean and build
echo   build.bat -c Debug           # Build Debug version
goto :end

:build
echo Chronomètre Build Script
echo =========================
echo.

REM Check if .NET 8 SDK is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo Error: .NET 8 SDK not found. Please install .NET 8 SDK from https://dotnet.microsoft.com/download
    exit /b 1
)

for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo Using .NET SDK version: !DOTNET_VERSION!
echo.

REM Clean if requested
if "%CLEAN%"=="true" (
    echo Cleaning previous builds...
    dotnet clean -c %CONFIGURATION%
    if errorlevel 1 (
        echo Clean failed!
        exit /b 1
    )
    echo Clean completed.
    echo.
)

REM Build and publish
echo Building Chronomètre...
echo Configuration: %CONFIGURATION%
echo Runtime: %RUNTIME%
echo.

dotnet publish -c %CONFIGURATION% -r %RUNTIME% --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

if errorlevel 1 (
    echo Build failed!
    exit /b 1
)

echo.
echo Build completed successfully!
echo.

REM Show output location
set OUTPUT_PATH=bin\%CONFIGURATION%\net8.0-windows\%RUNTIME%\publish
set EXE_PATH=%OUTPUT_PATH%\Chronometre.exe

if exist "%EXE_PATH%" (
    echo Output: %EXE_PATH%
    for %%F in ("%EXE_PATH%") do echo Size: %%~zF bytes
    echo.
    echo You can now run the application from the publish folder.
) else (
    echo Warning: Executable not found at expected location.
)

echo.
echo Build script completed.

:end
endlocal