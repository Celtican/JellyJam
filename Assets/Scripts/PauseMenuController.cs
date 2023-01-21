using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioController.Instance.GetVolume();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused()) Unpause();
            else Pause();
        }
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool IsPaused()
    {
        return pauseMenu.activeInHierarchy;
    }

    public void SetScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void SetVolume(float volume)
    {
        AudioController.Instance.SetVolume(volume);
    }
}
