using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    private SpriteRenderer rend;
    private int headSpriteNum;
    public Sprite currentHeadSprite;
    public Sprite[] headSprites;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        currentHeadSprite = headSprites[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHeadSprite()
    {
        headSpriteNum = Random.Range(0, headSprites.Length);
        Debug.Log(headSpriteNum);
        currentHeadSprite = headSprites[headSpriteNum];
        rend.sprite = currentHeadSprite;
    }

    public void ChangeEyeSprite()
    {
        
    }
}
