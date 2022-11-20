using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAndClick : MonoBehaviour
{
    private SpriteRenderer highlight;
    private Animator animator;
    private Color color;
    private Color playerColor;
    
    public SpriteRenderer player;
    public SpriteRenderer appearingSprite;

    private bool switchedOn;
    private bool switchedOff;
    
    public bool isInteractionAnimation;
    public bool isInteractionSound;
    public bool isSceneChanged;
    public bool isPlayerInvisable;
    public bool isAnotherSpriteAppearing;
    
    private static readonly int MouseClicked = Animator.StringToHash("MouseClicked");

    void Start()
    {
        highlight = GetComponent<SpriteRenderer>();
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        color = highlight.color;
        color.a = 0;
        highlight.color = color;
        switchedOff = true;
        switchedOn = false;
    }

    private void OnMouseEnter()
    {
        color.a = 255;
        highlight.color = color;
    }

    private void OnMouseExit()
    {
        color.a = 0;
        highlight.color = color;
    }

    private void OnMouseDown()
    {
        if (isInteractionAnimation)
        {
            animator.SetTrigger(MouseClicked);
        }
        else if(isAnotherSpriteAppearing)
        {
            if (switchedOff)
            {
                appearingSprite.enabled = true;
                switchedOn = true;
                switchedOff = false;
            }
            else if (switchedOn)
            {
                appearingSprite.enabled = false;
                switchedOff = true;
                switchedOn = false;
            }
        }else if (isSceneChanged)
        {
            SceneManager.LoadScene("Test_Level_02");
        }
    }
}
