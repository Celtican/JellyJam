using E7.Introloop;
using UnityEngine;

namespace E7.IntroloopDemo
{
    internal class IntroloopTesterSubclass : MonoBehaviour
    {
        [SerializeField] private IntroloopAudio maidBattle;

        [Space]
        [SerializeField] private IntroloopAudio assault;

        [Range(0, 2f)] [SerializeField] private float assaultFade;

        [Space] [SerializeField] private IntroloopAudio compete;

        [Range(0, 2f)] [SerializeField] private float competeFade;

        [Space]
        [Tooltip("This is not connected to anything in the scene, but to the prefab asset that" +
                 "has AudioSource on it as a template. Now it is easy to adjust your template " +
                 "from the Project pane.")]
        [SerializeField] private AudioSource templateForDemoSubclassPlayer;

        public void Awake()
        {
            // If this is called before the first access to the singleton instance,
            // it can start with useful settings such as having output AudioMixerGroup.
            DemoSubclassPlayer.SetSingletonInstanceTemplateSource(templateForDemoSubclassPlayer);
        }

        public void LeftPlay()
        {
            IntroloopPlayer.Instance.Play(maidBattle);
        }

        public void LeftPause()
        {
            IntroloopPlayer.Instance.Pause(0.5f);
        }

        public void LeftResume()
        {
            IntroloopPlayer.Instance.Resume();
        }

        public void LeftStop()
        {
            IntroloopPlayer.Instance.Stop(0.8f);
        }

        public void RightPlay1()
        {
            DemoSubclassPlayer.Instance.Play(assault, assaultFade);
        }

        public void RightPlay2()
        {
            DemoSubclassPlayer.Instance.Play(compete, competeFade);
        }

        public void RightPause()
        {
            DemoSubclassPlayer.Instance.Pause();
        }

        public void RightResume()
        {
            DemoSubclassPlayer.Instance.Resume();
        }

        public void RightStop()
        {
            DemoSubclassPlayer.Instance.Stop();
        }
    }
}