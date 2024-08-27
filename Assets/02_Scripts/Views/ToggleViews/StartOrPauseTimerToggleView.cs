using UniRx;
using Zenject;

namespace ClockAppDemo
{
    public class StartOrPauseTimerToggleView : MainScreenToggleView
    {
        [Inject] private readonly TimerManager _timerManager;

        public override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                _timerManager.IsTimerRunning.Value = isOn;

            }).AddTo(this);

            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                _toggle.isOn = isTimerCreated;

            }).AddTo(this);
        }
    }
}
