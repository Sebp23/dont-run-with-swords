using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [Tooltip("How fast the enemy knight's sword will travel")]
    [SerializeField]
    private float speed = 20f;

    [Tooltip("How many seconds before the enemy sword destroys itself")]
    [SerializeField]
    private float secondsBeforeDestroy = 5f;

    private Rigidbody2D enemySwordRigidbody;
    private Transform enemySwordTarget;

    private CharacterController characterControllerScript;
    private GameMaster gameMasterScript;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();

        enemySwordRigidbody = GetComponent<Rigidbody2D>();
        enemySwordTarget = GameObject.Find("Player").GetComponent<Transform>();

        //code credit to Sharad Khanna https://stackoverflow.com/questions/49567202/how-to-make-a-2d-sprite-rotate-towards-a-specific-point-while-facing-perpendicul

        //get the direction that the sword must travel based on the distance between the player and the sword
        var dir = enemySwordTarget.transform.position - gameObject.transform.position;
        //get the angle that the sword must rotate to face the player direction
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //rotate the sword to face the correct direction
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //set the velocity of the sword
        enemySwordRigidbody.velocity = enemySwordRigidbody.transform.right * speed;
        //destroy the sword after a certain amount of seconds
        Destroy(gameObject, secondsBeforeDestroy);
    }

    //if the enemy sword hits the player, then respawn the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name);
            characterControllerScript.killingSwordPosition = gameObject.transform.position.x;
            Debug.Log(characterControllerScript.killingSwordPosition);
            characterControllerScript.playerDead = true;
            Debug.Log(characterControllerScript.playerDead);
            characterControllerScript.Respawn();
            Debug.Log("Respawn Started");
            Destroy(gameObject);
        }
    }
}
