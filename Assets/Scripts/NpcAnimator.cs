using System;
using UnityEngine;
using UnityEngine.Events;

public class NpcAnimator : MonoBehaviour
{
    private Animator animator;
    private static readonly int Talking = Animator.StringToHash("Talking");

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
