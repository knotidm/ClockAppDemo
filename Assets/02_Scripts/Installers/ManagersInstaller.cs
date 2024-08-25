using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class ManagersInstaller : MonoInstaller
    {
        [SerializeField] private StopwatchManager _stopwatchManager;
        [SerializeField] private TimerManager _timerManager;

        public override void InstallBindings()
        {
            Container.Bind<StopwatchManager>().FromInstance(_stopwatchManager).AsSingle();
            Container.Bind<TimerManager>().FromInstance(_timerManager).AsSingle();
        }
    }
}