using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StartOrPauseStopwatchToggleView : MainScreenToggleView
    {
        [SerializeField] private StopwatchEventChannelSO _stopwatchEventChannel;

        protected override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                _stopwatchEventChannel.IsStopwatchPlaying.Value = isOn;

                if (isOn)
                {
                    _stopwatchEventChannel.IsStopwatchCreated.Value = true;
                }

            }).AddTo(this);
        }
    }
}
