using System.Diagnostics;
using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StopwatchManager : MonoBehaviour
    {
        public BoolReactiveProperty IsStopwatchCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsStopwatchRunning = new BoolReactiveProperty(false);

        private Stopwatch Stopwatch { get; set; }
        public long ElapsedMilliseconds { get; private set; }

        private void Start()
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

            }).AddTo(this);

            IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                if (!isStopwatchCreated)
                {
                    Stop();
                }
            }).AddTo(this);
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
        }
    }
}