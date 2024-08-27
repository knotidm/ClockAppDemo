using NUnit.Framework;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class ResetTimerButtonViewTests : ZenjectIntegrationTestFixture
    {
        private ResetTimerButtonView _resetTimerButtonView;
        private TimerManager _timerManager;
        private Button _button;

        [SetUp]
        public void CommonInstall()
        {
            Stopwatch stopwatch = new Stopwatch();
            _timerManager = new TimerManager(stopwatch);
            _button = new GameObject().AddComponent<Button>();

            PreInstall();

            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
            Container.Bind<ResetTimerButtonView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _resetTimerButtonView = Container.Resolve<ResetTimerButtonView>();
            _resetTimerButtonView._button = _button;
            _resetTimerButtonView.Start();
        }

        [Test]
        public void Start_WhenTimerIsCreated_ShouldEnableButton()
        {
            _timerManager.IsTimerCreated.Value = true;
            Assert.IsTrue(_button.interactable);
        }

        [Test]
        public void Start_WhenTimerIsNotCreated_ShouldDisableButton()
        {
            _timerManager.IsTimerCreated.Value = false;
            Assert.IsFalse(_button.interactable);
        }

        [Test]
        public void Start_WhenButtonClicked_ShouldResetTimerState()
        {
            _button.onClick.Invoke();

            Assert.IsFalse(_timerManager.IsTimerCreated.Value);
            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
            Assert.IsFalse(_button.interactable);
        }
    }
}