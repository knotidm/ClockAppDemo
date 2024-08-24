using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class StopwatchElapsedTimeTextView : MonoBehaviour
    {
        [SerializeField] TMP_Text _elapsedTimeText;
        [Inject] StopwatchManager _stopwatchManager;

        private void Start()
        {
            _stopwatchManager.OnStopwatchStopEvent += ResetElapsedTimeText;
        }

        private void OnDestroy()
        {
            _stopwatchManager.OnStopwatchStopEvent -= ResetElapsedTimeText;
        }

        private void Update()
        {
            if (_stopwatchManager.Stopwatch == null) return;

            TimeSpan currentTimeSpan = TimeSpan.FromMilliseconds(_stopwatchManager.ElapsedMilliseconds);

            _elapsedTimeText.text = currentTimeSpan.ToString(@"mm\:ss\.ff");
        }

        private void ResetElapsedTimeText()
        {
            _elapsedTimeText.text = TimeSpan.Zero.ToString();
        }
    }
}
