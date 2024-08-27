using TMPro;
using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    public class TimerInputFieldsPresenter : MonoBehaviour, IInputFieldsPresenter
    {
        private TimerManager _timerManager;

        [SerializeField] public TMP_InputField[] _inputFields;

        public void Initialize(TimerManager timerManager)
        {
            _timerManager = timerManager;

            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (isTimerCreated)
                {
                    int initialTime = GetTimerInitialTime();
                    _timerManager.SetInitialTimeInSeconds(initialTime);
                }

                SetActive(!isTimerCreated);
            });
        }

        public int GetTimerInitialTime()
        {
            string hoursInputFieldText = _inputFields[0].text;
            string minutesInputFieldText = _inputFields[1].text;
            string secondsInputFieldText = _inputFields[2].text;

            if (string.IsNullOrEmpty(hoursInputFieldText))
            {
                hoursInputFieldText = "00";
            }
            if (string.IsNullOrEmpty(minutesInputFieldText))
            {
                minutesInputFieldText = "00";
            }
            if (string.IsNullOrEmpty(secondsInputFieldText))
            {
                secondsInputFieldText = "00";
            }

            int hours = int.Parse(hoursInputFieldText);
            int minutes = int.Parse(minutesInputFieldText);
            int seconds = int.Parse(secondsInputFieldText);

            return hours * 3600 + minutes * 60 + seconds;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}