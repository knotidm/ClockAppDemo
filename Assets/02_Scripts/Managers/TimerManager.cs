using System;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TimerEventChannelSO _timerEventChannel;
        [Inject] private IInputFieldsPresenter _inputFieldsPresenter;

        public Stopwatch Stopwatch { get; private set; }

        private int _countDownSeconds;
        public int TimeToExpire { get; private set; }

        public event Action OnTimerStopEvent;

        private void Start()
        {
            _timerEventChannel.IsTimerCreated.Value = false;
            _timerEventChannel.IsTimerPlaying.Value = false;

            _timerEventChannel.IsTimerPlaying.Subscribe(isTimerPlaying =>
            {
                if (isTimerPlaying)
                {
                    if (!_timerEventChannel.IsTimerCreated.Value)
                    {
                        _countDownSeconds = _inputFieldsPresenter.GetTimerInitialTime();
                        _inputFieldsPresenter.SetActive(false);
                        UnityEngine.Debug.Log("Timer = new Timer() with _countDownSeconds -" + _countDownSeconds);

                        Stopwatch = new Stopwatch();
                        Play();
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Timer Resume()");
                        Resume();
                    }

                }
                else
                {
                    if (_timerEventChannel.IsTimerCreated.Value)
                    {
                        UnityEngine.Debug.Log("Timer Pause()");
                        Pause();
                    }
                }

            }).AddTo(this);

            _timerEventChannel.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    UnityEngine.Debug.Log("Timer Stop()");
                    _inputFieldsPresenter.SetActive(true);

                    Stop();
                    OnTimerStopEvent?.Invoke();
                }
            }).AddTo(this);
        }

        private void Play()
        {
            Stopwatch.Start();

            Stopwatch.ObserveEveryValueChanged(stopwatch => stopwatch.Elapsed.Seconds)
                .TakeWhile(elapsedSeconds => elapsedSeconds <= _countDownSeconds)
                .Subscribe(elapsedSeconds =>
                {
                    TimeToExpire = _countDownSeconds - elapsedSeconds;
                });
        }

        private void Pause()
        {
            Stopwatch?.Stop();
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
            Stopwatch?.Stop();
            Stopwatch = null;
        }
    }
}
