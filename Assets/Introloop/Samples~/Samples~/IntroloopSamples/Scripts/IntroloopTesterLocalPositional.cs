using E7.Introloop;
using UnityEngine;

namespace E7.IntroloopDemo
{
    internal class IntroloopTesterLocalPositional : MonoBehaviour
    {
        [SerializeField] private IntroloopPlayer leftSphere;
        [SerializeField] private GameObject rightSphere;

        [SerializeField] private IntroloopAudio forRightSphere;

        // You can attach a local IntroloopPlayer beforehand.
        // Left sphere already have an IntroloopPlayer, and the necessary child object will be spawned for you
        // when entering play mode.

        // Or you could attach it later.
        // Right sphere does not already have anything yet, but we can still use `.AddComponent`
        // and then the `Awake` code of it will prepare child objects.

        public void Awake()
        {
            var rightIntroloopPlayer = rightSphere.AddComponent<IntroloopPlayer>();
            var template = rightSphere.AddComponent<AudioSource>();
            template.spatialBlend = 1;
            rightIntroloopPlayer.TemplateSource = template;

            // If you don't do all that on awake, it is still possible to modify the template then apply again like this :
            var player = rightSphere.GetComponent<IntroloopPlayer>();
            player.TemplateSource.bypassEffects = true;
            player.ApplyAudioSource(player.TemplateSource);
        }

        public void LeftPlay()
        {
            // No argument, since the IntroloopAudio is connected to "Default Introloop Audio" slot in the Inspector.
            leftSphere.Play();
        }

        public void LeftStop()
        {
            leftSphere.Stop();
        }

        public void RightPlay()
        {
            rightSphere.GetComponent<IntroloopPlayer>().Play(forRightSphere);
        }

        public void RightStop()
        {
            rightSphere.GetComponent<IntroloopPlayer>().Stop();
        }
    }
}