using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerElapsedTimeTextView : MonoBehaviour
    {
        [SerializeField] TMP_Text _elapsedTimeText;
        [Inject] TimerManager _timerManager;


        private void Start()
        {
            _timerManager.OnTimerStopEvent += ResetElapsedTimeText;
        }

        private void OnDestroy()
        {
            _timerManager.OnTimerStopEvent -= ResetElapsedTimeText;
        }

        private void Update()
        {
            if (!_timerManager.timer.IsRunning) return;
            _elapsedTimeText.text = _timerManager.timer.CurrentTime.ToString();
        }

        private void ResetElapsedTimeText()
        {
            _elapsedTimeText.text = 0.ToString();
        }
    }
}
