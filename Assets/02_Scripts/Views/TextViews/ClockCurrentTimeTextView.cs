using System;
using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class ClockCurrentTimeTextView : MonoBehaviour
    {
        [SerializeField] public TMP_Text _currentTimeText;

        public void Update()
        {
            //Debug.Log(TimeZoneInfo.Local);
            _currentTimeText.text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
