using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace ClockAppDemo.Tests
{
    public class ClockCurrentTimeTextViewTests : ZenjectIntegrationTestFixture
    {
        private ClockCurrentTimeTextView _clockCurrentTimeTextView;
        private TMP_Text _currentTimeText;
        private TMP_Text _currentTimeZoneText;

        [SetUp]
        public void CommonInstall()
        {
            _currentTimeText = new GameObject().AddComponent<TextMeshProUGUI>();
            _currentTimeZoneText = new GameObject().AddComponent<TextMeshProUGUI>();

            PreInstall();
            Container.Bind<ClockCurrentTimeTextView>().FromNewComponentOnNewGameObject().AsSingle();

            PostInstall();

            _clockCurrentTimeTextView = Container.Resolve<ClockCurrentTimeTextView>();
            _clockCurrentTimeTextView._currentTimeText = _currentTimeText;
            _clockCurrentTimeTextView._currentTimeZoneText = _currentTimeZoneText;
        }

        [Test]
        public void Update_SetsCorrectTimeFormat()
        {
            _clockCurrentTimeTextView.Update();

            Assert.IsTrue(Regex.IsMatch(_currentTimeText.text, @"\d{2}:\d{2}:\d{2}", RegexOptions.IgnoreCase));
        }

        [UnityTest]
        public IEnumerator Update_UpdatesTimeEveryFrame()
        {
            _clockCurrentTimeTextView.Update();
            string firstUpdate = _currentTimeText.text;

            yield return new WaitForSeconds(1f);

            _clockCurrentTimeTextView.Update();
            string secondUpdate = _currentTimeText.text;

            Assert.AreNotEqual(firstUpdate, secondUpdate);
        }
    }
}
