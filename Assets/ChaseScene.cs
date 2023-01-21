using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChaseScene : MonoBehaviour
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
            StartTimeline();
            horns.GetComponent<Collider2D>().enabled = true;
        }
    }
}
