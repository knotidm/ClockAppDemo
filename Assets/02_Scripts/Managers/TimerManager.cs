using System;
using System.Diagnostics;
using UniRx;

namespace ClockAppDemo
{
    public class TimerManager
    {
        public BoolReactiveProperty IsTimerCreated { get; set; }
        public BoolReactiveProperty IsTimerRunning { get; set; }

        private Stopwatch Timer { get; set; }
        private int _initialTime;

        public IntReactiveProperty TimeToExpire { get; set; }

        public event Action OnTimerFinished;

        public TimerManager(Stopwatch timer)
        {
            IsTimerCreated = new BoolReactiveProperty(false);
            IsTimerRunning = new BoolReactiveProperty(false);
            TimeToExpire = new IntReactiveProperty();
            Timer = timer;
            Initialize();
        }

        ~TimerManager()
        {
            IsTimerCreated.Dispose();
            IsTimerRunning.Dispose();
        }

        private void Initialize()
        {
            IsTimerCreated.Value = false;
            IsTimerRunning.Value = false;

            IsTimerRunning.Subscribe(isTimerRunning =>
            {
                if (isTimerRunning)
                {
                    if (Timer == null && !IsTimerCreated.Value)
                    {
                        IsTimerCreated.Value = true;

                        Timer = new Stopwatch();
                        Run();
                    }
                    else if (Timer != null && IsTimerCreated.Value)
                    {
                        Resume();
                    }

                }
                else
                {
                    if (IsTimerCreated.Value)
                    {
                        Pause();
                    }
                }

            });

            IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    Stop();
                }
            });
        }

        public void SetInitialTimeInSeconds(int initialTime)
        {
            _initialTime = initialTime;
        }

        private void Run()
        {
            Timer.Start();


            Timer.ObserveEveryValueChanged(stopwatch => stopwatch.Elapsed.Seconds)
            .TakeWhile(elapsedSeconds => elapsedSeconds <= _initialTime)
            .Subscribe(elapsedSeconds =>
            {
                TimeToExpire.Value = _initialTime - elapsedSeconds;

                if (TimeToExpire.Value == 0)
                {
                    OnTimerFinished?.Invoke();
                }
            });
        }

        private void Pause()
        {
            Timer?.Stop();
        }

        private void Resume()
        {
            if (!Timer.IsRunning)
            {
                Run();
            }
        }

        private void Stop()
        {
            Timer?.Stop();
            Timer = null;
            IsTimerRunning.Value = false;
            TimeToExpire.Value = 0;
        }
    }
}
