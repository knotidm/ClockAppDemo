using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MainScreenPresenter _mainScreenPresenter;
        [SerializeField] private TimerInputFieldsPresenter _inputFieldsPresenter;
        [SerializeField] private RecordedTimesPresenter _recordedTimesPresenter;

        public override void InstallBindings()
        {
            Container
                .Bind<IMainScreenPresenter>()
                .FromInstance(_mainScreenPresenter);

            Container
                .Bind<IInputFieldsPresenter>()
                .FromInstance(_inputFieldsPresenter);

            Container
                .Bind<IRecordedTimesPresenter>()
                .FromInstance(_recordedTimesPresenter);
        }
    }
}