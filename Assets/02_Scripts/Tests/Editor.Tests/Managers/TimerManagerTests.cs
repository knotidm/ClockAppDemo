using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ClockAppDemo.Tests
{
    [TestFixture]
    public class TimerManagerTests
    {
        private TimerManager _timerManager;
        private Stopwatch _stopwatch;

        [SetUp]
        public void CommonInstall()
        {
            _stopwatch = new Stopwatch();
            _timerManager = new TimerManager(_stopwatch);
            _timerManager.SetInitialTimeInSeconds(5);
        }

        [Test]
        public void Initialize_SetsInitialValues()
        {
            Assert.IsFalse(_timerManager.IsTimerCreated.Value);
            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
        }

        [Test]
        public async Task Run_StartsTimerAndUpdatesTimeToExpire()
        {
            _timerManager.IsTimerRunning.Value = true;

            await Task.Delay(100);

            Assert.IsTrue(_timerManager.IsTimerCreated.Value);
            Assert.IsTrue(_timerManager.IsTimerRunning.Value);
            Assert.Greater(_timerManager.TimeToExpire, 0);
        }

        [Test]
        public async Task Pause_StopsTimer()
        {
            _timerManager.IsTimerRunning.Value = true;
            await Task.Delay(100);
            _timerManager.IsTimerRunning.Value = false;
            long pausedTime = _timerManager.TimeToExpire;
            await Task.Delay(100);

            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
            Assert.AreEqual(pausedTime, _timerManager.TimeToExpire);
        }


        [Test]
        public async Task Resume_ContinuesTimer()
        {
            _timerManager.IsTimerRunning.Value = true;
            await Task.Delay(1000);
            _timerManager.IsTimerRunning.Value = false;
            long pausedTime = _timerManager.TimeToExpire;
            _timerManager.IsTimerRunning.Value = true;
            await Task.Delay(1000);
            Assert.IsTrue(_timerManager.IsTimerRunning.Value);
            Assert.Less(_timerManager.TimeToExpire, pausedTime);
        }


        [Test]
        public async Task Stop_ResetsTimer()
        {
            _timerManager.IsTimerRunning.Value = true;
            await Task.Delay(1000);
            _timerManager.IsTimerCreated.Value = false;

            UnityEngine.Debug.Log(_timerManager.TimeToExpire);

            Assert.AreEqual(0, _timerManager.TimeToExpire);
            Assert.IsFalse(_timerManager.IsTimerCreated.Value);
            Assert.IsFalse(_timerManager.IsTimerRunning.Value);
        }

        [Test]
        public async Task TimeToExpire_DecreasesAsTimerRuns()
        {
            _timerManager.SetInitialTimeInSeconds(5);
            _timerManager.IsTimerRunning.Value = true;

            Assert.AreEqual(5, _timerManager.TimeToExpire);

            await Task.Delay(2000);

            Assert.AreEqual(3, _timerManager.TimeToExpire);
            Assert.Less(_timerManager.TimeToExpire, 5);
            Assert.GreaterOrEqual(_timerManager.TimeToExpire, 0);
        }
    }
}
