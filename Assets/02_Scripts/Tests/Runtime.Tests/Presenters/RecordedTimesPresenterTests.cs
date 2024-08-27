using NUnit.Framework;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class RecordedTimesPresenterTests : ZenjectIntegrationTestFixture
    {
        private RecordedTimesPresenter _recordedTimesPresenter;
        private Transform _content;
        private RecordedTimeView _recordedTimeViewPrefab;
        private TMP_Text _lapTimeLabelText;
        private TMP_Text _totalTimeLabelText;

        [SetUp]
        public void CommonInstall()
        {
            _content = new GameObject().transform;
            _recordedTimeViewPrefab = new GameObject().AddComponent<RecordedTimeView>();
            _lapTimeLabelText = new GameObject().AddComponent<TextMeshProUGUI>();
            _totalTimeLabelText = new GameObject().AddComponent<TextMeshProUGUI>();

            PreInstall();

            Container.Bind<RecordedTimesPresenter>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _recordedTimesPresenter = Container.Resolve<RecordedTimesPresenter>();
            _recordedTimesPresenter._content = _content;
            _recordedTimesPresenter._recordedTimeViewPrefab = _recordedTimeViewPrefab;
            _recordedTimesPresenter._lapTimeLabelText = _lapTimeLabelText;
            _recordedTimesPresenter._totalTimeLabelText = _totalTimeLabelText;
            _recordedTimesPresenter.Start();

        }

        [Test]
        public void AddRecordToList_FirstRecord_ActivatesLabelTexts()
        {
            _recordedTimesPresenter.AddRecordToList(1000, 1000);

            Assert.IsTrue(_lapTimeLabelText.gameObject.activeSelf);
            Assert.IsTrue(_totalTimeLabelText.gameObject.activeSelf);
        }

        [Test]
        public void AddRecordToList_MultipleRecords_CorrectIndexAndCount()
        {
            _recordedTimesPresenter.AddRecordToList(1000, 1000);
            _recordedTimesPresenter.AddRecordToList(2000, 3000);
            _recordedTimesPresenter.AddRecordToList(3000, 6000);

            Assert.AreEqual(3, _recordedTimesPresenter._recordedTimeViews.Count);
        }

        [Test]
        public void ClearRecordedTimesPanel_AfterAddingRecords_ClearsAllRecords()
        {
            _recordedTimesPresenter.AddRecordToList(1000, 1000);
            _recordedTimesPresenter.AddRecordToList(2000, 3000);

            _recordedTimesPresenter.ClearRecordedTimesPanel();

            Assert.AreEqual(0, _recordedTimesPresenter._recordedTimeViews.Count);
        }


        [Test]
        public void ClearRecordedTimesPanel_DisablesLabelTexts()
        {
            _recordedTimesPresenter.ClearRecordedTimesPanel();

            Assert.IsFalse(_lapTimeLabelText.gameObject.activeSelf);
            Assert.IsFalse(_totalTimeLabelText.gameObject.activeSelf);
        }

        [Test]
        public void ClearRecordedTimesPanel_ResetsIndex()
        {
            _recordedTimesPresenter.ClearRecordedTimesPanel();

            Assert.AreEqual(0, _recordedTimesPresenter._index);
        }

        [Test]
        public void ClearRecordedTimesPanel_WhenEmpty_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => _recordedTimesPresenter.ClearRecordedTimesPanel());
        }

        [Test]
        public void AddRecordToList_LargeNumbers_HandlesCorrectly()
        {
            long maxLong = long.MaxValue;
            _recordedTimesPresenter.AddRecordToList(maxLong, maxLong);

            Assert.AreEqual(1, _recordedTimesPresenter._recordedTimeViews.Count);
        }
    }
}