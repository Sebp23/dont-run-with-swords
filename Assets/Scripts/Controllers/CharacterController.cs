using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// An enum containing the possible player states
    /// </summary>
    public enum playerState
    {
        still, walking, jumping
    }

    [Tooltip("How fast the player moves")]
    [SerializeField]
    private float movementSpeed = 5f;

    [Tooltip("How high the player jumps")]
    [SerializeField]
    private float jumpHeight = 5f;

    [Tooltip("The empty object that determines how far the player must fall off the level to respawn (place at bottom of level to work as intended)")]
    [SerializeField]
    private GameObject respawnObject;

    [Tooltip("The player's sword")]
    [SerializeField]
    public GameObject playerSword;

    //animator parameters
    public int playerWalking = Animator.StringToHash(nameof(playerWalking));
    private int playerOnGroundAnimParam = Animator.StringToHash(nameof(playerOnGround));

    private bool playerFell = false;

    private bool playerOnGround = false;
    private bool playerDeathAnimationStarted = false;
    //property for checking if the player is on the ground
    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set
        {
            playerOnGround = value;
            legsAnimator.SetBool(playerOnGroundAnimParam, playerOnGround);
        }
    }
    public bool playerDead;
    public bool facingRight = true;
    public float killingSwordPosition = 0;

    public Rigidbody2D playerRigidbody;
    public playerState state;
    private Vector2 playerInput;
    private Animator legsAnimator;
    private Vector3 playerMovement;
    private Ammo ammoScript;
    private LevelEnd levelEndScript;
    private GameMaster gameMaster;
    private PlayerDeathController playerDeathControllerScript;

    
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;

        playerRigidbody = GetComponent<Rigidbody2D>();
        legsAnimator = GetComponentInChildren<Animator>();
        playerSword = transform.Find("PlayerBlade").gameObject;
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        levelEndScript = GameObject.Find("Cart (Level End Trigger)").GetComponent<LevelEnd>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        playerDeathControllerScript = gameObject.GetComponent<PlayerDeathController>();

        //spawn the player at the checkpoint position
        gameObject.transform.position = gameMaster.lastCheckPointPosition;

        //get the player state when the level loads
        UpdatePlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        //Only execute player states and everything else if game isn't paused and level isn't finished.
        if (!gameMaster.isPaused && !levelEndScript.levelComplete)
        {
            ExecutePlayerState();
            //Only execute player states and everything else if the player isn't dead
            if (!playerDead)
            {
                //player movement vector based on player x input
                playerMovement = new Vector3(playerInput.x, 0, 0);
                UpdatePlayerState();
                ExecutePlayerState();

                //if the player's y position on the axis is lower than the respawn object, then respawn the player
                if (gameObject.transform.position.y <= respawnObject.transform.position.y)
                {
                    playerFell = true;
                    Respawn();
                }
                else
                {
                    //get the player input and change the direction of the player based on if they're moving left or right
                    playerInput.x = Input.GetAxisRaw("Horizontal");
                    ChangeSpriteDirection();
                }

                //constantly check how much ammo the player has
                CheckAmmo();
            }
        }
       
    }

    /// <summary>
    /// This method checks to see if the state of the player (walking, jumping, still) has changed.
    /// If it has, then it will update the state accordingly.
    /// </summary>
    void UpdatePlayerState()
    {
        //if the player is moving
        if (playerInput.x != 0)
        {
            state = playerState.walking;
        }
        //if the player is not moving and not jumping
        else
        {
            state = playerState.still;
        }
        //if the player jumps (presses spacebar while on the ground)
        if (Input.GetButtonDown("Jump") && playerOnGround)
        {
            Debug.Log("Jump state");
            state = playerState.jumping;
        }
    }

    /// <summary>
    /// Move the player based on player movement (which is based on player movement), time.deltatime, and the movement speed
    /// </summary>
    void PlayerWalk()
    {
        transform.position += playerMovement * Time.deltaTime * movementSpeed;
    }

    /// <summary>
    /// This method makes a jump vector based on the jumpheight
    /// It then sets the player velocity to 0 to ensure the player doesn't suddenly slingshot into the sky
    /// And then adds force in the form of an impulse based on the jump vector
    /// </summary>
    void PlayerJump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);
        if(Input.GetButtonDown("Jump") && playerOnGround)
        {
            playerRigidbody.velocity = new Vector2(jumpVector.x, 0f);
            playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Call on the relevant death animation, which is likely scripted in a different class
    /// Scene reload will happen in different script, likely where the relevant death animation ends.
    /// Sword throw death animation (player gets stabbed by own sword) has scene reload in the DeathSword.cs script
    /// which happens when the sword collides with the player as a trigger.
    /// 
    /// //TODO add description as to where scene reload happens for explode animation
    /// </summary>
    public void Respawn()
    {
        if (!playerDeathAnimationStarted && !playerFell)
        {
            playerDeathAnimationStarted = true;
            playerDeathControllerScript.StartCoroutine("BeginPlayerDeathEvent");
            Debug.Log("Death event started");
        }
        else if (playerFell)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /// <summary>
    /// Change the direction of the player based on if they're moving left or right
    /// </summary>
    void ChangeSpriteDirection()
    {
        //Credit: https://answers.unity.com/questions/1604524/character-facing-the-position-of-mouse-cursor-2d-p.html

        if (playerInput.x > 0 && !facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = true;
        }
        else if (playerInput.x < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = false;
        }
    }

    /// <summary>
    /// check how much ammo the player has
    /// if there is no ammo, then remove the sword from the player object
    /// </summary>
    void CheckAmmo()
    {
        if (ammoScript.currentAmmo == 0)
        {
            playerSword.SetActive(false);
        }
        else
        {
            playerSword.SetActive(true);
        }
    }

    /// <summary>
    /// after observing which the state the player should be in,
    /// enable to corresponding animation and method.
    /// </summary>
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
                break;
        }
    }
}
