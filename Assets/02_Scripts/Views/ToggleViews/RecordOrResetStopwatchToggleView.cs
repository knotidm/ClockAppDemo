using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class RecordOrResetStopwatchToggleView : MainScreenToggleView
    {
        [SerializeField] private StopwatchEventChannelSO _stopwatchEventChannel;
        [Inject] private IRecordedTimesPresenter _recordedTimesPresenter;
        [Inject] private StopwatchManager _stopwatchManager;

        private long _latestLapElapsedMiliseconds;

        protected override void Start()
        {
            RestartLatestLapElapsedMiliseconds();

            _stopwatchEventChannel.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                _toggle.interactable = isStopwatchCreated;
            }).AddTo(this);

            _stopwatchEventChannel.IsStopwatchPlaying.Subscribe(isStopwatchPlaying =>
            {
                _toggleOnIcon.gameObject.SetActive(isStopwatchPlaying);
                _toggleOffIcon.gameObject.SetActive(!isStopwatchPlaying);
            }).AddTo(this);

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (_toggleOnIcon.isActiveAndEnabled)
                {
                    _recordedTimesPresenter.AddRecordToList(
                        _stopwatchManager.ElapsedMilliseconds - _latestLapElapsedMiliseconds,
                        _stopwatchManager.ElapsedMilliseconds
                        );

                    _latestLapElapsedMiliseconds = _stopwatchManager.ElapsedMilliseconds;
                }
                else
                {
                    if (_stopwatchEventChannel.IsStopwatchCreated.Value)
                    {
                        _recordedTimesPresenter.ClearRecordedTimesPanel();
                        RestartLatestLapElapsedMiliseconds();

                        _stopwatchEventChannel.IsStopwatchCreated.Value = false;
                        _toggle.interactable = false;
                    }

                }
            }).AddTo(this);
        }

        private void RestartLatestLapElapsedMiliseconds()
        {
            _latestLapElapsedMiliseconds = 0;
        }
    }
}
