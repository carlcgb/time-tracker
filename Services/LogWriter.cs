using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronometre.Services
{
    public class LogWriter
    {
        private readonly string _logFilePath;
        private const int MaxReadSize = 10 * 1024 * 1024; // 10MB max read for daily total calculation

        public string LogFilePath => _logFilePath;

        public LogWriter(string? customLogPath = null)
        {
            _logFilePath = GetLogFilePath(customLogPath);
            System.Diagnostics.Debug.WriteLine($"LogWriter initialized with path: {_logFilePath}");
            EnsureLogDirectoryExists();
            
            // Write a startup entry to ensure the log file is created
            WriteStartupEntry();
            
            // Show user where the log file is located
            ShowLogLocationNotification();
        }

        private string GetLogFilePath(string? customLogPath)
        {
            if (!string.IsNullOrEmpty(customLogPath))
            {
                // If custom path is provided, ensure it has the correct filename
                var directory = Path.GetDirectoryName(customLogPath);
                if (string.IsNullOrEmpty(directory))
                {
                    directory = customLogPath;
                }
                var fileName = Path.GetFileName(customLogPath);
                if (string.IsNullOrEmpty(fileName) || !fileName.EndsWith(".txt"))
                {
                    fileName = "Chrono-log.txt";
                }
                var fullPath = Path.Combine(directory, fileName);
                System.Diagnostics.Debug.WriteLine($"Using custom log path: {fullPath}");
                return fullPath;
            }

            // Try multiple methods to find the executable directory
            string executableDirectory;
            
            // Method 1: Use AppContext.BaseDirectory
            try
            {
                executableDirectory = AppContext.BaseDirectory;
                if (!string.IsNullOrEmpty(executableDirectory) && Directory.Exists(executableDirectory))
                {
                    // Test if we can write to this directory
                    var testPath = Path.Combine(executableDirectory, "test_write.tmp");
                    try
                    {
                        File.WriteAllText(testPath, "test");
                        File.Delete(testPath);
                        System.Diagnostics.Debug.WriteLine($"Using AppContext.BaseDirectory: {executableDirectory}");
                        var logPath = Path.Combine(executableDirectory, "Chrono-log.txt");
                        return logPath;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Cannot write to AppContext.BaseDirectory {executableDirectory}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AppContext.BaseDirectory failed: {ex.Message}");
            }

            // Method 2: Use Assembly.GetExecutingAssembly().Location
            try
            {
                var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                executableDirectory = Path.GetDirectoryName(assemblyLocation) ?? string.Empty;
                if (!string.IsNullOrEmpty(executableDirectory) && Directory.Exists(executableDirectory))
                {
                    // Test if we can write to this directory
                    var testPath = Path.Combine(executableDirectory, "test_write.tmp");
                    try
                    {
                        File.WriteAllText(testPath, "test");
                        File.Delete(testPath);
                        System.Diagnostics.Debug.WriteLine($"Using Assembly location directory: {executableDirectory}");
                        var logPath = Path.Combine(executableDirectory, "Chrono-log.txt");
                        return logPath;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Cannot write to Assembly location {executableDirectory}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Assembly location failed: {ex.Message}");
            }

            // Method 3: Use Environment.CurrentDirectory
            try
            {
                executableDirectory = Environment.CurrentDirectory;
                if (!string.IsNullOrEmpty(executableDirectory) && Directory.Exists(executableDirectory))
                {
                    // Test if we can write to this directory
                    var testPath = Path.Combine(executableDirectory, "test_write.tmp");
                    try
                    {
                        File.WriteAllText(testPath, "test");
                        File.Delete(testPath);
                        System.Diagnostics.Debug.WriteLine($"Using CurrentDirectory: {executableDirectory}");
                        var logPath = Path.Combine(executableDirectory, "Chrono-log.txt");
                        return logPath;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Cannot write to CurrentDirectory {executableDirectory}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CurrentDirectory failed: {ex.Message}");
            }

            // Fallback: Use Documents folder
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fallbackPath = Path.Combine(documentsPath, "Chronometre");
            System.Diagnostics.Debug.WriteLine($"Using fallback path: {fallbackPath}");
            return Path.Combine(fallbackPath, "Chrono-log.txt");
        }

        private bool CanWriteToPath(string path)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Test if we can actually write to the file
                File.WriteAllText(path, "# Test write\n");
                File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Cannot write to path {path}: {ex.Message}");
                return false;
            }
        }

        private void EnsureLogDirectoryExists()
        {
            try
            {
                var directory = Path.GetDirectoryName(_logFilePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Create the log file if it doesn't exist
                if (!File.Exists(_logFilePath))
                {
                    File.WriteAllText(_logFilePath, "# Chronomètre Time Tracker Log - Chrono-log.txt\n# Format: Date\tStartTime\tEndTime\tDuration\tNotes\n# All sessions are aggregated in this single file\n\n", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                // Log error to a fallback location
                var fallbackPath = Path.Combine(Path.GetTempPath(), "Chronometre_error.log");
                File.AppendAllText(fallbackPath, $"{DateTime.Now}: Failed to create log directory: {ex.Message}\n");
            }
        }

        private void WriteStartupEntry()
        {
            try
            {
                var now = DateTime.Now;
                var startupEntry = $"{now:yyyy-MM-dd}\t{now:HH:mm:ss}\tN/A\t00:00:00\tApplication started\n";
                System.Diagnostics.Debug.WriteLine($"Writing startup entry to: {_logFilePath}");
                File.AppendAllText(_logFilePath, startupEntry);
                System.Diagnostics.Debug.WriteLine("Startup entry written successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error writing startup entry: {ex.Message}");
                // Log error to a fallback location
                var fallbackPath = Path.Combine(Path.GetTempPath(), "Chronometre_error.log");
                File.AppendAllText(fallbackPath, $"{DateTime.Now}: Failed to write startup entry: {ex.Message}\n");
            }
        }

        private void ShowLogLocationNotification()
        {
            try
            {
                // Show a brief notification about where the log file is located
                var logLocation = Path.GetDirectoryName(_logFilePath);
                var logFileName = Path.GetFileName(_logFilePath);
                
                System.Diagnostics.Debug.WriteLine($"Log file location: {logLocation}\\{logFileName}");
                
                // If it's not on Desktop, show a notification
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (!_logFilePath.StartsWith(desktopPath, StringComparison.OrdinalIgnoreCase))
                {
                    // Show a system tray notification about log location
                    if (System.Windows.Forms.Application.OpenForms.Count > 0)
                    {
                        var mainForm = System.Windows.Forms.Application.OpenForms[0];
                        if (mainForm != null)
                        {
                            // This will be handled by the main application
                            System.Diagnostics.Debug.WriteLine($"Log file saved to: {logLocation}\\{logFileName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing log location notification: {ex.Message}");
            }
        }

        public void WriteSession(SessionData sessionData)
        {
            try
            {
                // Always write session data, even if idle (for debugging)
                var dailyTotal = CalculateDailyTotal(sessionData.StartTime.Date);
                var logLine = FormatLogLine(sessionData, dailyTotal);

                WriteToLogFile(logLine);
                
                // Also write to debug output
                System.Diagnostics.Debug.WriteLine($"Writing session: {logLine}");
            }
            catch (Exception ex)
            {
                // Log error to a fallback location
                var fallbackPath = Path.Combine(Path.GetTempPath(), "Chronometre_error.log");
                File.AppendAllText(fallbackPath, $"{DateTime.Now}: Failed to write session: {ex.Message}\n");
            }
        }

        private string FormatLogLine(SessionData sessionData, TimeSpan dailyTotal)
        {
            var date = sessionData.StartTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var startTime = sessionData.StartTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            var endTime = sessionData.EndTime?.ToString("HH:mm:ss", CultureInfo.InvariantCulture) ?? "N/A";
            var duration = sessionData.Duration.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            var notes = FormatNotes(sessionData.Notes);

            return $"{date}\t{startTime}\t{endTime}\t{duration}\t{notes}";
        }

        private string FormatNotes(List<string> notes)
        {
            if (notes == null || notes.Count == 0)
                return string.Empty;

            var combined = string.Join("; ", notes);
            // Escape quotes by doubling them
            return combined.Replace("\"", "\"\"");
        }

        private string GetApplicationVersion()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return version?.ToString(3) ?? "1.0.0";
        }

        private TimeSpan CalculateDailyTotal(DateTime date)
        {
            if (!File.Exists(_logFilePath))
                return TimeSpan.Zero;

            try
            {
                var dailyTotal = TimeSpan.Zero;
                var lines = ReadLogLinesFromEnd();

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var sessionDate = ExtractSessionDate(line);
                    if (sessionDate.Date != date.Date)
                        break; // Different date, stop reading

                    var duration = ExtractDuration(line);
                    dailyTotal = dailyTotal.Add(duration);
                }

                return dailyTotal;
            }
            catch (Exception)
            {
                // If we can't calculate the daily total, return zero
                return TimeSpan.Zero;
            }
        }

        private List<string> ReadLogLinesFromEnd()
        {
            var lines = new List<string>();
            
            if (!File.Exists(_logFilePath))
                return lines;

            try
            {
                var fileInfo = new FileInfo(_logFilePath);
                var readSize = Math.Min(fileInfo.Length, MaxReadSize);
                var buffer = new byte[readSize];
                
                using var stream = File.OpenRead(_logFilePath);
                stream.Seek(-readSize, SeekOrigin.End);
                stream.Read(buffer, 0, (int)readSize);
                
                var content = Encoding.UTF8.GetString(buffer);
                var allLines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                
                // Return lines in reverse order (newest first)
                for (int i = allLines.Length - 1; i >= 0; i--)
                {
                    lines.Add(allLines[i].Trim());
                }
            }
            catch
            {
                // If we can't read from the end, try reading the whole file
                try
                {
                    var allLines = File.ReadAllLines(_logFilePath);
                    for (int i = allLines.Length - 1; i >= 0; i--)
                    {
                        lines.Add(allLines[i].Trim());
                    }
                }
                catch
                {
                    // If all else fails, return empty list
                }
            }

            return lines;
        }

        private DateTime ExtractSessionDate(string logLine)
        {
            try
            {
                // Extract date from timestamp at the beginning of the line
                var match = Regex.Match(logLine, @"^(\d{4}-\d{2}-\d{2})");
                if (match.Success)
                {
                    return DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                // If parsing fails, return a date far in the past
            }

            return DateTime.MinValue;
        }

        private TimeSpan ExtractDuration(string logLine)
        {
            try
            {
                // Extract duration from "Duration=HH:mm:ss" pattern
                var match = Regex.Match(logLine, @"Duration=(\d{2}:\d{2}:\d{2})");
                if (match.Success)
                {
                    return TimeSpan.ParseExact(match.Groups[1].Value, @"hh\:mm\:ss", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                // If parsing fails, return zero duration
            }

            return TimeSpan.Zero;
        }

        private void WriteToLogFile(string logLine)
        {
            const int maxRetries = 3;
            var retryDelay = TimeSpan.FromMilliseconds(100);

            System.Diagnostics.Debug.WriteLine($"Attempting to write to log file: {_logFilePath}");
            System.Diagnostics.Debug.WriteLine($"Log line to write: {logLine}");

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    // Ensure directory exists before writing
                    var directory = Path.GetDirectoryName(_logFilePath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        System.Diagnostics.Debug.WriteLine($"Created directory: {directory}");
                    }

                    File.AppendAllText(_logFilePath, logLine + Environment.NewLine, Encoding.UTF8);
                    System.Diagnostics.Debug.WriteLine($"Successfully wrote to log file on attempt {attempt + 1}");
                    return;
                }
                catch (Exception ex) when (attempt < maxRetries - 1)
                {
                    System.Diagnostics.Debug.WriteLine($"Write attempt {attempt + 1} failed: {ex.Message}. Retrying in {retryDelay.TotalMilliseconds}ms...");
                    System.Threading.Thread.Sleep(retryDelay);
                    retryDelay = TimeSpan.FromMilliseconds(retryDelay.TotalMilliseconds * 2);
                }
                catch (Exception ex)
                {
                    // If all retries fail, log the error and throw
                    System.Diagnostics.Debug.WriteLine($"All write attempts failed. Final error: {ex.Message}");
                    var fallbackPath = Path.Combine(Path.GetTempPath(), "Chronometre_error.log");
                    File.AppendAllText(fallbackPath, $"{DateTime.Now}: Failed to write session to {_logFilePath}: {ex.Message}\n");
                    throw;
                }
            }
        }
    }
}
