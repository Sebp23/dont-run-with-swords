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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = characterControllerScript.playerSpawn.transform.position;
        }
    }
}
