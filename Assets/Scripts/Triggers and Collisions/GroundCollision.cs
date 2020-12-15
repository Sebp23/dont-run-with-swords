using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    private CharacterController characterControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = gameObject.GetComponent<CharacterController>();
    }

    //check to see if player is on the ground
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            characterControllerScript.PlayerOnGround = true;
        }
    }

    //when the player leaves the ground (jump/fall/etc)
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            characterControllerScript.PlayerOnGround = false;
        }
    }
}
