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

            //StopwatchManager.stopwatch.CurrentTime.Subscribe(currentTime =>
            //{
            //    Debug.Log("StopwatchManager.stopwatch.CurrentTime.Subscribe currentTime - " + currentTime);
            //    Debug.Log("StopwatchManager.stopwatch.CurrentTime - " + StopwatchManager.stopwatch.CurrentTime);

            //    _elapsedTimeText.text = currentTime.ToString();
            //}).AddTo(this);
        }

        private void OnDestroy()
        {
            _stopwatchManager.OnStopwatchStopEvent -= ResetElapsedTimeText;
        }

        private void Update()
        {
            if (!_stopwatchManager.stopwatch.IsRunning) return;
            _elapsedTimeText.text = _stopwatchManager.stopwatch.CurrentTime.ToString();
        }

        private void ResetElapsedTimeText()
        {
            _elapsedTimeText.text = 0.ToString();
        }
    }
}
