using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StartOrPauseTimerToggleView : MainScreenToggleView
    {
        [SerializeField] private TimerEventChannelSO _timerEventChannel;

        protected override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(startTimer =>
            {
                _timerEventChannel.IsTimerPlaying.Value = startTimer;

                if (startTimer)
                {
                    _timerEventChannel.IsTimerCreated.Value = true;
                }

            }).AddTo(this);

            _timerEventChannel.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                _toggle.isOn = isTimerCreated;

            }).AddTo(this);
        }
    }
}
