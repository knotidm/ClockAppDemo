using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class StopwatchElapsedTimeTextView : MonoBehaviour
    {
        [Inject] private readonly StopwatchManager _stopwatchManager;

        [SerializeField] public TMP_Text _elapsedTimeText;

        public void Start()
        {
            _stopwatchManager.IsStopwatchCreated.Subscribe(isStopwatchCreated =>
            {
                if (!isStopwatchCreated)
                {
                    ResetElapsedTimeText();
                }
            }).AddTo(this);

            _stopwatchManager.ElapsedMilliseconds.Subscribe(elapsedMilliseconds =>
            {
                if (!_stopwatchManager.IsStopwatchRunning.Value) return;

                TimeSpan currentTimeSpan = TimeSpan.FromMilliseconds(elapsedMilliseconds);

                _elapsedTimeText.text = currentTimeSpan.ToString(@"mm\:ss\.ff");

            }).AddTo(this);
        }

        private void ResetElapsedTimeText()
        {
            _elapsedTimeText.text = TimeSpan.Zero.ToString();
        }
    }
}
