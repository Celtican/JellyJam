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
        
        //todo switch depending on player preferences;
        randomName = femaleNames[Random.Range(0, femaleNames.Length)];
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
