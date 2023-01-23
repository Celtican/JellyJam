using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Animator animator;
    private int step = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AdvanceAnimation()
    {
        step++;
        animator.SetInteger("Step", step);
    }

    public void ContinueToGame()
    {
        SetScene("Game");
    }
}
