using System.Diagnostics;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private MainScreenPresenter _mainScreenPresenter;
        [SerializeField] private TimerInputFieldsPresenter _inputFieldsPresenter;
        [SerializeField] private RecordedTimesPresenter _recordedTimesPresenter;

        public override void InstallBindings()
        {
            StopwatchManager stopwatchManager = new StopwatchManager(new Stopwatch());
            TimerManager timerManager = new TimerManager(new Stopwatch());

            _inputFieldsPresenter.Initialize(timerManager);

            Container
                .Bind<IMainScreenPresenter>()
                .FromInstance(_mainScreenPresenter);

            Container
                .Bind<IInputFieldsPresenter>()
                .FromInstance(_inputFieldsPresenter);

            Container
                .Bind<IRecordedTimesPresenter>()
                .FromInstance(_recordedTimesPresenter);

            Container.Bind<StopwatchManager>().FromInstance(stopwatchManager).AsSingle();
            Container.Bind<TimerManager>().FromInstance(timerManager).AsSingle();
        }
    }
}