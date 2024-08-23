using ImprovedTimers;
using System;
using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TimerEventChannelSO _timerEventChannel;

        public Timer timer;

        public event Action OnTimerStopEvent;

        private void Start()
        {
            timer = new Timer(60f);
            _timerEventChannel.IsTimerCreated.Value = false;
            _timerEventChannel.IsTimerPlaying.Value = false;

            _timerEventChannel.IsTimerPlaying.Subscribe(isTimerPlaying =>
            {
                if (isTimerPlaying)
                {
                    if (!_timerEventChannel.IsTimerCreated.Value)
                    {
                        Debug.Log("Timer IsPlaying and not created = new Timer(60f)");

                        timer = new Timer(60f);
                        timer.Start();
                    }
                    else
                    {
                        Debug.Log("Timer IsPlaying and created - timer.Resume()");

                        timer.Resume();
                    }

                }
                else
                {
                    if (_timerEventChannel.IsTimerCreated.Value)
                    {
                        Debug.Log("Timer Is Not Playing and created - timer.Pause()");

                        timer.Pause();
                    }
                }

            }).AddTo(this);

            _timerEventChannel.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    Debug.Log("Timer Is not created - timer.Stop()");

                    timer.Stop();
                    OnTimerStopEvent?.Invoke();
                }
            }).AddTo(this);
        }
    }
}
