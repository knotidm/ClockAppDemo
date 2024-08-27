using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ClockAppDemo
{
    public class MainScreenToggleView : MonoBehaviour
    {
        [SerializeField] public Toggle _toggle;
        [SerializeField] public Image _toggleOnIcon;
        [SerializeField] public Image _toggleOffIcon;

        public virtual void Start()
        {
            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if (isOn)
                {
                    _toggleOnIcon.gameObject.SetActive(false);
                    _toggleOffIcon.gameObject.SetActive(true);
                }
                else
                {
                    _toggleOnIcon.gameObject.SetActive(true);
                    _toggleOffIcon.gameObject.SetActive(false);
                }
            }).AddTo(this);
        }
    }
}
