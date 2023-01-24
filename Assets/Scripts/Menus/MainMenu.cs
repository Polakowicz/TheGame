using Scripts.Game;
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
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    public void NewGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

		// Set flag to new game
		GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.NewGame;

        // Change scene
		LoadLevel();
    }

    public void ResumeGame()
    {
		FindObjectOfType<AudioManager>().Play("ButtonClick");

		// Load data from file
		GameEventSystem.Instance.SaveSystem.LoadGame();

        // Set flag to resume game
        GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.LoadedGame;

        // Change scene
        LoadLevel();
	}

    private void LoadLevel()
    {
        // Change scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		if (Time.timeScale == 0.0f)
		{
			Time.timeScale = 1.0f;
		}
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
        Application.Quit();
        /*
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
        */
    }
}