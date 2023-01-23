using System;
using UnityEngine;

public class NpcTraits : MonoBehaviour
{
    [HideInInspector]
    public TraitsList traitsList = new();
    
    public void Awake()
    {
        traitsList.Randomize();
    }
}
