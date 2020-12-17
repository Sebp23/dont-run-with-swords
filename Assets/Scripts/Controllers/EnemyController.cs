using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Code tutorial/source for some of this script: https://www.youtube.com/watch?v=aRxuKoJH9Y0&ab_channel=Blackthornprod
    [Tooltip("How fast the enemy moves")]
    [SerializeField]
    private float speed;

    [Tooltip("How far away the enemy detects the collider so they don't fall off the platform")]
    [SerializeField]
    private float platformDistance;

    [Tooltip("How far away the enemy detects the player")]
    [SerializeField]
    private float detectDistance;

    private bool movingLeft = true;
    private bool facingRight = true;
    public bool playerDetected = false;

    [Tooltip("An empty object attached to the enemy object that detects the ground")]
    [SerializeField]
    private Transform groundDetection;
    
    private int enemyKnightWalking = Animator.StringToHash(nameof(enemyKnightWalking));


    private Transform playerTransform;
    private Renderer enemyRenderer;
    private Animator enemyKnightLegsAnimator;
    private GameMaster gameMaster;

    private void Start()
    {
        enemyRenderer = gameObject.GetComponent<Renderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update()
    {
        //make sure game isn't paused
        if (!gameMaster.isPaused)
        {
            //if it's a regular enemy
            if (gameObject.tag == "Enemy")
            {
                NormalEnemyBehavior();
            }
            //if it's a knight enemy
            else if (gameObject.tag == "Knight Enemy")
            {
                enemyKnightLegsAnimator = GetComponentInChildren<Animator>();
                KnightEnemyBehavior();
            }
        }
    }

    /// <summary>
    /// Behavior for enemy that is not a knight
    /// </summary>
    void NormalEnemyBehavior()
    {
        //move the enemy
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //detect the ground and platform distance
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, platformDistance);

        //change sprite direction based on movement direction
        if (groundInfo.collider == false)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }

    /// <summary>
    /// Behavior for enemy knight
    /// </summary>
    void KnightEnemyBehavior()
    {
        //get the distance between the enemy and the player
        float distanceFromEnemy = Mathf.Abs(gameObject.transform.position.x - playerTransform.position.x);

        //if the knight is not in the camera and the player is too far from the enemy
        if (!enemyRenderer.isVisible || distanceFromEnemy > detectDistance)
        {
            playerDetected = false;
            
            //keep the enemy knight walking (animation)
            enemyKnightLegsAnimator.SetBool(enemyKnightWalking, true);

            //move the enemy
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            //detect the ground and platform distance
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, platformDistance);

            //change sprite direction based on movement direction
            if (groundInfo.collider == false)
            {
                if (movingLeft == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingLeft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingLeft = true;
                }
            }
        }

        //if the knight is in the camera and the player is close enough to the enemy
        else if (enemyRenderer.isVisible && distanceFromEnemy <= detectDistance)
        {
            //stop the knight walking (animation)
            enemyKnightLegsAnimator.SetBool(enemyKnightWalking, false);

            //change sprite direction based on player position
            if (playerTransform.position.x > gameObject.transform.position.x && !facingRight)
            {
                transform.Rotate(0f, 180f, 0);
                facingRight = true;
            }
            else if (playerTransform.position.x < gameObject.transform.position.x && facingRight)
            {
                transform.Rotate(0f, 180f, 0);
                facingRight = false;
            }

            //player is detected
            playerDetected = true;
        }
        
    }
}