using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chronometre.Services
{
    public class ReliableHotkeyManager : IDisposable
    {
        private readonly Dictionary<int, Action> _hotkeys = new();
        private readonly Dictionary<int, Keys> _hotkeyDefinitions = new();
        private readonly System.Windows.Forms.Timer _pollTimer;
        private readonly Dictionary<Keys, bool> _keyStates = new();
        private bool _disposed = false;
        private int _nextHotkeyId = 1;

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public ReliableHotkeyManager()
        {
            _pollTimer = new System.Windows.Forms.Timer();
            _pollTimer.Interval = 50; // Poll every 50ms
            _pollTimer.Tick += PollKeys;
            _pollTimer.Start();
            System.Diagnostics.Debug.WriteLine("ReliableHotkeyManager initialized with polling");
        }

        public bool RegisterHotkey(Keys keys, Action callback)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ReliableHotkeyManager));
            
            int hotkeyId = _nextHotkeyId++;
            _hotkeys[hotkeyId] = callback;
            _hotkeyDefinitions[hotkeyId] = keys;
            _keyStates[keys] = false; // Initialize as not pressed
            
            System.Diagnostics.Debug.WriteLine($"Registered hotkey: {keys} (ID: {hotkeyId})");
            return true;
        }

        public void UnregisterHotkey(Keys keys)
        {
            if (_disposed) return;
            
            // Find and remove the hotkey
            var toRemove = new List<int>();
            foreach (var kvp in _hotkeyDefinitions)
            {
                if (kvp.Value == keys)
                {
                    toRemove.Add(kvp.Key);
                }
            }
            
            foreach (var id in toRemove)
            {
                _hotkeys.Remove(id);
                _hotkeyDefinitions.Remove(id);
                _keyStates.Remove(keys);
            }
        }

        private void PollKeys(object? sender, EventArgs e)
        {
            if (_disposed) return;

            foreach (var kvp in _hotkeyDefinitions)
            {
                var keys = kvp.Value;
                var hotkeyId = kvp.Key;
                
                bool isPressed = IsHotkeyPressed(keys);
                
                // Ensure the key state is tracked
                if (!_keyStates.ContainsKey(keys))
                {
                    _keyStates[keys] = false;
                }
                
                bool wasPressed = _keyStates[keys];

                // If keys are pressed now but weren't pressed before, trigger the callback
                if (isPressed && !wasPressed)
                {
                    var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                    System.Diagnostics.Debug.WriteLine($"[{timestamp}] Hotkey triggered: {keys} (ID: {hotkeyId})");
                    if (_hotkeys.TryGetValue(hotkeyId, out var callback))
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine($"[{timestamp}] Executing hotkey callback for: {keys}");
                            callback();
                            System.Diagnostics.Debug.WriteLine($"[{timestamp}] Hotkey callback completed for: {keys}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[{timestamp}] Error executing hotkey callback: {ex.Message}");
                        }
                    }
                }

                // Update the state
                _keyStates[keys] = isPressed;
            }
        }

        private bool IsHotkeyPressed(Keys keys)
        {
            // Check modifier keys
            if (keys.HasFlag(Keys.Control))
            {
                if ((GetAsyncKeyState(0x11) & 0x8000) == 0) return false; // VK_CONTROL
            }
            if (keys.HasFlag(Keys.Alt))
            {
                if ((GetAsyncKeyState(0x12) & 0x8000) == 0) return false; // VK_MENU
            }
            if (keys.HasFlag(Keys.Shift))
            {
                if ((GetAsyncKeyState(0x10) & 0x8000) == 0) return false; // VK_SHIFT
            }

            // Check main key
            var mainKey = keys & Keys.KeyCode;
            if (mainKey != Keys.None)
            {
                int vkCode = (int)mainKey;
                if ((GetAsyncKeyState(vkCode) & 0x8000) == 0) return false;
            }

            return true;
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _pollTimer?.Stop();
            _pollTimer?.Dispose();
            _hotkeys.Clear();
            _hotkeyDefinitions.Clear();
            _keyStates.Clear();
            _disposed = true;
        }
    }
}
