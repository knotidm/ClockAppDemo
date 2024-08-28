using System;
using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class ClockCurrentTimeTextView : MonoBehaviour
    {
        [SerializeField] public TMP_Text _currentTimeText;
        [SerializeField] public TMP_Text _currentTimeZoneText;

        public void Update()
        {
            _currentTimeText.text = DateTime.Now.ToString("HH:mm:ss");
            _currentTimeZoneText.text = TimeZoneInfo.Local.DaylightName;
        }
    }
}
