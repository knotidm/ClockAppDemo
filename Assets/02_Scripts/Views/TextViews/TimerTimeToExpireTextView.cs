using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class TimerTimeToExpireTextView : MonoBehaviour
    {
        [Inject] private readonly TimerManager _timerManager;

        [SerializeField] private TMP_Text _timeToExpireText;

        private void Start()
        {
            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    ResetTimeToExpireText();
                }
            }).AddTo(this);
        }

        private void Update()
        {
            if (!_timerManager.IsTimerRunning.Value) return;

            TimeSpan currentTimeSpan = TimeSpan.FromSeconds(_timerManager.TimeToExpire);

            _timeToExpireText.text = $"{(int)currentTimeSpan.TotalHours:00}{currentTimeSpan:\\:mm\\:ss}";
        }

        private void ResetTimeToExpireText()
        {
            _timeToExpireText.text = string.Empty;
        }
    }
}
