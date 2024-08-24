using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class RecordedTimesPresenter : MonoBehaviour, IRecordedTimesPresenter
    {
        [SerializeField] private Transform _content;
        [SerializeField] private RecordedTimeView _recordedTimeViewPrefab;
        [SerializeField] private TMP_Text _lapTimeLabelText;
        [SerializeField] private TMP_Text _totalTimeLabelText;

        private List<RecordedTimeView> _recordedTimeViews;
        private int _index;

        private void Start()
        {
            _recordedTimeViews = new List<RecordedTimeView>();
            ResetIndex();
            SetActiveLabelTexts(false);
        }

        public void AddRecordToList(long lapTime, long totalTime)
        {
            ++_index;

            if (_index == 1)
            {
                SetActiveLabelTexts(true);
            }

            RecordedTimeView recordedTimeView = Instantiate(_recordedTimeViewPrefab, _content);
            recordedTimeView.SetRecord(_index, lapTime, totalTime);
            _recordedTimeViews.Add(recordedTimeView);
        }

        public void ClearRecordedTimesPanel()
        {
            ResetIndex();
            SetActiveLabelTexts(false);
            ClearRecordList();
        }

        private void ResetIndex()
        {
            _index = 0;
        }

        private void SetActiveLabelTexts(bool active)
        {
            _lapTimeLabelText.gameObject.SetActive(active);
            _totalTimeLabelText.gameObject.SetActive(active);
        }

        private void ClearRecordList()
        {
            for (int i = 0; i < _recordedTimeViews.Count; i++)
            {
                RecordedTimeView recordedTimeView = _recordedTimeViews[i];
                Destroy(recordedTimeView.gameObject);
            }

            _recordedTimeViews.Clear();
        }
    }
}
