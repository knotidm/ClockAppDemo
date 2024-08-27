using System;
using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class RecordedTimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberText;
        [SerializeField] private TMP_Text _lapTimeText;
        [SerializeField] private TMP_Text _totalTimeText;

        public void SetRecord(int index, long lapTime, long totalTime)
        {
            if (_numberText != null)
            {
                _numberText.text = index.ToString();
            }
            if (_lapTimeText != null)
            {
                TimeSpan lapTimeSpan = TimeSpan.FromMilliseconds(lapTime);
                _lapTimeText.text = lapTimeSpan.ToString(@"mm\:ss\.ff");
            }
            if (_totalTimeText != null)
            {
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(totalTime);
                _totalTimeText.text = totalTimeSpan.ToString(@"mm\:ss\.ff");
            }
        }
    }
}
