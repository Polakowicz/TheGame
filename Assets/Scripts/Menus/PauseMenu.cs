using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private float fixedDeltaTime;
    public GameObject pauseMenu;

    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        pauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}