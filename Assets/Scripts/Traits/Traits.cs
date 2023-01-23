using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Traits : MonoBehaviour
{
    [Serializable]
    public struct Trait 
    {
        [Range(1, 10)]
        public int value;
    }


    public float CalculateCompatibility(TraitsList playerTraits, TraitsList NPCtraits)
    {
        float compatibleTraits = 0;
        compatibleTraits += Math.Min(playerTraits.athletic.value, NPCtraits.athletic.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.nerdy.value, NPCtraits.nerdy.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.romantic.value, NPCtraits.romantic.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.funny.value, NPCtraits.funny.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.animalLover.value, NPCtraits.animalLover.value) / 10f;
        return compatibleTraits / 5f;
    }

    public enum Type
    {
        Athletic,
        Nerdy,
        Romantic,
        Funny,
        AnimalLover
    }
}
