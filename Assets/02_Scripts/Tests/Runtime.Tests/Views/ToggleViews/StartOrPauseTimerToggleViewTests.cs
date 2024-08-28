using NUnit.Framework;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class StartOrPauseTimerToggleViewTests : ZenjectIntegrationTestFixture
    {
        private TimerInputFieldsPresenter _timerInputFieldsPresenter;
        private TMP_InputField[] _inputFields;

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

            _inputFields = new TMP_InputField[3];

            for (int i = 0; i < 3; i++)
            {
                _inputFields[i] = new GameObject().AddComponent<TMP_InputField>();
            }

            _toggle = new GameObject().AddComponent<Toggle>();
            _toggleOnIcon = new GameObject().AddComponent<Image>();
            _toggleOffIcon = new GameObject().AddComponent<Image>();

            PreInstall();

            Container.Bind<TimerInputFieldsPresenter>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
            Container.Bind<StartOrPauseTimerToggleView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _timerInputFieldsPresenter = Container.Resolve<TimerInputFieldsPresenter>();

            Container.Bind<IInputFieldsPresenter>().FromInstance(_timerInputFieldsPresenter);

            _timerInputFieldsPresenter._inputFields = _inputFields;
            _timerInputFieldsPresenter.Start();

            _startOrPauseTimerToggleView = Container.Resolve<StartOrPauseTimerToggleView>();
            _startOrPauseTimerToggleView._toggle = _toggle;
            _startOrPauseTimerToggleView._toggleOnIcon = _toggleOnIcon;
            _startOrPauseTimerToggleView._toggleOffIcon = _toggleOffIcon;
            _startOrPauseTimerToggleView.Start();
        }

        [Test]
        public void StartPause_ToggleIsOnOff_SetsTimerRunningAndStops()
        {
            _inputFields[0].text = "2";
            _inputFields[1].text = "30";
            _inputFields[2].text = "15";

            _toggle.isOn = true;
            Assert.IsTrue(_timerManager.IsTimerRunning.Value);
            _toggle.isOn = false;
            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
        }

        [Test]
        public void TimerManager_IsTimerCreated_UpdatesToggleValue()
        {
            _inputFields[0].text = "2";
            _inputFields[1].text = "30";
            _inputFields[2].text = "15";

            _timerManager.IsTimerCreated.Value = true;
            Assert.IsTrue(_toggle.isOn);
            _timerManager.IsTimerCreated.Value = false;
            Assert.IsFalse(_toggle.isOn);
        }

    }
}
