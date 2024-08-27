using System.Diagnostics;
using UniRx;

namespace ClockAppDemo
{
    public class StopwatchManager
    {
        public BoolReactiveProperty IsStopwatchCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsStopwatchRunning = new BoolReactiveProperty(false);

        private Stopwatch Stopwatch { get; set; }
        public long ElapsedMilliseconds { get; private set; }

        public StopwatchManager(Stopwatch stopwatch)
        {
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
                    if (!IsStopwatchCreated.Value)
                    {
                        Stopwatch = new Stopwatch();
                        Run();
                        IsStopwatchCreated.Value = true;
                    }
                    else
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
                    ElapsedMilliseconds = elapsedMilliseconds;
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
            ElapsedMilliseconds = 0;
            Stopwatch?.Stop();
            Stopwatch = null;
            IsStopwatchRunning.Value = false;

        }
    }
}