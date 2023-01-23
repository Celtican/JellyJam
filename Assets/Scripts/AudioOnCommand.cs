using UnityEngine;

public class AudioOnCommand : MonoBehaviour
{
    public AudioController.Audio audio;
    
    // intended for animations/buttons

    public void PlayAudio()
    {
        PlayAudio(audio);
    }

    public void PlayAudio(AudioController.Audio audio)
    {
        AudioController.Instance.PlayAudio(audio);
    }
}
