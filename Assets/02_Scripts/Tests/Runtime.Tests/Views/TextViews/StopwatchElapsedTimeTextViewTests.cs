using NUnit.Framework;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class StopwatchElapsedTimeTextViewTests : ZenjectIntegrationTestFixture
    {
        private StopwatchElapsedTimeTextView _stopwatchElapsedTimeTextView;
        private StopwatchManager _stopwatchManager;
        private TMP_Text _elapsedTimeText;

        [SetUp]
        public void CommonInstall()
        {
            Stopwatch stopwatch = new Stopwatch();
            _stopwatchManager = new StopwatchManager(stopwatch);

            _elapsedTimeText = new GameObject().AddComponent<TextMeshProUGUI>();

            PreInstall();

            Container.Bind<StopwatchManager>().FromInstance(_stopwatchManager).AsSingle();
            Container.Bind<StopwatchElapsedTimeTextView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _stopwatchElapsedTimeTextView = Container.Resolve<StopwatchElapsedTimeTextView>();
            _stopwatchElapsedTimeTextView._elapsedTimeText = _elapsedTimeText;
            _stopwatchElapsedTimeTextView.Start();
        }

        [Test]
        public void Start_WhenStopwatchIsNotCreated_ResetsElapsedTimeText()
        {
            _stopwatchManager.IsStopwatchCreated.Value = false;

            Assert.AreEqual("00:00:00", _elapsedTimeText.text);
        }

        [Test]
        public void Update_WhenStopwatchIsRunning_UpdatesElapsedTimeText()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            _stopwatchManager.ElapsedMilliseconds.Value = 65000;

            Assert.AreEqual("01:05.00", _elapsedTimeText.text);
        }

        [Test]
        public void Update_WhenStopwatchIsNotRunning_DoesNotUpdateElapsedTimeText()
        {
            _stopwatchManager.IsStopwatchRunning.Value = false;
            _elapsedTimeText.text = "Initial Text";

            Assert.AreEqual("Initial Text", _elapsedTimeText.text);
        }

        [Test]
        public void Update_WhenStopwatchIsRunningWithMilliseconds_UpdatesElapsedTimeTextWithMilliseconds()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            _stopwatchManager.ElapsedMilliseconds.Value = 65123;

            Assert.AreEqual("01:05.12", _elapsedTimeText.text);
        }
    }
}
