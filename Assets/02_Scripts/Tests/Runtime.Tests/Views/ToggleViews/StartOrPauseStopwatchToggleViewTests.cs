using NUnit.Framework;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class StartOrPauseStopwatchToggleViewTests : ZenjectIntegrationTestFixture
    {
        private StartOrPauseStopwatchToggleView _startOrPauseStopwatchToggleView;
        private StopwatchManager _stopwatchManager;
        private Toggle _toggle;
        private Image _toggleOnIcon;
        private Image _toggleOffIcon;

        [SetUp]
        public void CommonInstall()
        {
            Stopwatch stopwatch = new Stopwatch();
            _stopwatchManager = new StopwatchManager(stopwatch);

            _toggle = new GameObject().AddComponent<Toggle>();
            _toggleOnIcon = new GameObject().AddComponent<Image>();
            _toggleOffIcon = new GameObject().AddComponent<Image>();

            PreInstall();

            Container.Bind<StopwatchManager>().FromInstance(_stopwatchManager).AsSingle();
            Container.Bind<StartOrPauseStopwatchToggleView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _startOrPauseStopwatchToggleView = Container.Resolve<StartOrPauseStopwatchToggleView>();
            _startOrPauseStopwatchToggleView._toggle = _toggle;
            _startOrPauseStopwatchToggleView._toggleOnIcon = _toggleOnIcon;
            _startOrPauseStopwatchToggleView._toggleOffIcon = _toggleOffIcon;
            _startOrPauseStopwatchToggleView.Start();

        }

        [Test]
        public void Start_WhenToggleIsOn_SetsStopwatchRunningAndCreated()
        {
            _toggle.isOn = true;

            Assert.IsTrue(_stopwatchManager.IsStopwatchRunning.Value);
            Assert.IsTrue(_stopwatchManager.IsStopwatchCreated.Value);
        }

        [Test]
        public void Pause_WhenToggleIsOff_SetsStopwatchRunningToFalse()
        {
            _toggle.isOn = false;

            Assert.IsFalse(_stopwatchManager.IsStopwatchRunning.Value);
        }
    }
}