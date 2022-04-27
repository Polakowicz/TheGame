using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // private float fixedDeltaTime;
    public GameObject pauseMenu;

    public static bool GameIsPaused = false;
    /*
        void Start()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
            pauseMenu.gameObject.SetActive(false);
        }
    */
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
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                pauseMenu.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                pauseMenu.gameObject.SetActive(false);
            }
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        */
    }

    public void GoMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Time.timeScale = 1f;
        GameIsPaused = false;
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}