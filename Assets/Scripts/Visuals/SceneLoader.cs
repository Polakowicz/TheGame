using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadOperation;
    [SerializeField] private Slider progressBar;
    [SerializeField] [Range(0, 1)] private float progressAnimationMultiplayer = 0.25f;
    private float currentValue;
    private float targetValue;
    void Start()
    {
        progressBar.value = currentValue = targetValue = 0;
        var currentScene = SceneManager.GetActiveScene();
        loadOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        loadOperation.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetValue = loadOperation.progress / 0.9f;
        currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplayer * Time.deltaTime);
        progressBar.value = currentValue;

        if (Mathf.Approximately(currentValue, 1))
        {
            loadOperation.allowSceneActivation = true;
        }
    }
}
