using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.Player;
using System;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class InteractionSystem : MonoBehaviour
{

    [Header("Detection")]
    // It was supposed to use e.g. an invisible object attached to the Player.
    // I instead opted to just use the Player's position from Transform component.
    //    public Transform detectionPoint;

    // Changing detection radius in the Editor.
    public float detectionRadius = 0.5f;
    // Changing detection layer in the Editor.
    public LayerMask detectionLayer;
    // Cached trigger object.
    public GameObject detectedObject;
    // Attached interaction indicator.
    public GameObject interactor;

    [Header("Comment")]
    public GameObject commentWindow;
    public TMP_Text commentTMP;
    public float visibleInSeconds;

    [Header("Dialogue")]
    public GameObject dialogueWindow;
    public TMP_Text dialogueTMP;

    [Header("Task")]
    public GameObject taskWindow;
    public TMP_Text taskTMP;

    [Header("Others")]
    // List of picked items.
    public List<Item> pickedItems = new List<Item>();

    void Start()
    {
        taskWindow.SetActive(true);
        StartCoroutine(WindowHider(taskWindow));
        // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Reversed order, it's more efficient to search for objects only if interaction was attempted.
        // Might reverse it back later e.g. for adding an interaction indicator.
        if (DetectObject())
        {
            // Setting interactor's position a bit higher the detected object.
            interactor.transform.position = new Vector3(detectedObject.transform.position.x, detectedObject.transform.position.y + 1, detectedObject.transform.position.z);
            interactor.SetActive(true);
            if (InteractInput())
            {
                interactor.SetActive(false);
                detectedObject.GetComponent<Item>().Interact();
            }
        }
        else
        {
            interactor.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            taskWindow.SetActive(true);
            StartCoroutine(WindowHider(taskWindow));
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        // return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        Collider2D obj = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void PickUp(Item item)
    {
        pickedItems.Add(item);
        if (item.hasTask)
        {
            changeTask(item);
        }
    }

    public void Comment(Item item)
    {
        commentWindow.SetActive(true);
        commentTMP.SetText(item.commentText);
        StartCoroutine(WindowHider(commentWindow));
        if (item.hasTask)
        {
            changeTask(item);
        }
    }

    public void Dialogue(Item item)
    {
        PlayerMovement.playerControlsEnabled = false;
        dialogueWindow.SetActive(true);
        StartCoroutine(ShowDialogue(item));
        
        if (item.hasTask)
        {
            changeTask(item);
        }
    }

    private IEnumerator ShowDialogue(Item item)
    {
        yield return null;
        foreach (string i in item.dialogueLines)
        {
            dialogueTMP.SetText(i);
            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }
            yield return null;
        }
        PlayerMovement.playerControlsEnabled = true;
        dialogueWindow.SetActive(false);
    }

    private IEnumerator WindowHider(GameObject window)
    {
        yield return new WaitForSeconds(visibleInSeconds);
        window.SetActive(false);
    }

    private void changeTask(Item item)
    {
        taskWindow.SetActive(true);
        taskTMP.SetText(item.taskText);
        StartCoroutine(WindowHider(taskWindow));
    }
}