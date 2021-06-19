using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundCollision : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterControllerScript;
    [SerializeField]
    private EndMenuCharacterController endMenuCharacterControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();
        endMenuCharacterControllerScript = gameObject.GetComponent<EndMenuCharacterController>();
    }

    //check to see if player is on the ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && SceneManager.GetActiveScene().name != "EndMenu")
        {
            characterControllerScript.PlayerOnGround = true;
        }

        if (other.gameObject.CompareTag("Ground") && SceneManager.GetActiveScene().name == "EndMenu")
        {
            endMenuCharacterControllerScript.PlayerOnGround = true;
        }
    }

    //when the player leaves the ground (jump/fall/etc)
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && SceneManager.GetActiveScene().name != "EndMenu")
        {
            characterControllerScript.PlayerOnGround = false;
        }

        if (other.gameObject.CompareTag("Ground") && SceneManager.GetActiveScene().name == "EndMenu")
        {
            endMenuCharacterControllerScript.PlayerOnGround = false;
        }
    }
}
