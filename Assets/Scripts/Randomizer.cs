using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Randomizer : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] masculineList;
    public Sprite[] feminineList;
    
    public Color[] colorList;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
      
    }

    public void GetRandomSprite(bool prefersFeminine)
    {
        if (prefersFeminine)
        {
            rend.sprite = feminineList[Random.Range(0, feminineList.Length)];
            rend.color = colorList[Random.Range(0, colorList.Length)];
        }
        else
        {
            rend.sprite = masculineList[Random.Range(0, masculineList.Length)];
            rend.color = colorList[Random.Range(0, colorList.Length)];
        }
    }


}
