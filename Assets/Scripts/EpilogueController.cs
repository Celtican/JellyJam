using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EpilogueController : MonoBehaviour
{
    private static string dateName;
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
            >= 5 => "You just had one of the best dates in your death! You and " + dateName +
                    " really hit it off running!\n",
            >= 4 => "You had an excellent time out, and " + dateName + " really enjoyed themselves!\n",
            >= 3 => "It was as good of a date as any. But " + dateName + " seemed quite promising!\n",
            >= 2 => "The date went... okay. It felt like you and " + dateName +
                    " were only getting started when it ended, though.\n",
            >= 1 => "That night took some unexpected turns. " + dateName +
                    " seemed flabbergasted by it all, but they agreed to give you a chance!\n",
            _ => "... The universe was too cruel that night. " + dateName +
                 " seemed quite unhappy. But after a lot of begging from yourself, " + dateName +
                 " agreed to give you a second chance.\n"
        };
        epilogueText += "Two hours later, at midnight, you used the powers of darkness to transform " + dateName + " into a vampire...\nSlowly, they rose from their new undeath, and... ";
        epilogueText += compatibility switch
        {
            >= 0.75f => "as it turns out, you two were actually quite compatible!\n",
            >= 0.5f => "luckily, the two of you were somewhat compatible.\n",
            >= 0.25f => "both of you had very different interests. But, it was bearable for a little while.\n",
            _ => "apparently you both aren't similar at all.\n"
        };
        epilogueText += hypnosisCount switch
        {
            >= 3 => "Much later, " + dateName + " seemed incredibly offended when they realized you used your hypnosis powers against them on your first date.\n",
            >= 2 => "Much later, " + dateName + " was not especially pleased when they learned the truth of your hypnosis powers.\n",
            >= 1 => dateName + " didn't even notice that you used your hypnosis powers on your first date.\n",
            _ => "You felt proud that you didn't even use your hypnosis powers during your first date! True love, surely!\n",
        };
        epilogueText += score switch
        {
            >= 0.75f => "All in all, you live a happily ever after in your castle for the next several thousand years! You and " + dateName + " are happy at last!\n",
            >= 0.5f => "Ah, it was good while it lasted. Unfortunately, you divorce after only " + (int)((score - 0.5f)*250 + 2) + " years...\n",
            >= 0.25f => "Peace only lasted a little while. Several years pass, and " + dateName + " is missing from the castle! You never see them again...\n",
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



    public TMP_Text tmpText;
    public float textSpeed = 20f;
    private float timeSinceStarted = 0;

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