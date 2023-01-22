using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    private SpriteRenderer rend;


    public Sprite[] spriteList;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GetRandomSprite();  
    }

    public void GetRandomSprite()
    {
        rend.sprite = spriteList[Random.Range(0, spriteList.Length)];
    }
}
