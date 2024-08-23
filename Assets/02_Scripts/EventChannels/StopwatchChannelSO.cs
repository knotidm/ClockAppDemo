using UniRx;
using UnityEngine;

namespace ClockAppDemo
{

    [CreateAssetMenu(fileName = "StopwatchChannelSO", menuName = "Scriptable Objects/StopwatchChannelSO")]
    public class StopwatchChannelSO : ScriptableObject
    {
        public BoolReactiveProperty IsStopwatchCreated = new BoolReactiveProperty(false);
        public BoolReactiveProperty IsStopwatchPlaying = new BoolReactiveProperty(false);
    }
}