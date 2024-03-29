﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSeatCollision : MonoBehaviour
{
    private LevelEnd levelEndScript;

    private void Start()
    {
        levelEndScript = GameObject.Find("Cart (Level End Trigger)").GetComponent<LevelEnd>();
    }

    //when the player lands on the cart seat at the end of the level
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" /*&& levelEndScript.transitionIntialized*/)
        {
            if (!levelEndScript.levelComplete)
            {
                levelEndScript.levelComplete = true;
                levelEndScript.backgroundMusic.Pause();
                levelEndScript.cartAudio.PlayOneShot(levelEndScript.levelEndSound);
                levelEndScript.backgroundMusic.Play();
            }
            Debug.Log("Player on cart");
            StartCoroutine(levelEndScript.WaitToMove());
            Debug.Log("Wait to move");
        }
    }
}
