using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EpilogueController : MonoBehaviour
{
    private static string dateName = "";
    private static float compatibility;
    private static float hearts;
    private static float hypnosisCount;
    private static float score;
    private static string epilogueText;

    public static void CalculateScore(string dateName, TraitsList playerTraits, TraitsList npcTraits)
    {
        compatibility = playerTraits == null ? 0.5f : Traits.CalculateCompatibility(playerTraits, npcTraits);
        hearts = HeartController.Instance.GetHearts();
        hypnosisCount = HeartController.Instance.GetHypnosis();
        score = (hearts / 5f) * (1 - (hypnosisCount / 5f)) * compatibility;
        EpilogueController.dateName = dateName;

        epilogueText = "";
        epilogueText += hearts switch
        {
            >= 5 => "Such luck, amidst a sea of woe. You and " + dateName +
                    " really hit it off running! You've found a companion fit to see you to the end of time.\n",
            >= 4 => "You had an excellent time out, and " + dateName + " really enjoyed themselves!\n",
            >= 3 => "What promise your date with " + dateName + " held for you!\n",
            >= 2 => "The date was... acceptable. It felt like you and " + dateName +
                    " were only getting started when it ended, though.\n",
            >= 1 => "The evening took some unexpected turns. " + dateName +
                    " seemed flabbergasted, but agreed to give you a chance!\n",
            _ => "... The universe was too cruel that night. " + dateName +
                 " seemed quite unhappy. After a lot of begging, " + dateName +
                 " agreed to give you a second chance.\n"
        };
        epilogueText += "Before the bell tolled midnight, you used the powers of darkness to transform " + dateName + " into a vampire...\nSlowly, they arose in their new undeath, and... ";
        epilogueText += compatibility switch
        {
            >= 0.75f => "as it turns out, you two were actually quite compatible!\n",
            >= 0.5f => "luckily, the two of you were somewhat compatible.\n",
            >= 0.25f => "both of you had very different interests. It was tolerable for a time.\n",
            _ => "you weren't similar in the slightest.\n"
        };
        epilogueText += hypnosisCount switch
        {
            >= 3 => "Much later, " + dateName + " seemed incredibly offended when they realized you used your power of hypnosis against them on your first date.\n",
            >= 2 => "Much later, " + dateName + " was not especially pleased when they learned the truth of your hypnotic power.\n",
            >= 1 => dateName + " never noticed that you used your hypnotic power on your first date.\n",
            _ => "You felt proud that you didn't need to use hypnosis during your first date! True love, surely!\n",
        };
        epilogueText += score switch
        {
            >= 0.75f => "All in all, you've lived a happily ever after in your castle for several thousand years! You and " + dateName + " are happy at last...\nPerhaps, it was simply meant to be.",
            >= 0.5f => "Ah yes, it was good while it lasted. At least " + dateName + " had your company, when you'd had none. Unfortunately, you divorced after only " + (int)((score - 0.5f)*250 + 2) + " years...\n",
            >= 0.25f => "Shudder the thought. You may have been dishonest or told them what they wanted to hear...\nbut you gave " + dateName + " the ultimate gift...and now they are missing from the castle! You never see them again...\n",
            _ => "Tensions were high for too many months. One day, " + dateName + " left the cold halls of the castle. Recently, you've heard rumors of their attempts to raise a revolution against you...\n",
        };
        epilogueText += "Score: " + (int)(score * 100000) + "\n";
        epilogueText += "Try again?";
    }

    public static void ForeverAlone()
    {
        dateName = "";
        compatibility = 0;
        hearts = 0;
        hypnosisCount = 0;
        score = 0;
        epilogueText = "What a horrible night to have a curse.\n" +
                       "None of your dates were a match... Not even one!\n" +
                       "No one in a thousand miles could possibly sympathize with your pain, your suffering, your loneliness...\n" +
                       "Left to wander the wastes of reality for another five hundred years, you begin to weep...\n" +
                       "Score: 0\n" +
                       "Try again?";
    }

    public GameObject musicHarmony;
    public GameObject musicNeutral;
    public GameObject musicDiscord;
    public GameObject musicAlone;

    public TMP_Text tmpText;
    public float textSpeed = 20f;
    private float timeSinceStarted = 0;

    private void Start()
    {
        if (dateName == "") musicAlone.SetActive(true);
        else
        {
            switch (score)
            {
                case >= 0.75f:
                    musicHarmony.SetActive(true);
                    break;
                case >= 0.5f:
                    musicNeutral.SetActive(true);
                    break;
                default:
                    musicDiscord.SetActive(true);
                    break;
            }
        }
    }

    public void Update()
    {
        timeSinceStarted += Time.deltaTime;
        int numCharacters = (int)(timeSinceStarted * textSpeed);
        if (numCharacters < epilogueText.Length)
        {
            tmpText.text = epilogueText.Remove(numCharacters);
        }
        else
        {
            tmpText.text = epilogueText;
        }
    }
}