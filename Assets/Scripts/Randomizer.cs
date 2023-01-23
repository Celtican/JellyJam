using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] universalspriteList;
    public Sprite[] feminineList;


    public Color[] colorList;

    
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
      
    }

 

    public void GetRandomSprite(bool preference)
    {
        if(preference == true)
        {
            rend.sprite = feminineList[Random.Range(0, feminineList.Length)];
            rend.color = colorList[Random.Range(0, colorList.Length)];
        }
        else
        {
            rend.sprite = universalspriteList[Random.Range(0, universalspriteList.Length)];
            rend.color = colorList[Random.Range(0, colorList.Length)];
        }
        
      

    }


}
