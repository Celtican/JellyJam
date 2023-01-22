using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] spriteList;

    public Color[] colorList;
    
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void GetRandomSprite()
    {
        rend.sprite = spriteList[Random.Range(0, spriteList.Length)];
        rend.color = colorList[Random.Range(0, colorList.Length)];

    }


}
