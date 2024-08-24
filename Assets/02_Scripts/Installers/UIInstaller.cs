using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MainScreenPresenter _mainScreenPresenter;
        [SerializeField] private InputFieldsPresenter _inputFieldsPresenter;

        public override void InstallBindings()
        {
            Container
                .Bind<IMainScreenPresenter>()
                .FromInstance(_mainScreenPresenter);

            Container
                .Bind<IInputFieldsPresenter>()
                .FromInstance(_inputFieldsPresenter);

        }
    }
}