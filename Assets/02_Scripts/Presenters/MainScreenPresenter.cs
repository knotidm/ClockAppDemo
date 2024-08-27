using System.Linq;
using UnityEngine;

namespace ClockAppDemo
{
    public class MainScreenPresenter : MonoBehaviour, IMainScreenPresenter
    {
        [SerializeField] public ScreenView[] _screenViews;

        public void ChangeToScreen(ScreenType screenType)
        {
            ScreenView resultScreen = _screenViews?.SingleOrDefault(_ => _.ScreenType == screenType);

            if (resultScreen != null)
            {
                DisableScreens();
                resultScreen.gameObject.SetActive(true);
            }
        }

        public void DisableScreens()
        {
            for (int i = 0; i < _screenViews.Length; i++)
            {
                ScreenView screen = _screenViews[i];
                screen.gameObject.SetActive(false);
            }
        }
    }
}
