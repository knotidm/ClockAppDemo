using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ClockAppDemo
{
    public class ResetTimerButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TimerChannelSO _timerChannel;

        void Start()
        {
            _timerChannel.IsTimerCreated.Subscribe(
                isTimerCreated =>
                {
                    _button.interactable = isTimerCreated;

                }).AddTo(this);

            _button.OnClickAsObservable().Subscribe(
                _ =>
                {
                    _timerChannel.IsTimerCreated.Value = false;
                    _timerChannel.IsTimerPlaying.Value = false;
                    _button.interactable = false;
                    // TODO: Reset Timer
                    Debug.Log("TODO: Reset Timer");

                }).AddTo(this);

        }
    }
}
