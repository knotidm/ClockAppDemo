using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerTimeToExpireTextView : MonoBehaviour
    {
        [SerializeField] TMP_Text _timeToExpireText;
        [Inject] TimerManager _timerManager;

        private void Start()
        {
            ResetElapsedTimeText();
            _timerManager.OnTimerStopEvent += ResetElapsedTimeText;
        }

        private void OnDestroy()
        {
            _timerManager.OnTimerStopEvent -= ResetElapsedTimeText;
        }

        private void Update()
        {
            if (_timerManager.Stopwatch == null) return;

            TimeSpan currentTimeSpan = TimeSpan.FromSeconds(_timerManager.TimeToExpire);

            _timeToExpireText.text = $"{(int)currentTimeSpan.TotalHours:00}{currentTimeSpan:\\:mm\\:ss}";
        }

        private void ResetElapsedTimeText()
        {
            _timeToExpireText.text = string.Empty;
        }
    }
}
