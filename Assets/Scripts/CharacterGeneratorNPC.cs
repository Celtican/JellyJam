using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneratorNPC : MonoBehaviour
{

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
    }

}
