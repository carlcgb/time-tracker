using System;
using System.IO;
using System.Text.Json;

namespace Chronometre.Services
{
    public class Settings
    {
        public string LastUsedLogFolder { get; set; } = GetDefaultLogFolder();
        public bool StartHotkeyEnabled { get; set; } = true;
        public bool PauseHotkeyEnabled { get; set; } = true;
        public bool StopHotkeyEnabled { get; set; } = true;
        public bool AddNoteHotkeyEnabled { get; set; } = true;
        public bool StartMinimized { get; set; } = true;
        
        // Overlay settings
        public bool ShowElapsedInIndicator { get; set; } = true;
        public int OverlayAutoHideMinutes { get; set; } = 10;

        private static string GetDefaultLogFolder()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Chronometre"
            );
        }

        public static Settings Load()
        {
            var settingsPath = GetSettingsPath();
            
            if (!File.Exists(settingsPath))
            {
                return new Settings();
            }

            try
            {
                var json = File.ReadAllText(settingsPath);
                var settings = JsonSerializer.Deserialize<Settings>(json);
                return settings ?? new Settings();
            }
            catch (Exception)
            {
                // If loading fails, return default settings
                return new Settings();
            }
        }

        public void Save()
        {
            var settingsPath = GetSettingsPath();
            var directory = Path.GetDirectoryName(settingsPath);
            
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                var json = JsonSerializer.Serialize(this, options);
                File.WriteAllText(settingsPath, json);
            }
            catch (Exception)
            {
                // If saving fails, we can't do much about it
                // In a real application, you might want to show a user notification
            }
        }

        private static string GetSettingsPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Chronometre",
                "settings.json"
            );
        }
    }
}
