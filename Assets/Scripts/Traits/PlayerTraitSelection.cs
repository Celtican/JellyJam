using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTraitSelection : MonoBehaviour
{
    public GameObject LikedTraits1;
    public GameObject LikedTraits2;
    public GameObject DislikedTrait;
    public PlayerTraits playerTraits;
    public TraitsList traitsList;
    string selectedTraitName;
    string LikedTrait1; 
    string LikedTrait2;

    private void Start()
    {
        LikedTraits1.SetActive(true);
        LikedTraits2.SetActive(false);
        DislikedTrait.SetActive(false);


    }
    public void IncreaseTrait(Button button)
    {
        selectedTraitName = button.GetComponentInChildren<TextMeshProUGUI>().text;
        switch (selectedTraitName)
        {
            case "Athletic":
                traitsList.Athletic.value = 10;
                
                SwitchMenu();
                break;
            case "Nerdy":
                traitsList.Nerdy.value = 10;
                SwitchMenu();
                break;
            case "Romantic":
                traitsList.Romantic.value = 10;
                SwitchMenu();
                break;
            case "Funny":
                traitsList.Funny.value = 10;
                SwitchMenu();
                break;
            case "Animal Lover":
                traitsList.AnimalLover.value = 10;
                SwitchMenu();
                break;
            default:
                break;
        }  
    }
    public void DecreaseTrait(Button button)
    {
        selectedTraitName = button.GetComponentInChildren<TextMeshProUGUI>().text;
        switch (selectedTraitName)
        {
            case "Athletic":
                traitsList.Athletic.value = 1;
                SwitchMenu();
                break;
            case "Nerdy":
                traitsList.Nerdy.value = 1;
                SwitchMenu();
                break;
            case "Romantic":
                traitsList.Romantic.value = 1;
                SwitchMenu();
                break;
            case "Funny":
                traitsList.Funny.value = 1;
                SwitchMenu();
                break;
            case "Animal Lover":
                traitsList.AnimalLover.value = 1;
                SwitchMenu();
                break;
            default:
                break;
        }
    }

    public void SwitchMenu()
    {
        if (LikedTraits1.activeInHierarchy == true)
        {
            LikedTraits2.SetActive(true);
            LikedTraits1.SetActive(false);

        }
        else if (LikedTraits2.activeInHierarchy == true)
        {
            DislikedTrait.SetActive(true);
            LikedTraits2.SetActive(false);
        }
        else if (DislikedTrait.activeInHierarchy == true)
        {
            DislikedTrait.SetActive(false);
        }
    }
}
