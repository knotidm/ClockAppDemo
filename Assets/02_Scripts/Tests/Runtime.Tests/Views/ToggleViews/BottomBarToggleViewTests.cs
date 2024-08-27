using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class BottomBarToggleViewTests : ZenjectIntegrationTestFixture
    {
        private BottomBarToggleView _bottomBarToggleView;
        private MainScreenPresenter _mainScreenPresenter;
        private Toggle _toggle;

        private ScreenView[] _screenViews;

        [SetUp]
        public void CommonInstall()
        {
            _screenViews = new ScreenView[3];

            for (int i = 0; i < 3; i++)
            {
                var screenViewObject = new GameObject();
                var screenView = screenViewObject.AddComponent<ScreenView>();
                screenView.ScreenType = (ScreenType)i;
                _screenViews[i] = screenView;
            }

            _mainScreenPresenter = new GameObject().AddComponent<MainScreenPresenter>();
            _mainScreenPresenter._screenViews = _screenViews;
            _toggle = new GameObject().AddComponent<Toggle>();

            PreInstall();

            Container.Bind<IMainScreenPresenter>().FromInstance(_mainScreenPresenter);
            Container.Bind<BottomBarToggleView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _bottomBarToggleView = Container.Resolve<BottomBarToggleView>();
            _bottomBarToggleView._toggle = _toggle;
            _bottomBarToggleView.Start();
        }

        [Test]
        public void Start_WhenToggleIsOn_SelectsToggle()
        {
            _toggle.isOn = true;
            Assert.IsTrue(_toggle.isOn);
        }

        [Test]
        public void Start_WhenToggleIsOff_DoesNotSelectToggle()
        {
            _toggle.isOn = false;
            Assert.IsFalse(_toggle.isOn);
        }

        [Test]
        public void ToggleValueChanged_WhenTurnedOn_CallsChangeToScreen()
        {
            _mainScreenPresenter._screenViews[0].gameObject.SetActive(false);
            _mainScreenPresenter._screenViews[1].gameObject.SetActive(false);
            _mainScreenPresenter._screenViews[2].gameObject.SetActive(false);

            _bottomBarToggleView._screenType = ScreenType.TimerScreen;
            _toggle.isOn = true;

            Assert.IsTrue(_mainScreenPresenter._screenViews[1].gameObject.activeSelf);
        }

        [Test]
        public void ToggleValueChanged_WhenTurnedOff_DoesNotCallChangeToScreen()
        {
            _mainScreenPresenter._screenViews[0].gameObject.SetActive(false);
            _mainScreenPresenter._screenViews[1].gameObject.SetActive(false);
            _mainScreenPresenter._screenViews[2].gameObject.SetActive(false);

            _bottomBarToggleView._screenType = ScreenType.TimerScreen;
            _toggle.isOn = false;

            Assert.IsFalse(_mainScreenPresenter._screenViews[1].gameObject.activeSelf);
        }
    }
}