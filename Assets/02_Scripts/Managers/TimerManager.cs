using System.Diagnostics;
using UniRx;

namespace ClockAppDemo
{
    public class TimerManager
    {
        public BoolReactiveProperty IsTimerCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsTimerRunning = new BoolReactiveProperty(false);

        private Stopwatch Timer { get; set; }
        private int _initialTime;

        public int TimeToExpire { get; private set; }

        public TimerManager(Stopwatch timer)
        {
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
                    if (!IsTimerCreated.Value)
                    {
                        Timer = new Stopwatch();
                        Run();
                        IsTimerCreated.Value = true;
                    }
                    else
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
                    TimeToExpire = _initialTime - elapsedSeconds;
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
            TimeToExpire = 0;
        }
    }
}
