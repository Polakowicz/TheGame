using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneCode : MonoBehaviour
{
    public PlayableDirector introCutscene = null;
    private double skipTime = 6441f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            introCutscene.time = skipTime;
        }
    }
}
