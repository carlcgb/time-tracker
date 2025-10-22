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
            EnsureLogDirectoryExists();
            
            // Write a startup entry to ensure the log file is created
            WriteStartupEntry();
        }

        private string GetLogFilePath(string? customLogPath)
        {
            if (!string.IsNullOrEmpty(customLogPath))
            {
                System.Diagnostics.Debug.WriteLine($"Using custom log path: {customLogPath}");
                return customLogPath;
            }

            // Use Desktop folder as primary location
            var desktopPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Chrono-log.txt"
            );

            System.Diagnostics.Debug.WriteLine($"Testing desktop path: {desktopPath}");
            if (CanWriteToPath(desktopPath))
            {
                System.Diagnostics.Debug.WriteLine($"Using desktop path: {desktopPath}");
                return desktopPath;
            }

            // Fallback to Documents folder
            var documentsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Chronometre",
                "Chrono-log.txt"
            );

            System.Diagnostics.Debug.WriteLine($"Testing documents path: {documentsPath}");
            if (CanWriteToPath(documentsPath))
            {
                System.Diagnostics.Debug.WriteLine($"Using documents path: {documentsPath}");
                return documentsPath;
            }

            // Final fallback to AppData
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Chronometre",
                "Chrono-log.txt"
            );

            System.Diagnostics.Debug.WriteLine($"Using AppData fallback path: {appDataPath}");
            return appDataPath;
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
                    File.WriteAllText(_logFilePath, "# Chronomètre Time Tracker Log - Chrono-log.txt\n# Format: YYYY-MM-DD HH:mm:ss zzz    Duration=HH:mm:ss    DayTotal=HH:mm:ss    State=Stopped    Notes=\"...\"    AppVersion=X.Y.Z\n# All sessions are aggregated in this single file\n\n", Encoding.UTF8);
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
                var startupEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss zzz}\tDuration=00:00:00\tDayTotal=00:00:00\tState=ApplicationStarted\tNotes=\"Chronomètre started\"\tAppVersion={GetApplicationVersion()}\n";
                File.AppendAllText(_logFilePath, startupEntry);
            }
            catch (Exception ex)
            {
                // Log error to a fallback location
                var fallbackPath = Path.Combine(Path.GetTempPath(), "Chronometre_error.log");
                File.AppendAllText(fallbackPath, $"{DateTime.Now}: Failed to write startup entry: {ex.Message}\n");
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
            var timestamp = sessionData.StartTime.ToString("yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture);
            var duration = sessionData.Duration.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            var dayTotal = dailyTotal.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            var state = sessionData.FinalState.ToString();
            var notes = FormatNotes(sessionData.Notes);
            var version = GetApplicationVersion();

            return $"{timestamp}\tDuration={duration}\tDayTotal={dayTotal}\tState={state}\tNotes=\"{notes}\"\tAppVersion={version}";
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

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    File.AppendAllText(_logFilePath, logLine + Environment.NewLine, Encoding.UTF8);
                    return;
                }
                catch (Exception ex) when (attempt < maxRetries - 1)
                {
                    System.Threading.Thread.Sleep(retryDelay);
                    retryDelay = TimeSpan.FromMilliseconds(retryDelay.TotalMilliseconds * 2);
                }
                catch
                {
                    // If all retries fail, we can't do much about it
                    // In a real application, you might want to show a user notification
                    throw;
                }
            }
        }
    }
}
