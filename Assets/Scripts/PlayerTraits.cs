using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Traits;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class PlayerTraits : MonoBehaviour
{
    public Traits playerTraits;
    public TraitsList playerTraitsList;



    

    // Start is called before the first frame update
    void Start()
    {
        playerTraits = GetComponent<Traits>();
        playerTraitsList = playerTraits.GetComponent<TraitsList>();
        playerTraitsList.Athletic.value = 5; 
        playerTraitsList.Nerdy.value = 5;
        playerTraitsList.Weird.value = 5;
        playerTraitsList.Funny.value = 5;
        playerTraitsList.AnimalLover.value = 5;
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateAthletics()
    {
        playerTraitsList.Athletic.value = 10;
    }
    void UpdateNerdy()
    {
        playerTraitsList.Nerdy.value = 10;
    }
    void UpdateWeird()
    {
        playerTraitsList.Weird.value = 10;
    }
    void UpdateFunny()
    {
        playerTraitsList.Funny.value = 10;
    }
    void UpdateAnimalLover()
    {
        playerTraitsList.AnimalLover.value = 10;
    }


}
