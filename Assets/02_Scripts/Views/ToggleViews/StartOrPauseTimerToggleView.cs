using UniRx;
using Zenject;

namespace ClockAppDemo
{
    public class StartOrPauseTimerToggleView : MainScreenToggleView
    {
        [Inject] private readonly TimerManager _timerManager;
        [Inject] private readonly IInputFieldsPresenter _inputFieldsPresenter;

        public override void Start()
        {
            base.Start();

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (_inputFieldsPresenter.GetTimerInitialTime() > 0)
                {
                    _timerManager.IsTimerRunning.Value = isOn;
                }
                else
                {
                    _toggle.isOn = false;
                }
            }).AddTo(this);

            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                _toggle.isOn = isTimerCreated;

            }).AddTo(this);
        }
    }
}
