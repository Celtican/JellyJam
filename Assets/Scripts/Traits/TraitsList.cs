using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static Traits;
using Random = UnityEngine.Random;


[Serializable]
public class TraitsList : MonoBehaviour
{
    // renamed to lowercase
    [FormerlySerializedAs("Athletic")] public Trait athletic;
    [FormerlySerializedAs("Nerdy")] public Trait nerdy;
    [FormerlySerializedAs("Romantic")] public Trait romantic;
    [FormerlySerializedAs("Funny")] public Trait funny;
    [FormerlySerializedAs("AnimalLover")] public Trait animalLover;

    public bool isNpc;

    private void Start()
    {
        if (isNpc) Randomize();
    }

    public void Randomize()
    {
        athletic.value = Random.Range(1, 10);
        nerdy.value = Random.Range(1, 10);
        romantic.value = Random.Range(1, 10);
        funny.value = Random.Range(1, 10);
        animalLover.value = Random.Range(1, 10);
    }

    public Trait GetTraitOfType(Traits.Type type)
    {
        switch (type)
        {
            case Traits.Type.Athletic:
                return athletic;
            case Traits.Type.Nerdy:
                return nerdy;
            case Traits.Type.Romantic:
                return romantic;
            case Traits.Type.Funny:
                return funny;
            case Traits.Type.AnimalLover:
                return animalLover;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
