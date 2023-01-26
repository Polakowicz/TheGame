using Scripts.Game;
using Scripts.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChaseScene : ExtendedMonoBehaviour
{
    public PlayableDirector director;
    public GameObject horns;
    
    public void StartTimeline()
    {
        director.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventSystem.Instance.OnCutsceneStarted.Invoke();
            StartTimeline();
            horns.GetComponent<Collider2D>().enabled = true;
            StartCoroutine(WaitAndDo((float)director.duration, () => GameEventSystem.Instance.OnCutsceneEnded.Invoke()));
        }
    }
}
