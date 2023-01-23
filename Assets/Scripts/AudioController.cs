using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    
    private static float soundVolume = 1;

    public AudioMixer audioMixer;
    
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(Audio audio)
    {
        audioSource.volume = audio.volume + Random.Range(-audio.randomVolume, audio.randomVolume);
        // audioSource.pitch = audio.pitch + Random.Range(-audio.randomPitch, audio.randomPitch);
        audioSource.pitch = 1; // do I like it? nah. is it a jam? ya.
        audioSource.PlayOneShot(audio.clip);
    }

    public void SetVolume(float percent)
    {
        soundVolume = percent;
        audioMixer.SetFloat("Volume", PercentToDecibels(percent));
    }

    public float GetVolume()
    {
        return soundVolume;
    }

    public float PercentToDecibels(float percent)
    {
        return percent == 0 ? -144 : Mathf.Log10(percent) * 20;
    }

    public float DecibelsToPercent(float decibels)
    {
        return Mathf.Pow(10, decibels/20f);
    }
    
    [Serializable]
    public class Audio
    {
        public AudioClip clip;
        public float volume = 1;
        public float pitch = 1;
        public float randomVolume = 0.1f;
        public float randomPitch = 0.1f;

        public Audio(AudioClip clip, float volume, float pitch, float randomVolume, float randomPitch)
        {
            this.clip = clip;
            this.volume = volume;
            this.pitch = pitch;
            this.randomVolume = randomVolume;
            this.randomPitch = randomPitch;
        }
    }
}
