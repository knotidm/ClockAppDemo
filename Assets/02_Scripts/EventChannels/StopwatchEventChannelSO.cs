using UniRx;
using UnityEngine;

namespace ClockAppDemo
{
    [CreateAssetMenu(fileName = "StopwatchEventChannelSO", menuName = "Scriptable Objects/StopwatchEventChannelSO")]
    public class StopwatchEventChannelSO : ScriptableObject
    {
        public BoolReactiveProperty IsStopwatchCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsStopwatchPlaying = new BoolReactiveProperty(false);
    }
}