using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Heartstring/Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    public List<Speech> speech;
    public List<FollowUpObject> followUps;
    
    [Serializable]
    public class Speech
    {
        public bool isPlayer;
        [TextArea(1, 2)] public string text;
    }
}
