using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneCode : MonoBehaviour
{
    public PlayableDirector introCutscene = null;
    public String scene;
    private double skipTime = 6441f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            introCutscene.time = skipTime;
            SceneManager.LoadScene(scene);
        }
        else if (introCutscene.state != PlayState.Playing)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
