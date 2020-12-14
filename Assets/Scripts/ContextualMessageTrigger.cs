using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualMessageTrigger : MonoBehaviour
{
    [Tooltip("Which message to display when the trigger is set off")]
    [SerializeField]
    [TextArea(3, 5)]
    private string message = "Default message";

    [Tooltip("How many seconds should the message be displayed")]
    [SerializeField]
    private float messageDuration = 1.0f;

    //connect to the message controller
    public static event Action<string, float> ContextualMessageTriggered;

    //when the player enters the trigger, display the message
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ContextualMessageTriggered != null)
            {
                ContextualMessageTriggered.Invoke(message, messageDuration);
            }
        }
    }
}
