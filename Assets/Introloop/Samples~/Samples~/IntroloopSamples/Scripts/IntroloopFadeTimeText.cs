using UnityEngine;
using UnityEngine.UI;

namespace E7.IntroloopDemo
{
    internal class IntroloopFadeTimeText : MonoBehaviour
    {
        [SerializeField] private Slider fadeTimeSlider;
        [SerializeField] private Text text;

        private void Start()
        {
            UpdateFadeTimeText();
        }

        public void UpdateFadeTimeText()
        {
            text.text = fadeTimeSlider.value.ToString("0.00") + (fadeTimeSlider.value == 0 ? " second" : " seconds");
        }
    }
}