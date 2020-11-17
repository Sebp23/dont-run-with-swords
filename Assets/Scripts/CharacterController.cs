using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private GameObject playerBlade;

    private int playerWalking = Animator.StringToHash(nameof(playerWalking));
    private int playerOnGroundAnimParam = Animator.StringToHash(nameof(playerOnGround));

    private bool facingRight = true;

    private bool playerOnGround = false;
    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set
        {
            playerOnGround = value;
            legsAnimator.SetBool(playerOnGroundAnimParam, playerOnGround);
        }
    }

    public Rigidbody2D playerRB;
    private playerState state;
    private Transform playerSpawn;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 input;
    private CinemachineVirtualCamera cinemachineCam;
    private Animator legsAnimator;
    private Vector3 movement;
    private Ammo ammoScript;
    private GameMaster gm;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        cinemachineCam = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        legsAnimator = GetComponentInChildren<Animator>();
        playerBlade = transform.Find("PlayerBlade").gameObject;
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        //spawn the player at the checkpoint position
        gameObject.transform.position = gm.lastCheckPointPosition;

        UpdatePlayerState();
    }

    private void FixedUpdate()
    {
        //UpdatePlayerState();
        //ExecutePlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(input.x, 0, 0);
        UpdatePlayerState();
        ExecutePlayerState();

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

        CheckAmmo();
    }

    void UpdatePlayerState()
    {
        if (input.x != 0)
        {
            state = playerState.walking;
        }
        else
        {
            state = playerState.still;
        }
        if (Input.GetButtonDown("Jump") && playerOnGround)
        {
            Debug.Log("Jump state");
            state = playerState.jumping;
        }
    }

    void PlayerWalk()
    {
        transform.position += movement * Time.deltaTime * movementSpeed;
    }

    void PlayerJump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);
        if(Input.GetButtonDown("Jump") && playerOnGround)
        {
            playerRB.velocity = new Vector2(jumpVector.x, 0f);
            playerRB.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    //void PlayerStop()
    //{
    //    //do something with this
    //}

    public void Respawn()
    {
        //gameObject.transform.position = playerSpawn.transform.position;
        //cinemachineCam.enabled = false;
        //cinemachineCam.Follow = playerSpawn.transform;
        //StartCoroutine(CameraRespawnWaitForSeconds());

        //reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void CheckAmmo()
    {
        if (ammoScript.currentAmmo == 0)
        {
            playerBlade.SetActive(false);
        }
        else
        {
            playerBlade.SetActive(true);
        }
    }

    private void ExecutePlayerState()
    {
        switch (state)
        {
            case playerState.walking:
                legsAnimator.SetBool(playerWalking, true);
                PlayerWalk();
                break;
            case playerState.jumping:
                legsAnimator.SetBool(playerWalking, false);
                PlayerJump();
                break;
            case playerState.still:
                legsAnimator.SetBool(playerWalking, false);
                //PlayerStop();
                break;
        }
    }
}
