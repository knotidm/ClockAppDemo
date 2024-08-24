using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class TimerInputFieldsPresenter : MonoBehaviour, IInputFieldsPresenter
    {
        [SerializeField] TMP_InputField[] _inputFields;

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
