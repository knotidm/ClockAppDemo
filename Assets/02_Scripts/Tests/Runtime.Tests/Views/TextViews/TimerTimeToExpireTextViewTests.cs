using NUnit.Framework;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class TimerTimeToExpireTextViewTests : ZenjectIntegrationTestFixture
    {
        private TimerTimeToExpireTextView _timerTimeToExpireTextView;
        private TimerManager _timerManager;
        private TMP_Text _timeToExpireText;

        [SetUp]
        public void CommonInstaller()
        {
            Stopwatch stopwatch = new Stopwatch();
            _timerManager = new TimerManager(stopwatch);
            _timeToExpireText = new GameObject().AddComponent<TextMeshProUGUI>();

            PreInstall();

            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
            Container.Bind<TimerTimeToExpireTextView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _timerTimeToExpireTextView = Container.Resolve<TimerTimeToExpireTextView>();
            _timerTimeToExpireTextView._timeToExpireText = _timeToExpireText;
            _timerTimeToExpireTextView.Start();
        }

        [Test]
        public void Start_WhenTimerIsNotCreated_ResetsTimeToExpireText()
        {
            _timerManager.IsTimerCreated.Value = false;
            _timerTimeToExpireTextView.Start();

            Assert.AreEqual(string.Empty, _timeToExpireText.text);
        }

        [Test]
        public void Update_WhenTimerIsNotRunning_DoesNotUpdateText()
        {
            _timerManager.IsTimerRunning.Value = false;
            _timerManager.TimeToExpire = 3600;
            _timeToExpireText.text = "Initial Text";

            _timerTimeToExpireTextView.Update();

            Assert.AreEqual("Initial Text", _timeToExpireText.text);
        }

        [Test]
        public void Update_WhenTimerIsRunning_UpdatesTextCorrectly()
        {
            _timerManager.IsTimerRunning.Value = true;
            _timerManager.TimeToExpire = 3661;

            _timerTimeToExpireTextView.Update();

            Assert.AreEqual("01:01:01", _timeToExpireText.text);
        }

        [Test]
        public void Update_WhenTimerIsRunningLessThanOneHour_UpdatesTextCorrectly()
        {
            _timerManager.IsTimerRunning.Value = true;
            _timerManager.TimeToExpire = 3599;

            _timerTimeToExpireTextView.Update();

            Assert.AreEqual("00:59:59", _timeToExpireText.text);
        }
    }
}
