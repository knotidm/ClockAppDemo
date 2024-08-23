using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class RecordOrResetStopwatchToggleView : MainScreenToggleView
    {
        [SerializeField] private StopwatchEventChannelSO _stopwatchEventChannel;

        protected override void Start()
        {
            _stopwatchEventChannel.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                _toggle.interactable = isStopwatchCreated;
            }).AddTo(this);

            _stopwatchEventChannel.IsStopwatchPlaying.Subscribe(isStopwatchPlaying =>
            {
                //_toggle.isOn = isStopwatchPlaying;
                _toggleOnIcon.gameObject.SetActive(isStopwatchPlaying);
                _toggleOffIcon.gameObject.SetActive(!isStopwatchPlaying);
            }).AddTo(this);

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (_toggleOnIcon.isActiveAndEnabled)
                {
                    // TODO: Record Stopwatch
                    //Debug.Log("TODO: Record Stopwatch");
                }
                else
                {
                    if (_stopwatchEventChannel.IsStopwatchCreated.Value)
                    {
                        Debug.Log("ResetStopwatchToggle _stopwatchEventChannel.IsStopwatchCreated.Value = false;");

                        _stopwatchEventChannel.IsStopwatchCreated.Value = false;
                        _toggle.interactable = false;

                        // TODO: Reset Stopwatch
                        //Debug.Log("TODO: Reset Stopwatch");
                    }

                }
            }).AddTo(this);
        }
    }
}
