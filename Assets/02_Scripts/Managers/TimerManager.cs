using System.Diagnostics;
using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerManager : MonoBehaviour
    {
        [Inject] private readonly IInputFieldsPresenter _inputFieldsPresenter;

        public BoolReactiveProperty IsTimerCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsTimerRunning = new BoolReactiveProperty(false);

        private Stopwatch Timer { get; set; }
        private int _initialTime;

        public int TimeToExpire { get; private set; }

        private void Start()
        {
            IsTimerCreated.Value = false;
            IsTimerRunning.Value = false;

            IsTimerRunning.Subscribe(isTimerRunning =>
            {
                if (isTimerRunning)
                {
                    if (!IsTimerCreated.Value)
                    {
                        _initialTime = _inputFieldsPresenter.GetTimerInitialTime();
                        _inputFieldsPresenter.SetActive(false);

                        Timer = new Stopwatch();
                        Run();
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

            }).AddTo(this);

            IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    _inputFieldsPresenter.SetActive(true);
                    Stop();
                }
            }).AddTo(this);
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
        }
    }
}
