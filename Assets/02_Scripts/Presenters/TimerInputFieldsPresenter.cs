using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerInputFieldsPresenter : MonoBehaviour, IInputFieldsPresenter
    {
        [Inject] private readonly TimerManager _timerManager;

        [SerializeField] TMP_InputField[] _inputFields;

        protected void Start()
        {
            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (isTimerCreated)
                {
                    _timerManager.SetInitialTimeInSeconds(GetTimerInitialTime());
                }

                SetActive(!isTimerCreated);
            });
        }

        public int GetTimerInitialTime()
        {
            int hours = int.Parse(_inputFields[0].text);
            int minutes = int.Parse(_inputFields[1].text);
            int seconds = int.Parse(_inputFields[2].text);

            return hours * 3600 + minutes * 60 + seconds;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
