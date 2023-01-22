using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTraitSelection : MonoBehaviour
{
    public GameObject TraitsMenu;
    public PlayerTraits playerTraits;
    public TraitsList traitsList;
    string selectedTraitName;
    public int positiveTraitSelected;
    public bool positiveTraitSelectionDone; 
    public TextMeshProUGUI TraitMenuText;


    private void Start()
    {
        positiveTraitSelected = 0;
        positiveTraitSelectionDone = false;
        


    }
    private void Update()
    {
        TraitMenuChange(TraitMenuText);
    }


    public void TraitMenuChange(TextMeshProUGUI textMeshProUGUI)
    {
        switch (positiveTraitSelected)
        {
            case 0:
                textMeshProUGUI.text = "Choose your first liked Trait";
                break;
            case 1:
                textMeshProUGUI.text = "Choose your second liked Trait";
                break;
            case 2:
                textMeshProUGUI.text = "Choose your disliked Trait";
                positiveTraitSelectionDone = true;
                break;
        }
        

    }
    public void IncreaseTrait(Button button)
    {
        if (positiveTraitSelectionDone == false )
        {
            selectedTraitName = button.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (selectedTraitName)
            {
                case "Athletic":
                    traitsList.Athletic.value = 10;
                    button.interactable = false;


                    break;
                case "Nerdy":
                    traitsList.Nerdy.value = 10;
                    button.interactable = false;
                    

                    break;
                case "Romantic":
                    traitsList.Romantic.value = 10;
                    button.interactable = false;
                    

                    break;
                case "Funny":
                    traitsList.Funny.value = 10;
                    button.interactable = false;


                    break;
                case "Animal Lover":
                    traitsList.AnimalLover.value = 10;
                    button.interactable = false;


                    break;
                default:
                    break;
            }
            positiveTraitSelected++;
        }
        
    }
    public void DecreaseTrait(Button button)
    {
        if (positiveTraitSelectionDone == true)
        {
            selectedTraitName = button.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (selectedTraitName)
            {
                case "Athletic":
                    traitsList.Athletic.value = 1;

                    break;
                case "Nerdy":
                    traitsList.Nerdy.value = 1;

                    break;
                case "Romantic":
                    traitsList.Romantic.value = 1;

                    break;
                case "Funny":
                    traitsList.Funny.value = 1;

                    break;
                case "Animal Lover":
                    traitsList.AnimalLover.value = 1;

                    break;
                default:
                    break;
            }
            TraitsMenu.SetActive(false);
        }
       
    }

   
}
