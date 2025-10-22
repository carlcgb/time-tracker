using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chronometre.Services
{
    public class HotkeyManager : IDisposable
    {
        private readonly Dictionary<int, Action> _hotkeys = new();
        private readonly Dictionary<int, Keys> _registeredKeys = new();
        private readonly HotkeyForm _hotkeyForm;
        private bool _disposed = false;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public HotkeyManager()
        {
            _hotkeyForm = new HotkeyForm(this);
            _hotkeyForm.Show(); // Show the form to ensure it's in the message loop
        }

        public bool RegisterHotkey(Keys keys, Action callback)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(HotkeyManager));

            var modifiers = GetModifiers(keys);
            var virtualKey = GetVirtualKey(keys);
            var id = _hotkeys.Count + 1;

            if (!RegisterHotKey(_hotkeyForm.Handle, id, modifiers, virtualKey))
            {
                // Return false instead of throwing exception
                return false;
            }

            _hotkeys[id] = callback;
            _registeredKeys[id] = keys;
            return true;
        }

        public void UnregisterHotkey(Keys keys)
        {
            if (_disposed) return;

            var id = GetHotkeyId(keys);
            if (id > 0)
            {
                UnregisterHotKey(_hotkeyForm.Handle, id);
                _hotkeys.Remove(id);
                _registeredKeys.Remove(id);
            }
        }

        public void ProcessHotkey(int id)
        {
            System.Diagnostics.Debug.WriteLine($"Processing hotkey ID: {id}");
            if (_hotkeys.TryGetValue(id, out var callback))
            {
                System.Diagnostics.Debug.WriteLine($"Executing hotkey callback for ID: {id}");
                callback();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"No callback found for hotkey ID: {id}");
            }
        }

        private int GetHotkeyId(Keys keys)
        {
            foreach (var kvp in _registeredKeys)
            {
                if (kvp.Value == keys)
                {
                    return kvp.Key;
                }
            }
            return -1;
        }

        private uint GetModifiers(Keys keys)
        {
            uint modifiers = 0;
            if (keys.HasFlag(Keys.Control)) modifiers |= 0x0002; // MOD_CONTROL
            if (keys.HasFlag(Keys.Alt)) modifiers |= 0x0001;     // MOD_ALT
            if (keys.HasFlag(Keys.Shift)) modifiers |= 0x0004;   // MOD_SHIFT
            if (keys.HasFlag(Keys.LWin) || keys.HasFlag(Keys.RWin)) modifiers |= 0x0008; // MOD_WIN
            return modifiers;
        }

        private uint GetVirtualKey(Keys keys)
        {
            return (uint)(keys & Keys.KeyCode);
        }

        public void Dispose()
        {
            if (_disposed) return;

            foreach (var id in _hotkeys.Keys)
            {
                UnregisterHotKey(_hotkeyForm.Handle, id);
            }

            _hotkeys.Clear();
            _registeredKeys.Clear();
            _hotkeyForm?.Dispose();
            _disposed = true;
        }
    }

    // Hidden form to receive hotkey messages
    public class HotkeyForm : Form
    {
        private readonly HotkeyManager _manager;
        private const uint WM_HOTKEY = 0x0312;

        public HotkeyForm(HotkeyManager manager)
        {
            _manager = manager;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;
            this.Opacity = 0;
            this.Size = new System.Drawing.Size(1, 1);
            this.Location = new System.Drawing.Point(-1000, -1000);
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                System.Diagnostics.Debug.WriteLine($"WM_HOTKEY received: wParam={m.WParam}, lParam={m.LParam}");
                _manager.ProcessHotkey((int)m.WParam);
            }
            base.WndProc(ref m);
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false); // Always keep hidden
        }
    }
}