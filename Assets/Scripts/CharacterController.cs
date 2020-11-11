using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterController : MonoBehaviour
{
    private enum playerState
    {
        still, walking, jumping
    }

    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 5f;

    [SerializeField]
    private GameObject respawnObject;

    //leaving this here until animator is finished in case animator fails
    [SerializeField]
    private Animation legsAnimation;
    [SerializeField]
    private AnimationClip walkClip, jumpClip;

    private bool facingRight = true;

    public bool playerOnGround = false;

    public Rigidbody2D playerRB;
    private playerState state;
    private Transform playerSpawn;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 input;
    private CinemachineVirtualCamera cinemachineCam;
    private Vector3 movement;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        cinemachineCam = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        //respawnObject = GameObject.Find("Respawn Object").GetComponent<GameObject>();

        GetPlayerState();
    }

    private void FixedUpdate()
    {
        //GetPlayerState();
        ChangePlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(input.x, 0, 0);

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

    void GetPlayerState()
    {
        if (input.x != 0)
        {
            state = playerState.walking;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetButton("Jump")) && playerOnGround)
        {
            Debug.Log("Jump state");
            state = playerState.jumping;
        }
        else
        {
            state = playerState.still;
        }
    }

    void PlayerWalk()
    {
        if (playerOnGround)
        {
            legsAnimation.clip = walkClip;
            legsAnimation.Play();
        }
        transform.position += movement * Time.deltaTime * movementSpeed;
        if ((Input.GetKey(KeyCode.W) || Input.GetButton("Jump")) && playerOnGround)
        {
            Debug.Log("Jump state");
            state = playerState.jumping;
        }
        else if (input.x == 0)
        {
            state = playerState.still;
        }
    }

    void PlayerJump()
    {
        legsAnimation.clip = jumpClip;
        legsAnimation.Play();
        var jumpVector = new Vector2(0f, jumpHeight);
        if((Input.GetKey(KeyCode.W) || Input.GetButton("Jump")) && playerOnGround)
        {
            playerRB.velocity = new Vector2(jumpVector.x, 0f);
            playerRB.AddForce(jumpVector, ForceMode2D.Impulse);
        }
        if (input.x != 0 && playerOnGround)
        {
            state = playerState.walking;
        }
        else if (input.x == 0 && playerOnGround)
        {
            state = playerState.still;
        }
    }

    void PlayerStop()
    {
        legsAnimation.Stop();
        if (input.x != 0 && playerOnGround)
        {
            state = playerState.walking;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetButton("Jump")) && playerOnGround)
        {
            Debug.Log("Jump state");
            state = playerState.jumping;
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = playerSpawn.transform.position;
        cinemachineCam.enabled = false;
        cinemachineCam.Follow = playerSpawn.transform;
        StartCoroutine(CameraRespawnWaitForSeconds());
    }

    IEnumerator CameraRespawnWaitForSeconds()
    {
        yield return new WaitForSeconds(0.1f);
        cinemachineCam.Follow = gameObject.transform;
        cinemachineCam.enabled = true;
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

    private void ChangePlayerState()
    {
        switch (state)
        {
            case playerState.walking:
                PlayerWalk();
                break;
            case playerState.jumping:
                PlayerJump();
                break;
            case playerState.still:
                PlayerStop();
                break;
            default:
                PlayerStop();
                break;
        }
    }
}
