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
        }

        public void Update()
        {
            if (!_stopwatchManager.IsStopwatchRunning.Value) return;

            TimeSpan currentTimeSpan = TimeSpan.FromMilliseconds(_stopwatchManager.ElapsedMilliseconds);

            _elapsedTimeText.text = currentTimeSpan.ToString(@"mm\:ss\.ff");
        }

        private void ResetElapsedTimeText()
        {
            _elapsedTimeText.text = TimeSpan.Zero.ToString();
        }
    }
}
