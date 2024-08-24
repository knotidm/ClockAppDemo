using System;
using System.Diagnostics;
using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StopwatchManager : MonoBehaviour
    {
        [SerializeField] private StopwatchEventChannelSO _stopwatchChannel;

        public Stopwatch Stopwatch { get; private set; }
        public long ElapsedMilliseconds { get; private set; }

        public event Action OnStopwatchStopEvent;

        private void Start()
        {
            _stopwatchChannel.IsStopwatchCreated.Value = false;
            _stopwatchChannel.IsStopwatchPlaying.Value = false;

            _stopwatchChannel.IsStopwatchPlaying.Subscribe(isStopwatchPlaying =>
            {
                if (isStopwatchPlaying)
                {
                    if (!_stopwatchChannel.IsStopwatchCreated.Value)
                    {
                        UnityEngine.Debug.Log("Stopwatch = new Stopwatch()");
                        Stopwatch = new Stopwatch();
                        Play();
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Stopwatch Resume()");
                        Resume();
                    }

                }
                else
                {
                    if (_stopwatchChannel.IsStopwatchCreated.Value)
                    {
                        UnityEngine.Debug.Log("Stopwatch Pause()");
                        Pause();
                    }
                }

            }).AddTo(this);

            _stopwatchChannel.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                if (!isStopwatchCreated)
                {
                    UnityEngine.Debug.Log("Stopwatch Stop()");

                    Stop();
                    OnStopwatchStopEvent?.Invoke();
                }
            }).AddTo(this);
        }

        private void Play()
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
                Play();
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