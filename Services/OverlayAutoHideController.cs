using System;
using System.Windows.Forms;
using Chronometre.Forms;

namespace Chronometre.Services
{
    public class OverlayAutoHideController
    {
        private readonly OverlayIndicatorForm _overlay;
        private readonly System.Windows.Forms.Timer _updateTimer;
        private DateTime _runningSince;
        private int _autoHideMinutes = 10;
        private bool _isRunning = false;
        private bool _overlayVisible = false;

        public event EventHandler? SettingsChanged;

        public int AutoHideMinutes
        {
            get => _autoHideMinutes;
            set
            {
                _autoHideMinutes = value;
                SettingsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool ShowElapsed
        {
            get => _overlay.ShowElapsed;
            set
            {
                _overlay.SetShowElapsed(value);
                SettingsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public OverlayAutoHideController(OverlayIndicatorForm overlay)
        {
            _overlay = overlay;
            _updateTimer = new System.Windows.Forms.Timer { Interval = 1000 }; // Update every second
            _updateTimer.Tick += OnUpdateTimerTick;
        }

        public void OnStateChanged(TimerState newState, TimeSpan elapsed)
        {
            switch (newState)
            {
                case TimerState.Running:
                    _isRunning = true;
                    _runningSince = DateTime.Now;
                    _overlayVisible = true;
                    _overlay.ShowRunningIndicator(elapsed);
                    _updateTimer.Start();
                    break;

                case TimerState.Paused:
                    _isRunning = false;
                    _overlayVisible = true;
                    _overlay.ShowPausedIndicator(elapsed);
                    _updateTimer.Stop();
                    break;

                case TimerState.Idle:
                    _isRunning = false;
                    _overlayVisible = false;
                    _overlay.HideIndicator();
                    _updateTimer.Stop();
                    break;
            }
        }

        public void OnElapsedUpdated(TimeSpan elapsed)
        {
            if (_overlayVisible && !_overlay.IsAutoHidden)
            {
                _overlay.UpdateElapsed(elapsed);
            }

            // Check auto-hide threshold
            if (_isRunning && _autoHideMinutes > 0)
            {
                var runningDuration = DateTime.Now - _runningSince;
                if (runningDuration.TotalMinutes >= _autoHideMinutes)
                {
                    _overlay.HideIndicator();
                    _overlayVisible = false;
                }
            }
        }

        public void Peek()
        {
            if (_overlayVisible)
            {
                _overlay.Peek(3000);
            }
        }

        private void OnUpdateTimerTick(object? sender, EventArgs e)
        {
            if (_isRunning && _autoHideMinutes > 0)
            {
                var runningDuration = DateTime.Now - _runningSince;
                if (runningDuration.TotalMinutes >= _autoHideMinutes)
                {
                    _overlay.HideIndicator();
                    _updateTimer.Stop();
                }
            }
        }

        public void Dispose()
        {
            _updateTimer?.Dispose();
        }
    }
}
