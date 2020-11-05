using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Code tutorial/source for this script: https://www.youtube.com/watch?v=aRxuKoJH9Y0&ab_channel=Blackthornprod

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
    //[SerializeField]
    //private Animation legsAnimation;
    //[SerializeField]
    //private AnimationClip walkClip;

    private Transform playerTransform;
    private Renderer enemyRenderer;

    private void Start()
    {
        enemyRenderer = gameObject.GetComponent<Renderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (gameObject.tag == "Enemy")
        {
            NormalEnemyBehavior();
        }
        else if (gameObject.tag == "Knight Enemy")
        {
            KnightEnemyBehavior();
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
        float distanceFromEnemy = gameObject.transform.position.x - playerTransform.position.x;

        if (!enemyRenderer.isVisible || distanceFromEnemy > detectDistance)
        {
            playerDetected = false;
            //legsAnimation.clip = walkClip;
            //legsAnimation.Play();
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
