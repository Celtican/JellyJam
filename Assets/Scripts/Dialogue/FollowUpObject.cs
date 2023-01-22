using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Follow Up", menuName = "Heartstring/Follow Up", order = 2)]
public class FollowUpObject : ScriptableObject
{
    public float heartsGained;
    public string text;
    public List<DialogueObject.Speech> speech;
}
