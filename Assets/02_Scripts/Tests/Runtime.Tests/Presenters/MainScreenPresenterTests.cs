using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class MainScreenPresenterTests : ZenjectIntegrationTestFixture
    {
        private MainScreenPresenter _mainScreenPresenter;
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

            PreInstall();

            Container.Bind<MainScreenPresenter>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _mainScreenPresenter = Container.Resolve<MainScreenPresenter>();
            _mainScreenPresenter._screenViews = _screenViews;
        }

        [Test]
        public void ChangeToScreen_ActivatesCorrectScreen()
        {
            _screenViews[0].gameObject.SetActive(false);

            _mainScreenPresenter.ChangeToScreen(ScreenType.ClockScreen);

            Assert.IsTrue(_screenViews[0].gameObject.activeSelf);
        }

        [Test]
        public void ChangeToScreen_DeactivatesOtherScreens()
        {
            _screenViews[1].gameObject.SetActive(true);
            _screenViews[2].gameObject.SetActive(true);

            _mainScreenPresenter.ChangeToScreen(ScreenType.ClockScreen);

            Assert.IsFalse(_screenViews[1].gameObject.activeSelf);
            Assert.IsFalse(_screenViews[2].gameObject.activeSelf);
        }

        [Test]
        public void DisableScreens_DeactivatesAllScreens()
        {
            foreach (var screenView in _screenViews)
            {
                screenView.gameObject.SetActive(true);
            }

            _mainScreenPresenter.DisableScreens();

            foreach (var screenView in _screenViews)
            {
                Assert.IsFalse(screenView.gameObject.activeSelf);
            }
        }

        [Test]
        public void ChangeToScreen_NonExistentScreen_DoesNotChangeAnyScreen()
        {
            _screenViews[0].gameObject.SetActive(true);
            _screenViews[1].gameObject.SetActive(false);
            _screenViews[2].gameObject.SetActive(false);

            _mainScreenPresenter.ChangeToScreen((ScreenType)99);

            Assert.IsTrue(_screenViews[0].gameObject.activeSelf);
            Assert.IsFalse(_screenViews[1].gameObject.activeSelf);
            Assert.IsFalse(_screenViews[2].gameObject.activeSelf);
        }
    }
}
