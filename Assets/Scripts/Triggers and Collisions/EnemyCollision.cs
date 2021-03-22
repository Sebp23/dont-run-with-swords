using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private CharacterController characterControllerScript;

    private void Start()
    {
        characterControllerScript = gameObject.GetComponent<CharacterController>();
    }

    //when the player collides with an enemy, respawn
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Knight Enemy")
        {
            characterControllerScript.playerDead = true;
            characterControllerScript.Respawn();
        }
    }
}
