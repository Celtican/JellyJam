using E7.Introloop;
using UnityEngine;

public class FadeOutOnDemand : MonoBehaviour
{
    // again, for animators
    
    public float duration;
    public IntroloopPlayer player;

    public void FadeOut()
    {
        player.Stop(duration);
    }
}
