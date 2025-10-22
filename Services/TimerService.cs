using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chronometre.Services
{
    public enum TimerState
    {
        Idle,
        Running,
        Paused
    }

    public class TimerStateChangedEventArgs : EventArgs
    {
        public TimerState NewState { get; }
        public TimerState PreviousState { get; }

        public TimerStateChangedEventArgs(TimerState newState, TimerState previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }
    }

    public class SessionData
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Notes { get; set; } = new();
        public TimerState FinalState { get; set; }
    }

    public class TimerService
    {
        private TimerState _currentState = TimerState.Idle;
        private DateTime _startTime;
        private DateTime _lastPauseTime;
        private TimeSpan _pausedDuration = TimeSpan.Zero;
        private readonly List<string> _sessionNotes = new();
        private readonly System.Windows.Forms.Timer _updateTimer;

        public event EventHandler<TimerStateChangedEventArgs>? StateChanged;
        public event EventHandler<TimeSpan>? ElapsedUpdated;

        public TimerState CurrentState => _currentState;
        public TimeSpan ElapsedTime => CalculateElapsedTime();

        public TimerService()
        {
            _updateTimer = new System.Windows.Forms.Timer { Interval = 1000 }; // Update every second
            _updateTimer.Tick += OnUpdateTimerTick;
        }

        public void Start(string? initialNotes = null)
        {
            if (_currentState != TimerState.Idle) return;

            _startTime = DateTime.Now;
            _pausedDuration = TimeSpan.Zero;
            _sessionNotes.Clear();

            if (!string.IsNullOrWhiteSpace(initialNotes))
            {
                _sessionNotes.Add(initialNotes);
            }

            SetState(TimerState.Running);
        }

        public void Pause()
        {
            if (_currentState != TimerState.Running) return;

            _lastPauseTime = DateTime.Now;
            SetState(TimerState.Paused);
        }

        public void Resume()
        {
            if (_currentState != TimerState.Paused) return;

            _pausedDuration += DateTime.Now - _lastPauseTime;
            SetState(TimerState.Running);
        }

        public void Stop()
        {
            if (_currentState == TimerState.Idle) return;

            SetState(TimerState.Idle);
        }

        public void AddNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note)) return;
            if (_currentState == TimerState.Idle) return;

            _sessionNotes.Add(note);
        }

        public SessionData GetSessionData()
        {
            var endTime = _currentState == TimerState.Idle ? DateTime.Now : (DateTime?)null;
            var duration = CalculateElapsedTime();

            return new SessionData
            {
                StartTime = _startTime,
                EndTime = endTime,
                Duration = duration,
                Notes = new List<string>(_sessionNotes),
                FinalState = _currentState
            };
        }

        private TimeSpan CalculateElapsedTime()
        {
            if (_currentState == TimerState.Idle)
                return TimeSpan.Zero;

            var now = DateTime.Now;
            var totalElapsed = now - _startTime - _pausedDuration;

            return totalElapsed < TimeSpan.Zero ? TimeSpan.Zero : totalElapsed;
        }

        private void SetState(TimerState newState)
        {
            var previousState = _currentState;
            _currentState = newState;
            
            // Start/stop update timer based on state
            if (newState == TimerState.Running)
            {
                _updateTimer.Start();
            }
            else
            {
                _updateTimer.Stop();
            }
            
            StateChanged?.Invoke(this, new TimerStateChangedEventArgs(newState, previousState));
        }

        private void OnUpdateTimerTick(object? sender, EventArgs e)
        {
            if (_currentState == TimerState.Running)
            {
                ElapsedUpdated?.Invoke(this, CalculateElapsedTime());
            }
        }
    }
}
