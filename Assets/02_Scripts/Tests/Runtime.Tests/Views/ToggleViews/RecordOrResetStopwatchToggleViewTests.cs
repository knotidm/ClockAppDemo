using NUnit.Framework;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class RecordOrResetStopwatchToggleViewTests : ZenjectIntegrationTestFixture
    {
        private RecordOrResetStopwatchToggleView _recordOrResetStopwatchToggleView;

        private RecordedTimesPresenter _recordedTimesPresenter;
        private Transform _content;
        private RecordedTimeView _recordedTimeViewPrefab;
        private TMP_Text _lapTimeLabelText;
        private TMP_Text _totalTimeLabelText;

        private StopwatchManager _stopwatchManager;
        private Toggle _toggle;
        private Image _toggleOnIcon;
        private Image _toggleOffIcon;

        [SetUp]
        public void CommonInstall()
        {
            _content = new GameObject().transform;
            _recordedTimeViewPrefab = new GameObject().AddComponent<RecordedTimeView>();
            _lapTimeLabelText = new GameObject().AddComponent<TextMeshProUGUI>();
            _totalTimeLabelText = new GameObject().AddComponent<TextMeshProUGUI>();

            _recordedTimesPresenter = new GameObject().AddComponent<RecordedTimesPresenter>();
            _recordedTimesPresenter._content = _content;
            _recordedTimesPresenter._recordedTimeViewPrefab = _recordedTimeViewPrefab;
            _recordedTimesPresenter._lapTimeLabelText = _lapTimeLabelText;
            _recordedTimesPresenter._totalTimeLabelText = _totalTimeLabelText;
            _recordedTimesPresenter.Start();

            Stopwatch stopwatch = new Stopwatch();
            _stopwatchManager = new StopwatchManager(stopwatch);
            _toggle = new GameObject().AddComponent<Toggle>();

            _toggleOnIcon = new GameObject().AddComponent<Image>();
            _toggleOffIcon = new GameObject().AddComponent<Image>();

            PreInstall();

            Container.Bind<IRecordedTimesPresenter>().FromInstance(_recordedTimesPresenter);
            Container.Bind<StopwatchManager>().FromInstance(_stopwatchManager).AsSingle();
            Container.Bind<RecordOrResetStopwatchToggleView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _recordOrResetStopwatchToggleView = Container.Resolve<RecordOrResetStopwatchToggleView>();
            _recordOrResetStopwatchToggleView._toggle = _toggle;
            _recordOrResetStopwatchToggleView._toggleOnIcon = _toggleOnIcon;
            _recordOrResetStopwatchToggleView._toggleOffIcon = _toggleOffIcon;
            _recordOrResetStopwatchToggleView.Start();
        }

        [Test]
        public void AddStopwatchRecord_ShouldCallAddRecordToList()
        {
            _stopwatchManager.ElapsedMilliseconds = 5000;
            _toggleOnIcon.gameObject.SetActive(true);
            _toggle.isOn = true;

            Assert.AreEqual(1, _recordedTimesPresenter._recordedTimeViews.Count);
        }

        [Test]
        public void RestartStopwatch_ShouldClearRecordsAndResetStopwatch()
        {
            _stopwatchManager.IsStopwatchCreated.Value = true;
            _toggleOffIcon.gameObject.SetActive(true);
            _toggle.isOn = true;

            Assert.AreEqual(0, _recordedTimesPresenter._recordedTimeViews.Count);
            Assert.IsFalse(_stopwatchManager.IsStopwatchCreated.Value);
            Assert.IsFalse(_toggle.interactable);
        }

        [Test]
        public void ToggleValueChanged_ShouldNotTriggerActionWhenStopwatchNotCreated()
        {
            _stopwatchManager.IsStopwatchCreated.Value = false;
            _toggle.isOn = true;

            Assert.AreEqual(0, _recordedTimesPresenter._recordedTimeViews.Count);
            Assert.IsFalse(_stopwatchManager.IsStopwatchCreated.Value);
            Assert.IsFalse(_toggle.interactable);
        }

        [Test]
        public void IsStopwatchCreated_ShouldUpdateToggleInteractable()
        {
            _stopwatchManager.IsStopwatchCreated.Value = true;
            Assert.IsTrue(_toggle.interactable);

            _stopwatchManager.IsStopwatchCreated.Value = false;
            Assert.IsFalse(_toggle.interactable);
        }

        [Test]
        public void IsStopwatchRunning_ShouldUpdateIconVisibility()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            Assert.IsTrue(_toggleOnIcon.gameObject.activeSelf);
            Assert.IsFalse(_toggleOffIcon.gameObject.activeSelf);

            _stopwatchManager.IsStopwatchRunning.Value = false;
            Assert.IsFalse(_toggleOnIcon.gameObject.activeSelf);
            Assert.IsTrue(_toggleOffIcon.gameObject.activeSelf);
        }
    }
}
