using UniRx;
using UnityEngine;
using Zenject;

namespace ClockAppDemo
{
    public class AudioPlayer : MonoBehaviour
    {
        [Inject] private readonly TimerManager _timerManager;

        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            _timerManager.IsTimerCreated.Subscribe(isTimerCreated => _audioSource.Stop()).AddTo(this);

            _timerManager.OnTimerFinished += PlayAudioSource;
        }

        private void OnDestroy()
        {
            _timerManager.OnTimerFinished -= PlayAudioSource;
        }

        private void PlayAudioSource()
        {
            _audioSource.Play();
        }
    }
}