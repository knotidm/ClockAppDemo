using UniRx;
using Zenject;

namespace ClockAppDemo
{
    public class RecordOrResetStopwatchToggleView : MainScreenToggleView
    {
        [Inject] private readonly IRecordedTimesPresenter _recordedTimesPresenter;
        [Inject] private readonly StopwatchManager _stopwatchManager;

        private long _latestLapElapsedMiliseconds;

        public override void Start()
        {
            RestartLatestLapElapsedMiliseconds();

            _stopwatchManager.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                _toggle.interactable = isStopwatchCreated;
            }).AddTo(this);

            _stopwatchManager.IsStopwatchRunning.Subscribe(isStopwatchRunning =>
            {
                _toggleOnIcon.gameObject.SetActive(isStopwatchRunning);
                _toggleOffIcon.gameObject.SetActive(!isStopwatchRunning);
            }).AddTo(this);

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (_toggleOnIcon.isActiveAndEnabled)
                {
                    AddStopwatchRecord();
                }
                else
                {
                    if (_stopwatchManager.IsStopwatchCreated.Value)
                    {
                        RestartStopwatch();
                    }

                }
            }).AddTo(this);
        }

        private void AddStopwatchRecord()
        {
            _recordedTimesPresenter.AddRecordToList(
                _stopwatchManager.ElapsedMilliseconds - _latestLapElapsedMiliseconds,
                _stopwatchManager.ElapsedMilliseconds
                );

            _latestLapElapsedMiliseconds = _stopwatchManager.ElapsedMilliseconds;
        }

        private void RestartStopwatch()
        {
            _recordedTimesPresenter.ClearRecordedTimesPanel();
            RestartLatestLapElapsedMiliseconds();

            _stopwatchManager.IsStopwatchCreated.Value = false;
            _toggle.interactable = false;
        }

        private void RestartLatestLapElapsedMiliseconds()
        {
            _latestLapElapsedMiliseconds = 0;
        }
    }
}
