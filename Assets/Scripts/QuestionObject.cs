using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Heartstring/Question", order = 0)]
public class QuestionObject : ScriptableObject
{
    public string text;
    public DialogueObject[] potentialDialogue;
}
