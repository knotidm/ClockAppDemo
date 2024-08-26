using System.Linq;
using UnityEngine;

namespace ClockAppDemo
{
    public class MainScreenPresenter : MonoBehaviour, IMainScreenPresenter
    {
        [SerializeField] public ScreenView[] _screenViews;

        public void ChangeToScreen(ScreenType screenType)
        {
            DisableScreens();
            _screenViews.SingleOrDefault(_ => _.ScreenType == screenType).gameObject.SetActive(true);
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
