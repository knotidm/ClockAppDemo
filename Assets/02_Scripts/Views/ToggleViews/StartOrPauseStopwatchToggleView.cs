using UniRx;
using Zenject;

namespace ClockAppDemo
{
    public class StartOrPauseStopwatchToggleView : MainScreenToggleView
    {
        [Inject] private readonly StopwatchManager _stopwatchManager;

        protected override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                _stopwatchManager.IsStopwatchRunning.Value = isOn;

                if (isOn)
                {
                    _stopwatchManager.IsStopwatchCreated.Value = true;
                }

            }).AddTo(this);
        }
    }
}
