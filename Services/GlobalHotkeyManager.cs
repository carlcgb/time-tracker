using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chronometre.Services
{
    public class GlobalHotkeyManager : IDisposable
    {
        private readonly Dictionary<int, Action> _hotkeys = new();
        private readonly Dictionary<int, Keys> _hotkeyDefinitions = new();
        private readonly LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;
        private bool _disposed = false;
        private int _nextHotkeyId = 1;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        public GlobalHotkeyManager()
        {
            _proc = HookCallback;
            _hookId = SetHook(_proc);
            System.Diagnostics.Debug.WriteLine($"Global hotkey manager initialized. Hook ID: {_hookId}");
        }

        public bool RegisterHotkey(Keys keys, Action callback)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(GlobalHotkeyManager));
            
            int hotkeyId = _nextHotkeyId++;
            _hotkeys[hotkeyId] = callback;
            _hotkeyDefinitions[hotkeyId] = keys;
            
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
            }
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                
                // Check for modifier keys
                bool ctrlPressed = (GetKeyState(0x11) & 0x8000) != 0; // VK_CONTROL
                bool altPressed = (GetKeyState(0x12) & 0x8000) != 0;   // VK_MENU
                bool shiftPressed = (GetKeyState(0x10) & 0x8000) != 0; // VK_SHIFT

                Keys fullKey = key;
                if (ctrlPressed) fullKey |= Keys.Control;
                if (altPressed) fullKey |= Keys.Alt;
                if (shiftPressed) fullKey |= Keys.Shift;

                var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine($"[{timestamp}] Key detected: {fullKey}");

                // Check if this matches any registered hotkey
                foreach (var kvp in _hotkeyDefinitions)
                {
                    if (kvp.Value == fullKey)
                    {
                        System.Diagnostics.Debug.WriteLine($"[{timestamp}] Hotkey match found: {fullKey} (ID: {kvp.Key})");
                        if (_hotkeys.TryGetValue(kvp.Key, out var callback))
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"[{timestamp}] Executing hotkey callback for: {fullKey}");
                                callback();
                                System.Diagnostics.Debug.WriteLine($"[{timestamp}] Hotkey callback completed for: {fullKey}");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"[{timestamp}] Error executing hotkey callback: {ex.Message}");
                            }
                        }
                        break; // Only execute one callback per key press
                    }
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
            
            _hotkeys.Clear();
            _hotkeyDefinitions.Clear();
            _disposed = true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }
}
