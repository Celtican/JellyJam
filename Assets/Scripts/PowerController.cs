using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PowerController : MonoBehaviour
{
    public float timeBetweenPowers;
    public Image fillImage;
    public GameObject mindReadingPrefab;

    private float cooldown;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        fillImage.fillAmount = -(Mathf.Clamp01(cooldown/timeBetweenPowers)-1);
        button.interactable = cooldown <= 0;
    }

    public void HypnotizePower()
    {
        cooldown = timeBetweenPowers;
        HeartController.Instance.Hypnotize(1f);
    }

    public void ReadMindPower()
    {
        cooldown = timeBetweenPowers;
        Traits.Type randomTrait;
        string traitName;
        switch (Random.Range(0, 5))
        {
            case 1:
                randomTrait = Traits.Type.Athletic;
                traitName = "sports";
                break;
            case 2:
                randomTrait = Traits.Type.Funny;
                traitName = "jokes";
                break;
            case 3:
                randomTrait = Traits.Type.Nerdy;
                traitName = "pop culture";
                break;
            case 4:
                randomTrait = Traits.Type.AnimalLover;
                traitName = "animals";
                break;
            default:
                randomTrait = Traits.Type.Romantic;
                traitName = "romance";
                break;
        }
        GameObject mindReading = Instantiate(mindReadingPrefab, transform.parent);
        bool npcLikes = DialogueController.Instance.npcTraits.GetTraitOfType(randomTrait).value > 5;
        foreach (TMP_Text tmpText in mindReading.GetComponentsInChildren<TMP_Text>())
        {
            tmpText.text = DialogueController.Instance.npcAnimator.randomName + (npcLikes ? " likes " : " dislikes ") + traitName + "!";
        }
    }

}
