using E7.Introloop;
using UnityEngine;

public class StopMainMenuMusic : MonoBehaviour
{
    public IntroloopPlayer audioPlayer;
    public float fadeDuration = 6f;

    public void Stop()
    {
        audioPlayer.Stop(fadeDuration);
    }
}