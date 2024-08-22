using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MainScreenPresenter _mainScreenPresenter;

        public override void InstallBindings()
        {
            Container
                .Bind<IMainScreenPresenter>()
                .FromInstance(_mainScreenPresenter);
        }
    }
}