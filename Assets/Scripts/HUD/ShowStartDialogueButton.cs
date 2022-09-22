using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStartDialogueButton : MonoBehaviour
{
    public Button startDialogueButton;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startDialogueButton.gameObject.SetActive(true);
        }
    }
}