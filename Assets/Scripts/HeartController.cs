using System;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public static HeartController Instance { get; private set; }
    
    public float startingHearts = 1;
    public float hypnotizedHearts;
    
    private float maxHearts;
    private float curHearts;
    private Animator[] heartAnimators;

    private void Awake()
    {
        Instance = this;
        heartAnimators = GetComponentsInChildren<Animator>();
        maxHearts = heartAnimators.Length; // how many heart children are attached
    }

    private void Start()
    {
        ResetHearts();
    }

    public void ResetHearts()
    {
        hypnotizedHearts = 0;
        SetHearts(startingHearts);
    }

    public void ClearHearts()
    {
        hypnotizedHearts = 0;
        SetHearts(0);
    }

    public void AddHearts(float amount)
    {
        SetHearts(curHearts + amount);
    }

    public void SetHearts(float amount)
    {
        curHearts = Mathf.Clamp(amount, hypnotizedHearts, maxHearts);
        for (int i = 0; i < maxHearts; i++)
        {
            heartAnimators[i].SetBool("Full", i <= curHearts-1);
            heartAnimators[i].SetBool("Hypnotized", i <= hypnotizedHearts-1);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Hypnotize(float amount)
    {
        hypnotizedHearts += amount;
        AddHearts(amount);
    }
}