using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // public GameObject mainScreen;
    // public GameObject optionsScreen;
    // public GameObject creditsScreen;

    void Start()
    {
        // mainScreen.gameObject.SetActive(true);
        // optionsScreen.gameObject.SetActive(false);
        // creditsScreen.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }

    public void ResumeGame()
    {
        // GameManager.Instance.LoadGame();
        NewGame();
    }
    /*
        public void GoSettings(){
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            mainScreen.gameObject.SetActive(false);
            optionsScreen.gameObject.SetActive(true);
        }

        public void GoCredits(){
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            mainScreen.gameObject.SetActive(false);
            creditsScreen.gameObject.SetActive(true);
        }

        public void GoBack(){
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            optionsScreen.gameObject.SetActive(false);
            creditsScreen.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(true);
        }
    */

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Debug.Log("Można teraz bezpiecznie wyłączyć komputer.");
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}