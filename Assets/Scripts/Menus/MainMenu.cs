using Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    public Button resumeButton;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("MenuMusic");
        if(GameEventSystem.Instance.SaveSystem.LoadGame()){
            resumeButton.interactable = true;
        }
    }

    public void NewGame()
    {
        audioManager.Play("ButtonClick");

		// Set flag to new game
		GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.NewGame;

        // Change scene
        SceneManager.LoadScene("OpeningCutscene");
		// LoadLevel();
    }

    public void ResumeGame()
    {
		audioManager.Play("ButtonClick");

		// Load data from file
		// GameEventSystem.Instance.SaveSystem.LoadGame();

        // Set flag to resume game
        GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.LoadedGame;

        // Change scene
        LoadLevel();
	}

    private void LoadLevel()
    {
        // Change scene
		SceneManager.LoadScene("Test_Level_02");
		if (Time.timeScale == 0.0f)
		{
			Time.timeScale = 1.0f;
		}
	}

    public void ExitGame()
    {
        audioManager.Play("ButtonClick");
        Debug.Log("Można teraz bezpiecznie wyłączyć komputer.");
        Application.Quit();
    }

    public void Noonwraith()
    {
        audioManager.Play("Blooper");
    }

    public void Intruder()
    {
        audioManager.Play("AmperSong");
    }
}