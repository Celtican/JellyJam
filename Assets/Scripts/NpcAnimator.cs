using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NpcAnimator : MonoBehaviour
{
    private Animator animator;
    private static readonly int Talking = Animator.StringToHash("Talking");
    public string randomName;

    public string[] maleNames;
    public string[] femaleNames;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        string[] nameArray = PlayerTraits.Instance != null ? maleNames :
            PlayerTraits.Instance.prefersFemales ? femaleNames : maleNames;
        randomName = nameArray[Random.Range(0, nameArray.Length)];
    }

    public void StartTalking()
    {
        animator.SetBool(Talking, true);
    }
    public void StopTalking()
    {
        animator.SetBool(Talking, false);
    }
    
    public void StopWalking() // called by animator
    {
        DialogueController.Instance.NextStep();
    }

    public void Exit()
    {
        animator.SetBool("Exit", true);
    }
    
}
