using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioRandomizer : MonoBehaviour
{
    public AudioClip[] clipSelection;
    public float volume = 1;
    public float randomVolume = 0.1f;
    public float pitch = 1;
    public float randomPitch = 0.1f;

    private new AudioController.Audio audio;

    public void Awake()
    {
        audio = new AudioController.Audio(clipSelection[0], volume, pitch, randomVolume, randomPitch);
    }

    public void Play()
    {
        audio.clip = clipSelection[Random.Range(0, clipSelection.Length)];
        AudioController.Instance.PlayAudio(audio);
    }
}
