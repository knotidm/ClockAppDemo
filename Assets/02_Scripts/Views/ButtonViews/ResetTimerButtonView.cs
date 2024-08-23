using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ClockAppDemo
{
    public class ResetTimerButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TimerEventChannelSO _timerEventChannel;

        void Start()
        {
            _timerEventChannel.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                _button.interactable = isTimerCreated;

            }).AddTo(this);

            _button.OnClickAsObservable().Subscribe(_ =>
            {
                _timerEventChannel.IsTimerCreated.Value = false;
                _timerEventChannel.IsTimerPlaying.Value = false;
                _button.interactable = false;
                // TODO: Reset Timer
                //Debug.Log("TODO: Reset Timer");

            }).AddTo(this);

        }
    }
}
