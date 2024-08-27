using NUnit.Framework;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class StartOrPauseTimerToggleViewTests : ZenjectIntegrationTestFixture
    {
        private StartOrPauseTimerToggleView _startOrPauseTimerToggleView;
        private TimerManager _timerManager;
        private Toggle _toggle;
        private Image _toggleOnIcon;
        private Image _toggleOffIcon;

        [SetUp]
        public void CommonInstall()
        {
            Stopwatch stopwatch = new Stopwatch();
            _timerManager = new TimerManager(stopwatch);
            _timerManager.SetInitialTimeInSeconds(5);

            _toggle = new GameObject().AddComponent<Toggle>();
            _toggleOnIcon = new GameObject().AddComponent<Image>();
            _toggleOffIcon = new GameObject().AddComponent<Image>();

            PreInstall();

            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
            Container.Bind<StartOrPauseTimerToggleView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _startOrPauseTimerToggleView = Container.Resolve<StartOrPauseTimerToggleView>();
            _startOrPauseTimerToggleView._toggle = _toggle;
            _startOrPauseTimerToggleView._toggleOnIcon = _toggleOnIcon;
            _startOrPauseTimerToggleView._toggleOffIcon = _toggleOffIcon;
            _startOrPauseTimerToggleView.Start();
        }

        [Test]
        public void StartPause_ToggleIsOnOff_SetsTimerRunningAndStops()
        {
            _toggle.isOn = true;
            Assert.IsTrue(_timerManager.IsTimerRunning.Value);
            _toggle.isOn = false;
            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
        }

        [Test]
        public void TimerManager_IsTimerCreated_UpdatesToggleValue()
        {
            _timerManager.IsTimerCreated.Value = true;
            Assert.IsTrue(_toggle.isOn);
            _timerManager.IsTimerCreated.Value = false;
            Assert.IsFalse(_toggle.isOn);
        }

    }
}
