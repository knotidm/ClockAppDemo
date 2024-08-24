using System;
using TMPro;
using UnityEngine;

namespace ClockAppDemo
{
    public class ClockCurrentTimeTextView : MonoBehaviour
    {
        [SerializeField] TMP_Text _currentTimeText;

        private void Update()
        {
            _currentTimeText.text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
