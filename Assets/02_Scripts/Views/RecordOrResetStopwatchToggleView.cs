using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class RecordOrResetStopwatchToggleView : MainScreenToggleView
    {
        [SerializeField] private StopwatchChannelSO _stopwatchChannel;

        protected override void Start()
        {
            _stopwatchChannel.IsStopwatchCreated.Subscribe(
                isStopwatchCreated =>
                {
                    _toggle.interactable = isStopwatchCreated;
                }).AddTo(this);

            _stopwatchChannel.IsStopwatchPlaying.Subscribe(
                isStopwatchPlaying =>
                {
                    _toggle.isOn = isStopwatchPlaying;
                    _toggleOnIcon.gameObject.SetActive(isStopwatchPlaying);
                    _toggleOffIcon.gameObject.SetActive(!isStopwatchPlaying);
                }).AddTo(this);

            _toggle.OnValueChangedAsObservable().Subscribe(
                isOn =>
                {
                    if (_toggleOnIcon.isActiveAndEnabled)
                    {
                        // TODO: Record Stopwatch
                        Debug.Log("TODO: Record Stopwatch");
                    }
                    else
                    {
                        if (_stopwatchChannel.IsStopwatchCreated.Value)
                        {
                            _stopwatchChannel.IsStopwatchCreated.Value = false;
                            _toggle.interactable = false;

                            // TODO: Reset Stopwatch
                            Debug.Log("TODO: Reset Stopwatch");
                        }

                    }
                }).AddTo(this);
        }
    }
}
