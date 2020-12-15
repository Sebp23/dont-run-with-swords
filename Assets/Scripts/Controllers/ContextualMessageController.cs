using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This is the observation pattern
public class ContextualMessageController : MonoBehaviour
{
    [Tooltip("How long before the message fades")]
    [SerializeField]
    private float fadeOutDuration = 1.0f;

    private CanvasGroup canvasGroup;
    private TMP_Text messageText;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponent<TMP_Text>();

        //invisible
        canvasGroup.alpha = 0;
    }

    private IEnumerator ShowMessage(string message, float duration)
    {
        //visible
        canvasGroup.alpha = 1;
        messageText.text = message;
        //wait for duration
        yield return new WaitForSeconds(duration);
        //start fading out
        float fadeElapsedTime = 0;
        float fadeStartTime = Time.time;
        while (fadeElapsedTime < fadeOutDuration)
        {
            fadeElapsedTime = Time.time - fadeStartTime;
            canvasGroup.alpha = 1 - fadeElapsedTime / fadeOutDuration;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// When the message is triggered
    /// </summary>
    /// <param name="message">What message to display</param>
    /// <param name="messageDuration">How long should the message be displayed</param>
    private void OnContextualMessageTriggered(string message, float messageDuration)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message, messageDuration));
    }

    //subscribe
    private void OnEnable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered += OnContextualMessageTriggered;
    }
    //unsubscribe
    private void OnDisable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered -= OnContextualMessageTriggered;
    }
}
