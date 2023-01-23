using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterGeneratorNPC : MonoBehaviour
{
    public VoiceList[] maleVoices;
    public VoiceList[] femaleVoices;
    public float randomVoicePitchRange = 0.1f;
    public AudioRandomizer voiceAudioSource;
    public bool feminine;

    
    // Start is called before the first frame update
    void Start()
    {
        feminine = false;
        if (PlayerTraits.Instance != null) feminine = PlayerTraits.Instance.prefersFemales;
        GenerateCharacter();

    }


    public void FemininePref()
    {
        feminine = true;
    }
    public void universalPref()
    {
        feminine = false;
    }



    public void GenerateCharacter()
    {
        Randomizer[] randomizers = GetComponentsInChildren<Randomizer>();
        foreach (Randomizer randomizer in randomizers)
        {
            randomizer.GetRandomSprite(feminine);
        }
        if (voiceAudioSource!= null)
        {
            VoiceList[] voiceArray = feminine ? femaleVoices : maleVoices;
            voiceAudioSource.clipSelection = voiceArray[Random.Range(0, voiceArray.Length)].voices;
            voiceAudioSource.pitch = Random.Range(-randomVoicePitchRange, randomVoicePitchRange);
        }
        
      
    }

 

    // this is the only way to serialize a nested array. Stupid, I know, but alas

    [Serializable]
    public class VoiceList
    {
        public AudioClip[] voices;
    }
}
