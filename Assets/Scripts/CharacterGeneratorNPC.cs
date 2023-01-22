using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneratorNPC : MonoBehaviour
{
    public Randomizer randHead;
    public Randomizer randEyes;
    public Randomizer randMouth;
    public Randomizer randNose;
    public Randomizer randShirt;
    public Randomizer randHair;
    public Randomizer randAccessories;


    // Start is called before the first frame update
    void Start()
    {
        GenerateCharacter();
    }


    public void GenerateCharacter()
    {
        randHead.GetRandomSprite();
        randEyes.GetRandomSprite();
        randMouth.GetRandomSprite();
        randNose.GetRandomSprite();
        randShirt.GetRandomSprite();
        randHair.GetRandomSprite();
        randAccessories.GetRandomSprite();


    }

}
