using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Code tutorial/source for some of this script: https://www.youtube.com/watch?v=aRxuKoJH9Y0&ab_channel=Blackthornprod

    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float detectDistance;

    private bool movingLeft = true;
    private bool facingRight = true;
    public bool playerDetected = false;

    [SerializeField]
    private Transform groundDetection;
    
    private int enemyKnightWalking = Animator.StringToHash(nameof(enemyKnightWalking));


    private Transform playerTransform;
    private Renderer enemyRenderer;
    private Animator enemyKnightLegsAnimator;
    private GameMaster gm;

    private void Start()
    {
        enemyRenderer = gameObject.GetComponent<Renderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update()
    {
        if (!gm.isPaused)
        {
            if (gameObject.tag == "Enemy")
            {
                NormalEnemyBehavior();
            }
            else if (gameObject.tag == "Knight Enemy")
            {
                enemyKnightLegsAnimator = GetComponentInChildren<Animator>();
                KnightEnemyBehavior();
            }
        }
    }

    void NormalEnemyBehavior()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
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

    void KnightEnemyBehavior()
    {
        float distanceFromEnemy = Mathf.Abs(gameObject.transform.position.x - playerTransform.position.x);

        if (!enemyRenderer.isVisible || distanceFromEnemy > detectDistance)
        {
            playerDetected = false;
            enemyKnightLegsAnimator.SetBool(enemyKnightWalking, true);
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
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
        else if (enemyRenderer.isVisible && distanceFromEnemy <= detectDistance)
        {
            enemyKnightLegsAnimator.SetBool(enemyKnightWalking, false);

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
            playerDetected = true;
        }
        
    }
}