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
                textMeshProUGUI.text = "Choose two things that describe you!";
                break;
            case 1:
                textMeshProUGUI.text = "Choose two things that describe you!";
                break;
            case 2:
                textMeshProUGUI.text = "Choose one thing that you aren't!";
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
                    traitsList.athletic.value = 10;
                    button.interactable = false;


                    break;
                case "Nerdy":
                    traitsList.nerdy.value = 10;
                    button.interactable = false;
                    

                    break;
                case "Romantic":
                    traitsList.romantic.value = 10;
                    button.interactable = false;
                    

                    break;
                case "Funny":
                    traitsList.funny.value = 10;
                    button.interactable = false;


                    break;
                case "Animal Lover":
                    traitsList.animalLover.value = 10;
                    button.interactable = false;


                    break;
                default:
                    break;
            }
            positiveTraitSelected++;
        }
        else
        {
            selectedTraitName = button.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (selectedTraitName)
            {
                case "Athletic":
                    traitsList.athletic.value = 1;

                    break;
                case "Nerdy":
                    traitsList.nerdy.value = 1;

                    break;
                case "Romantic":
                    traitsList.romantic.value = 1;

                    break;
                case "Funny":
                    traitsList.funny.value = 1;

                    break;
                case "Animal Lover":
                    traitsList.animalLover.value = 1;

                    break;
                default:
                    break;
            }
            TraitsMenu.SetActive(false);
        }
       
    }

   
}
