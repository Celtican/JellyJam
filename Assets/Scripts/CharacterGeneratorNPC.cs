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
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateCharacter();
    }


    public void GenerateCharacter()
    {
        Randomizer[] randomizers = GetComponentsInChildren<Randomizer>();
        foreach (Randomizer randomizer in randomizers)
        {
            randomizer.GetRandomSprite();
        }
        
        // todo: switch array based on player preference
        voiceAudioSource.clipSelection = femaleVoices[Random.Range(0, femaleVoices.Length)].voices;
        voiceAudioSource.pitch = Random.Range(-randomVoicePitchRange, randomVoicePitchRange);
    }

    // this is the only way to serialize a nested array. Stupid, I know, but alas
    [Serializable]
    public class VoiceList
    {
        public AudioClip[] voices;
    }
}
