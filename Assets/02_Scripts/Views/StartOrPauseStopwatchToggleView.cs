using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class StartOrPauseStopwatchToggleView : MainScreenToggleView
    {
        [SerializeField] private StopwatchChannelSO _stopwatchChannel;

        protected override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                _stopwatchChannel.IsStopwatchPlaying.Value = isOn;

                if (isOn)
                {
                    _stopwatchChannel.IsStopwatchCreated.Value = true;
                    // TODO: Play Stopwatch
                    Debug.Log("TODO: Play Stopwatch");
                }
                else
                {
                    // TODO: Pause Stopwatch
                    Debug.Log("TODO: Pause Stopwatch");
                }

            }).AddTo(this);
        }
    }
}
