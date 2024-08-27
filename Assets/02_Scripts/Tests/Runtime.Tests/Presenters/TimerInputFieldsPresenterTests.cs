using NUnit.Framework;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class TimerInputFieldsPresenterTests : ZenjectIntegrationTestFixture
    {
        private TimerInputFieldsPresenter _timerInputFieldsPresenter;
        private TimerManager _timerManager;
        private TMP_InputField[] _inputFields;

        [SetUp]
        public void CommonInstall()
        {
            Stopwatch stopwatch = new Stopwatch();
            _timerManager = new TimerManager(stopwatch);

            _inputFields = new TMP_InputField[3];

            for (int i = 0; i < 3; i++)
            {
                _inputFields[i] = new GameObject().AddComponent<TMP_InputField>();
            }

            PreInstall();

            Container.Bind<TimerInputFieldsPresenter>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _timerInputFieldsPresenter = Container.Resolve<TimerInputFieldsPresenter>();
            _timerInputFieldsPresenter.Initialize(_timerManager);
            _timerInputFieldsPresenter._inputFields = _inputFields;
        }

        [Test]
        public void GetTimerInitialTime_ValidInput_ReturnsCorrectSeconds()
        {
            _inputFields[0].text = "2";
            _inputFields[1].text = "30";
            _inputFields[2].text = "15";

            int result = _timerInputFieldsPresenter.GetTimerInitialTime();

            Assert.AreEqual(9015, result);
        }

        [Test]
        public void GetTimerInitialTime_ZeroInput_ReturnsZero()
        {
            _inputFields[0].text = "0";
            _inputFields[1].text = "0";
            _inputFields[2].text = "0";

            int result = _timerInputFieldsPresenter.GetTimerInitialTime();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetTimerInitialTime_MaxValues_ReturnsCorrectSeconds()
        {
            _inputFields[0].text = "99";
            _inputFields[1].text = "99";
            _inputFields[2].text = "99";

            int result = _timerInputFieldsPresenter.GetTimerInitialTime();

            Assert.AreEqual(362439, result);
        }

        [Test]
        public void SetActive_True_EnablesGameObject()
        {
            _timerInputFieldsPresenter.SetActive(true);

            Assert.IsTrue(_timerInputFieldsPresenter.gameObject.activeSelf);
        }

        [Test]
        public void SetActive_False_DisablesGameObject()
        {
            _timerInputFieldsPresenter.SetActive(false);

            Assert.IsFalse(_timerInputFieldsPresenter.gameObject.activeSelf);
        }

        [Test]
        public void Start_TimerCreated_DisablesPresenter()
        {
            _timerManager.IsTimerCreated.Value = true;

            Assert.IsFalse(_timerInputFieldsPresenter.gameObject.activeSelf);
        }

        [Test]
        public void Start_TimerNotCreated_EnablesPresenter()
        {
            _timerManager.IsTimerCreated.Value = false;

            Assert.IsTrue(_timerInputFieldsPresenter.gameObject.activeSelf);
        }

        [Test]
        public void Start_TimerCreated_SetsInitialTime()
        {
            _inputFields[0].text = "1";
            _inputFields[1].text = "30";
            _inputFields[2].text = "0";

            _timerManager.IsTimerCreated.Value = true;

            Assert.AreEqual(5400, _timerInputFieldsPresenter.GetTimerInitialTime());
        }
    }
}
