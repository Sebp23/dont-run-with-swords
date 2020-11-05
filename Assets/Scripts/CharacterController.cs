using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 5f;

    private Transform playerSpawn;

    [SerializeField]
    private GameObject respawnObject;

    //leaving this here until animator is finished in case animator fails
    [SerializeField]
    private Animation legsAnimation;
    [SerializeField]
    private AnimationClip walkClip, jumpClip;

    private bool facingRight = true;

    public bool playerOnGround = false;
    //public bool isJumping;

    public Rigidbody2D playerRB;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 input;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        //respawnObject = GameObject.Find("Respawn Object").GetComponent<GameObject>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0, 0);
        if (input.x != 0)
        {
            if (playerOnGround)
            {
                //leaving this here until animator is finished in case animator fails
                legsAnimation.clip = walkClip;
                legsAnimation.Play();
            }
            transform.position += movement * Time.deltaTime * movementSpeed;
        }
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= respawnObject.transform.position.y)
        {
            Respawn();
        }
        else
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            ChangeSpriteDirection();
        }
    }

    void Jump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);

        if (Input.GetKey(KeyCode.W) && playerOnGround)
        {
            //leaving this here until animator is finished in case animator fails
            legsAnimation.clip = jumpClip;
            legsAnimation.Play();
            playerRB.velocity = new Vector2(jumpVector.x, 0f);
            playerRB.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = playerSpawn.transform.position;
    }

    void ChangeSpriteDirection()
    {
        //Credit: https://answers.unity.com/questions/1604524/character-facing-the-position-of-mouse-cursor-2d-p.html

        if (input.x > 0 && !facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = true;
        }
        else if (input.x < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = false;
        }
    }
}
