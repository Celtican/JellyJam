using UnityEngine;
using UnityEngine.UI;

namespace E7.IntroloopDemo
{
    internal class IntroloopTestSwitches : MonoBehaviour
    {
        [SerializeField] private bool useFade;
        [SerializeField] private float fadeTime;

        [SerializeField] private Toggle fadeToggle;

        [SerializeField] private Slider fadeTimeSlider;
        internal bool UsingFade => useFade;
        internal float FadeTime => fadeTime;

        public void Awake()
        {
            UseFade();
            UpdateFadeTime();
        }

        public void UseFade()
        {
            useFade = fadeToggle.isOn;
        }

        public void UpdateFadeTime()
        {
            fadeTime = fadeTimeSlider.value;
        }
    }
}