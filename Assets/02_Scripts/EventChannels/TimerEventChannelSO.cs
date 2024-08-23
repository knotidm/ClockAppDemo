using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    [CreateAssetMenu(fileName = "TimerEventChannelSO", menuName = "Scriptable Objects/TimerEventChannelSO")]
    public class TimerEventChannelSO : ScriptableObject
    {
        public BoolReactiveProperty IsTimerCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsTimerPlaying = new BoolReactiveProperty(false);
    }
}