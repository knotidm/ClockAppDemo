using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    [CreateAssetMenu(fileName = "TimerChannelSO", menuName = "Scriptable Objects/TimerChannelSO")]
    public class TimerChannelSO : ScriptableObject
    {
        public BoolReactiveProperty IsTimerCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsTimerPlaying = new BoolReactiveProperty(false);
    }
}