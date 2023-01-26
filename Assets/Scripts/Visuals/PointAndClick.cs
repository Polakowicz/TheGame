using Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PointAndClick : MonoBehaviour
{
    // [whereistheguru] Adding sound effects & hiding notes.
    private AudioManager audioManager;
    public enum ObjectType { NONE, Couch, Switch, Maps, Plant, Toilet, Door, Computer, Radio, Books }
    public ObjectType type;
    public AudioMixer musicMixer;
    public static bool RadioIsOn = true;
    float tempVolume;
    public GameObject notes;
    // ends here
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
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("CapsuleMusic");
        
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
            // Set flag to resume game
            GameEventSystem.Instance.StartType = GameEventSystem.GameStartType.LoadedGame;

            SceneManager.LoadScene("Test_Level_02");
        }

        // [whereistheguru] Adding sound effects.
        switch (type)
        {
            case ObjectType.Couch:
                audioManager.Play("Sleeping");
                break;
            case ObjectType.Switch:
                audioManager.Play("LightSwitch");
                break;
            case ObjectType.Maps:
                audioManager.Play("Maps");
                break;
            case ObjectType.Plant:
                audioManager.Play("Plant");
                break;
            case ObjectType.Toilet:
                audioManager.Play("Toilet");
                break;
            case ObjectType.Door:
                audioManager.Play("ExitDoor");
                break;
            case ObjectType.Computer:
                /*
                if (FindObjectOfType<InteractionSystem>().pickedItems.Count == 5) {
                    SceneManager.LoadScene("EndingCutscene");
                } else {
                    audioManager.Play("Computer");
                }
                */
                break;
            case ObjectType.Radio:
                if (RadioIsOn) {
                    musicMixer.GetFloat("musicVolume", out tempVolume);
                    musicMixer.SetFloat("musicVolume", -80);
                    RadioIsOn = false;
                    notes.SetActive(false);
                } else {
                    musicMixer.SetFloat("musicVolume", tempVolume);
                    RadioIsOn = true;
                    notes.SetActive(true);
                }
                break;
            case ObjectType.Books:
                audioManager.Play("Books");
                break;
            default:
                Debug.Log("NO TYPE");
                break;
        }
    }
}
