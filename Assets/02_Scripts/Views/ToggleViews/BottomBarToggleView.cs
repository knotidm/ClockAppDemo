using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClockAppDemo
{
    public class BottomBarToggleView : MonoBehaviour
    {
        [Inject] private readonly IMainScreenPresenter _mainScreenPresenter;

        [SerializeField] public Toggle _toggle;
        [SerializeField] public ScreenType _screenType;

        public void Start()
        {
            if (_toggle.isOn)
            {
                _toggle.Select();
            }

            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (isOn)
                {
                    _mainScreenPresenter.ChangeToScreen(_screenType);
                }

            }).AddTo(this);
        }
    }
}