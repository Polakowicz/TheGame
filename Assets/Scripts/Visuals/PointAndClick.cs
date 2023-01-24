using Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAndClick : MonoBehaviour
{
    // [whereistheguru] Adding sound effects.
    public enum ObjectType { NONE, Couch, Switch, Maps, Plant, Toilet, Door, Computer, Radio, Books }
    public ObjectType type;
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
        FindObjectOfType<AudioManager>().Play("CapsuleMusic");
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
            // Set flag to resume game
            GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.LoadedGame;

            SceneManager.LoadScene("Test_Level_02");
        }

        // [whereistheguru] Adding sound effects.
        switch (type)
        {
            case ObjectType.Couch:
                FindObjectOfType<AudioManager>().Play("Sleeping");
                break;
            case ObjectType.Switch:
                FindObjectOfType<AudioManager>().Play("LightSwitch");
                break;
            case ObjectType.Maps:
                FindObjectOfType<AudioManager>().Play("Maps");
                break;
            case ObjectType.Plant:
                FindObjectOfType<AudioManager>().Play("Plant");
                break;
            case ObjectType.Toilet:
                FindObjectOfType<AudioManager>().Play("Toilet");
                break;
            case ObjectType.Door:
                FindObjectOfType<AudioManager>().Play("ExitDoor");
                break;
            case ObjectType.Computer:
                FindObjectOfType<AudioManager>().Play("Computer");
                break;
            case ObjectType.Radio:
                FindObjectOfType<AudioManager>().Play("Radio");
                break;
            case ObjectType.Books:
                FindObjectOfType<AudioManager>().Play("Books");
                break;
            default:
                Debug.Log("NO TYPE");
                break;
        }
    }
}
