using NUnit.Framework;
using TMPro;
using UnityEngine;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class RecordedTimeViewTests : ZenjectIntegrationTestFixture
    {
        private RecordedTimeView _recordedTimeView;
        private TMP_Text _numberText;
        private TMP_Text _lapTimeText;
        private TMP_Text _totalTimeText;

        [SetUp]
        public void CommonInstall()
        {
            _numberText = new GameObject().AddComponent<TextMeshProUGUI>();
            _lapTimeText = new GameObject().AddComponent<TextMeshProUGUI>();
            _totalTimeText = new GameObject().AddComponent<TextMeshProUGUI>();

            PreInstall();
            Container.Bind<RecordedTimeView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _recordedTimeView = Container.Resolve<RecordedTimeView>();
            _recordedTimeView._numberText = _numberText;
            _recordedTimeView._lapTimeText = _lapTimeText;
            _recordedTimeView._totalTimeText = _totalTimeText;

        }

        [Test]
        public void SetRecord_SetsCorrectValues()
        {
            _recordedTimeView.SetRecord(1, 65000, 120000);

            Assert.AreEqual("1", _numberText.text);
            Assert.AreEqual("01:05.00", _lapTimeText.text);
            Assert.AreEqual("02:00.00", _totalTimeText.text);
        }

        [Test]
        public void SetRecord_HandlesZeroValues()
        {
            _recordedTimeView.SetRecord(0, 0, 0);

            Assert.AreEqual("0", _numberText.text);
            Assert.AreEqual("00:00.00", _lapTimeText.text);
            Assert.AreEqual("00:00.00", _totalTimeText.text);
        }

        [Test]
        public void SetRecord_HandlesLargeValues()
        {
            _recordedTimeView.SetRecord(999, 3599999, 7199999);

            Assert.AreEqual("999", _numberText.text);
            Assert.AreEqual("59:59.99", _lapTimeText.text);
            Assert.AreEqual("59:59.99", _totalTimeText.text);
        }

        [Test]
        public void SetRecord_HandlesNullTexts()
        {
            _recordedTimeView._numberText = null;
            _recordedTimeView._lapTimeText = null;
            _recordedTimeView._totalTimeText = null;

            Assert.DoesNotThrow(() => _recordedTimeView.SetRecord(1, 1000, 2000));
        }
    }
}