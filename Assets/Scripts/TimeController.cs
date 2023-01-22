using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void TimeOver()
    {
        // THE END OF TIME
        DialogueController.Instance.TimeOver();
        // THE WORLD IS DOOMED
        
        // fun fact: shortly after I wrote this comment, a GitHub conflict caused me to lose a significant amount of progress
    }
}