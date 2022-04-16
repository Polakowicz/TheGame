using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject settingsScreen;
    public GameObject creditsScreen;

    // private bool isCreditsActive;
    // private bool isSettingsActive;

    void Start()
    {
        mainScreen.gameObject.SetActive(true);
        settingsScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Test_Level_01");
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

    public void GoSettings(){
        mainScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(true);
    }

    public void GoCredits(){
        mainScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(true);
    }

    // public void Credits()
    // {
    //     if(isCreditsActive){
    //         isCreditsActive = false;
    //         logo.gameObject.SetActive(true);
    //         background.gameObject.SetActive(true);
    //         spookyBackground.gameObject.SetActive(false);
    //         credits.gameObject.SetActive(false);
    //         instructions.gameObject.SetActive(false);
    //     }else{
    //         isCreditsActive = true;
    //         isInstructionActive = false;
    //         logo.gameObject.SetActive(false);
    //         background.gameObject.SetActive(false);
    //         spookyBackground.gameObject.SetActive(true);
    //         credits.gameObject.SetActive(true);
    //         instructions.gameObject.SetActive(false);
    //     }     
    // }

    public void GoBack(){
        settingsScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(true);
    }

    // public void Instructions()
    // {
    //     if(isInstructionActive){
    //         isInstructionActive = false;
    //         logo.gameObject.SetActive(true);
    //         background.gameObject.SetActive(true);
    //         spookyBackground.gameObject.SetActive(false);
    //         credits.gameObject.SetActive(false);
    //         instructions.gameObject.SetActive(false);
    //     }else {
    //         isInstructionActive = true;
    //         isCreditsActive = false;
    //         logo.gameObject.SetActive(false);
    //         background.gameObject.SetActive(false);
    //         spookyBackground.gameObject.SetActive(true);
    //         credits.gameObject.SetActive(false);
    //         instructions.gameObject.SetActive(true);
    //     }
        
    // }

    public void ExitGame()
    {
        Application.Quit();
    }
}