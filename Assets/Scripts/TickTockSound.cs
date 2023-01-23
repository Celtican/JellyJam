using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TickTockSound : MonoBehaviour
{
    public float timeBetweenSounds;
    public AudioClip tickSound;
    public AudioClip tockSound;

    private float timeUntilSound = 1;
    private bool tick;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timeUntilSound -= Time.deltaTime;
        if (timeUntilSound <= 0)
        {
            timeUntilSound = timeBetweenSounds;
            
            audioSource.PlayOneShot(tick ? tickSound : tockSound);
            tick = !tick;
        }
    }
}
