using UnityEngine;

public class HeartAudioPlayer : MonoBehaviour
{
    public AudioController.Audio growAudio;
    public AudioController.Audio emptyAudio;

    public void PlayGrowAudio()
    {
        AudioController.Instance.PlayAudio(growAudio);
    }

    public void PlayEmptyAudio()
    {
        AudioController.Instance.PlayAudio(emptyAudio);
    }
}