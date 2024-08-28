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

        [SerializeField] public TMP_Text _timeToExpireText;

        public void Start()
        {
            _timerManager.IsTimerCreated.Subscribe(isTimerCreated =>
            {
                if (!isTimerCreated)
                {
                    ResetTimeToExpireText();
                }
            }).AddTo(this);

            _timerManager.TimeToExpire.Subscribe(timeToExpire =>
            {
                if (!_timerManager.IsTimerRunning.Value) return;

                TimeSpan currentTimeSpan = TimeSpan.FromSeconds(timeToExpire);

                _timeToExpireText.text = $"{(int)currentTimeSpan.TotalHours:00}{currentTimeSpan:\\:mm\\:ss}";

            }).AddTo(this);
        }

        private void ResetTimeToExpireText()
        {
            _timeToExpireText.text = string.Empty;
        }
    }
}
