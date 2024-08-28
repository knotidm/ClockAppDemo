using System.Diagnostics;
using UniRx;

namespace ClockAppDemo
{
    public class StopwatchManager
    {
        public BoolReactiveProperty IsStopwatchCreated { get; set; }
        public BoolReactiveProperty IsStopwatchRunning { get; set; }

        private Stopwatch Stopwatch { get; set; }
        public LongReactiveProperty ElapsedMilliseconds { get; set; }

        public StopwatchManager(Stopwatch stopwatch)
        {
            IsStopwatchCreated = new BoolReactiveProperty(false);
            IsStopwatchRunning = new BoolReactiveProperty(false);
            ElapsedMilliseconds = new LongReactiveProperty();

            Stopwatch = stopwatch;
            Initialize();
        }

        ~StopwatchManager()
        {
            IsStopwatchCreated.Dispose();
            IsStopwatchRunning.Dispose();
        }

        private void Initialize()
        {
            IsStopwatchCreated.Value = false;
            IsStopwatchRunning.Value = false;

            IsStopwatchRunning.Subscribe(isStopwatchRunning =>
            {
                if (isStopwatchRunning)
                {
                    if (Stopwatch == null && !IsStopwatchCreated.Value)
                    {
                        Stopwatch = new Stopwatch();
                        Run();
                        IsStopwatchCreated.Value = true;
                    }
                    else if (Stopwatch != null && IsStopwatchCreated.Value)
                    {
                        Resume();
                    }

                }
                else
                {
                    if (IsStopwatchCreated.Value)
                    {
                        Pause();
                    }
                }

            });

            IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                if (!isStopwatchCreated)
                {
                    Stop();
                }
            });
        }

        private void Run()
        {
            Stopwatch.Start();

            Stopwatch.ObserveEveryValueChanged(stopwatch => stopwatch.ElapsedMilliseconds)
                .Subscribe(elapsedMilliseconds =>
                {
                    ElapsedMilliseconds.Value = elapsedMilliseconds;
                });
        }

        private void Pause()
        {
            if (Stopwatch.IsRunning)
            {
                Stopwatch.Stop();
            }
        }

        private void Resume()
        {
            if (!Stopwatch.IsRunning)
            {
                Run();
            }
        }

        private void Stop()
        {
            ElapsedMilliseconds.Value = 0;
            Stopwatch?.Stop();
            Stopwatch = null;
            IsStopwatchRunning.Value = false;

        }
    }
}