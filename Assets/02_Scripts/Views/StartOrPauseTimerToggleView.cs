using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StartOrPauseTimerToggleView : MainScreenToggleView
    {
        [SerializeField] private TimerChannelSO _timerChannel;

        protected override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                _timerChannel.IsTimerPlaying.Value = isOn;

                if (isOn)
                {
                    _timerChannel.IsTimerCreated.Value = true;
                    // TODO: Play Timer
                    Debug.Log("TODO: Play Timer");
                }
                else
                {
                    // TODO: Pause Timer
                    Debug.Log("TODO: Pause Timer");
                }

            }).AddTo(this);

            _timerChannel.IsTimerCreated.Subscribe(
                isTimerCreated =>
                {
                    _toggle.isOn = isTimerCreated;

                }).AddTo(this);
        }
    }
}
