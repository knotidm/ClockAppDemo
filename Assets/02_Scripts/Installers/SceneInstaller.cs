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
            Container
                .Bind<IMainScreenPresenter>()
                .FromInstance(_mainScreenPresenter);

            Container
                .Bind<IInputFieldsPresenter>()
                .FromInstance(_inputFieldsPresenter);

            Container
                .Bind<IRecordedTimesPresenter>()
                .FromInstance(_recordedTimesPresenter);

            StopwatchManager _stopwatchManager = new StopwatchManager(new Stopwatch());
            TimerManager _timerManager = new TimerManager(new Stopwatch());

            Container.Bind<StopwatchManager>().FromInstance(_stopwatchManager).AsSingle();
            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
        }
    }
}