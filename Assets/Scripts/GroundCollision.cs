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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(NoJump());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            characterControllerScript.playerOnGround = false;
        }
    }

    private IEnumerator NoJump()
    {
        yield return new WaitForFixedUpdate();
        characterControllerScript.playerOnGround = true;
    }
}
