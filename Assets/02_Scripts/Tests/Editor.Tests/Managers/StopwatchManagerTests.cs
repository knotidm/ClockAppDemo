using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ClockAppDemo.Tests
{
    [TestFixture]
    public class StopwatchManagerTests
    {
        private Stopwatch _stopwatch;
        private StopwatchManager _stopwatchManager;

        [SetUp]
        public void CommonInstall()
        {
            _stopwatch = new Stopwatch();
            _stopwatchManager = new StopwatchManager(_stopwatch);
        }

        [TearDown]
        public void TearDown()
        {
            _stopwatch = null;
            _stopwatchManager = null;
        }

        [Test]
        public void Initialize_SetsInitialValues()
        {
            Assert.IsFalse(_stopwatchManager.IsStopwatchCreated.Value);
            Assert.IsFalse(_stopwatchManager.IsStopwatchRunning.Value);
        }

        [Test]
        public async Task Run_StartsStopwatchAndUpdatesElapsedMilliseconds()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;

            await Task.Delay(100);

            UnityEngine.Debug.Log(_stopwatchManager.IsStopwatchCreated.Value);
            UnityEngine.Debug.Log(_stopwatchManager.IsStopwatchRunning.Value);
            UnityEngine.Debug.Log(_stopwatchManager.ElapsedMilliseconds);

            Assert.IsTrue(_stopwatchManager.IsStopwatchCreated.Value);
            Assert.IsTrue(_stopwatchManager.IsStopwatchRunning.Value);
            Assert.Greater(_stopwatchManager.ElapsedMilliseconds, 0);
        }

        [Test]
        public async Task Pause_StopsStopwatch()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            await Task.Delay(100);
            _stopwatchManager.IsStopwatchRunning.Value = false;

            long pausedTime = _stopwatchManager.ElapsedMilliseconds;
            await Task.Delay(100);

            Assert.AreEqual(pausedTime, _stopwatchManager.ElapsedMilliseconds);
        }

        [Test]
        public async Task Resume_ContinuesStopwatch()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            await Task.Delay(100);
            _stopwatchManager.IsStopwatchRunning.Value = false;

            long pausedTime = _stopwatchManager.ElapsedMilliseconds;
            await Task.Delay(100);

            _stopwatchManager.IsStopwatchRunning.Value = true;
            await Task.Delay(100);

            Assert.Greater(_stopwatchManager.ElapsedMilliseconds, pausedTime);
        }

        [Test]
        public async Task Stop_ResetsStopwatch()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            await Task.Delay(100);
            _stopwatchManager.IsStopwatchCreated.Value = false;

            Assert.AreEqual(0, _stopwatchManager.ElapsedMilliseconds);
            Assert.IsFalse(_stopwatchManager.IsStopwatchCreated.Value);
            Assert.IsFalse(_stopwatchManager.IsStopwatchRunning.Value);
        }

        [Test]
        public async Task ElapsedMilliseconds_IncreasesAsStopwatchRuns()
        {
            _stopwatchManager.IsStopwatchRunning.Value = true;
            await Task.Delay(100);
            Assert.Greater(_stopwatchManager.ElapsedMilliseconds, 100);
            Assert.Less(_stopwatchManager.ElapsedMilliseconds, 500);
        }
    }
}
