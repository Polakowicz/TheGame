using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a BoxCollider2D component automatically when script is added to the object.
[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    // Determines what kind of interaction is needed from the object.
    public enum InteractionType { NONE, PickUp, Comment, Dialogue }
    public InteractionType type;

    [Header("Comment")]
    public string commentText;

    [Header("Dialogue")]
    public List<string> dialogueLines;

    [Header("Task")]
    public bool hasTask;
    public string taskText;

    // Make sure that created BoxCollider2D component has "Is Trigger" value set to true, as well as its layer set to "Interactable".
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 23;
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.PickUp:
                // Add the object to the picked up items list
                FindObjectOfType<AudioManager>().Play("ItemPickup");
                FindObjectOfType<InteractionSystem>().PickUp(this);
                // Disable the object (destroying will result in Missing Object on the list)
                gameObject.SetActive(false);
                break;
            case InteractionType.Comment:
                FindObjectOfType<InteractionSystem>().Comment(this);
                break;
            case InteractionType.Dialogue:
                FindObjectOfType<InteractionSystem>().Dialogue(this);
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }
}