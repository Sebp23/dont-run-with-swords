using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSeatCollision : MonoBehaviour
{
    private CharacterController characterControllerScript;
    private LevelEnd levelEndScript;

    private void Start()
    {
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();
        levelEndScript = GameObject.Find("Cart (Level End Trigger)").GetComponent<LevelEnd>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && levelEndScript.transitionIntialized)
        {
            StartCoroutine(levelEndScript.WaitToMove());
        }
    }
}
