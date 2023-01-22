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
        compatibleTraits += Math.Min(playerTraits.Athletic.value, NPCtraits.Athletic.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.Nerdy.value, NPCtraits.Nerdy.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.Romantic.value, NPCtraits.Romantic.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.Funny.value, NPCtraits.Funny.value) / 10f;
        compatibleTraits += Math.Min(playerTraits.AnimalLover.value, NPCtraits.AnimalLover.value) / 10f;
        return compatibleTraits / 5f;
    }
}
