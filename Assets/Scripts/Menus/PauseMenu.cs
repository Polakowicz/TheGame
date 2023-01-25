using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private AudioManager audioManager;
    // private float fixedDeltaTime;
    public GameObject pauseMenu;
    public GameObject healthbar;
    public AudioMixer musicMixer;

    public static bool GameIsPaused = false;
    float tempVolume;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Music");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void GoMenu()
    {
        audioManager.Play("ButtonClick");
        musicMixer.SetFloat("musicVolume", tempVolume);
        Time.timeScale = 1f;
        GameIsPaused = false;
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume()
    {
        musicMixer.SetFloat("musicVolume", tempVolume);
        pauseMenu.SetActive(false);
        healthbar.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        musicMixer.GetFloat("musicVolume", out tempVolume);
        musicMixer.SetFloat("musicVolume", -80);
        pauseMenu.SetActive(true);
        healthbar.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}