using ImprovedTimers;
using System;
using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StopwatchManager : MonoBehaviour
    {
        [SerializeField] private StopwatchEventChannelSO _stopwatchChannel;

        public Stopwatch stopwatch;

        public event Action OnStopwatchStopEvent;

        private void Start()
        {
            stopwatch = new Stopwatch();
            _stopwatchChannel.IsStopwatchCreated.Value = false;
            _stopwatchChannel.IsStopwatchPlaying.Value = false;

            _stopwatchChannel.IsStopwatchPlaying.Subscribe(isStopwatchPlaying =>
            {
                if (isStopwatchPlaying)
                {
                    if (!_stopwatchChannel.IsStopwatchCreated.Value)
                    {
                        Debug.Log("Stopwatch IsPlaying and not created = new Stopwatch()");

                        stopwatch = new Stopwatch();
                        stopwatch.Start();
                    }
                    else
                    {
                        Debug.Log("Stopwatch IsPlaying and Created - stopwatch.Resume()");

                        stopwatch.Resume();
                    }

                }
                else
                {
                    if (_stopwatchChannel.IsStopwatchCreated.Value)
                    {
                        Debug.Log("Stopwatch Is Not Playing and created - stopwatch.Pause()");

                        stopwatch.Pause();
                    }
                }

            }).AddTo(this);

            _stopwatchChannel.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                if (!isStopwatchCreated)
                {
                    Debug.Log("Stopwatch Is Not Playing and not created - stopwatch.Stop()");

                    stopwatch.Stop();
                    OnStopwatchStopEvent?.Invoke();
                }
            }).AddTo(this);
        }
    }
}
