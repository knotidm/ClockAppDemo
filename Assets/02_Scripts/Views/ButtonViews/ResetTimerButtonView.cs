using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo
{
    public class ResetTimerButtonView : MonoBehaviour
    {
        [Inject] private readonly TimerManager _timerManager;

        [SerializeField] private Button _button;

        void Start()
        {
            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                _button.interactable = isTimerCreated;

            }).AddTo(this);

            _button.OnClickAsObservable().Subscribe(onClick =>
            {
                _timerManager.IsTimerCreated.Value = false;
                _timerManager.IsTimerRunning.Value = false;
                _button.interactable = false;
            }).AddTo(this);

        }
    }
}
