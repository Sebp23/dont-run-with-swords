using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Tooltip("The sound that is played when the level end is triggered")]
    [SerializeField]
    public AudioClip levelEndSound;

    [Tooltip("How fast the cart and player move during the level end animation")]
    [SerializeField]
    private float cartSpeed;

    [Tooltip("How fast the wheels on the cart rotate during the level end animation")]
    [SerializeField]
    private float wheelRotateSpeed;

    [Tooltip("How high the player jumps to reach the cart seat during the level end animation")]
    [SerializeField]
    private float levelEndJumpHeight;

    [Tooltip("How long to wait before the next level is loaded")]
    [SerializeField]
    private float transitionWaitTime = 2f;

    [Tooltip("How long to wait after the player jumps to begin moving the cart")]
    [SerializeField]
    private float levelEndAnimationWaitTime = 1.5f;

    private bool playerOnCart = false;

    public bool levelComplete = false;
    public bool transitionIntialized = false;

    [Tooltip("The transform of the player object")]
    [SerializeField]
    private Transform playerObjectTransform;

    [Tooltip("The animator for the player's legs")]
    [SerializeField]
    private Animator playerLegsAnimator;

    [Tooltip("The cart's right wheel")]
    [SerializeField]
    private GameObject cartWheelRight;

    [Tooltip("The cart's left wheel")]
    [SerializeField]
    private GameObject cartWheelLeft;

    private Transform playerSpawn;
    private GameMaster gameMaster;
    public AudioSource cartAudio;
    public AudioSource backgroundMusic;
    private CharacterController characterControllerScript;

    private void Start()
    {
        //set all the level end bools to false to make sure the level doesn't end prematurely.
        playerOnCart = false;
        levelComplete = false;
        transitionIntialized = false;

        //get all required components
        playerObjectTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        cartAudio = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();

        //set the player legs animator to true to make sure animations work after level transition
        playerLegsAnimator.enabled = true;
    }

    private void Update()
    {
        //when the level ends, and the player is on the cart, begin the level end animation
        if (levelComplete && playerOnCart)
        {
            AnimateLevelEnd();
        }
    }

    /// <summary>
    /// Start rotating the cart wheels and moving the cart. 
    /// This is the animation that plays at the end of a level.
    /// </summary>
    private void AnimateLevelEnd()
    {
        cartWheelRight.transform.Rotate(Vector3.back * Time.deltaTime * wheelRotateSpeed);
        cartWheelLeft.transform.Rotate(Vector3.back * Time.deltaTime * wheelRotateSpeed);
        gameObject.transform.position += Vector3.right * Time.deltaTime * cartSpeed;
        playerObjectTransform.transform.position += Vector3.right * Time.deltaTime * cartSpeed;
    }

    /// <summary>
    /// Makes the player character automatically jump the correct height to land on the cart seat 
    /// and begin the level end animation.
    /// </summary>
    private void PlayerJumpOnCart()
    {
        var jumpVector = new Vector2(0f, levelEndJumpHeight);
        characterControllerScript.playerRigidbody.velocity = new Vector2(jumpVector.x, 0f);
        characterControllerScript.playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);

    }

    //this triggers the level end process
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !levelComplete)
        {
            levelComplete = true;
            transitionIntialized = true;
            backgroundMusic.Pause();
            cartAudio.PlayOneShot(levelEndSound);
            backgroundMusic.Play();
            PlayerJumpOnCart();
        }
    }

    /// <summary>
    /// wait a few seconds before moving the cart to allow the animation to look smoother
    /// </summary>
    /// <returns>The amount of seconds waited</returns>
    public IEnumerator WaitToMove()
    {
        characterControllerScript.PlayerOnGround = true;
        playerLegsAnimator.SetBool(characterControllerScript.playerWalking, false);
        transitionIntialized = false;
        yield return new WaitForSeconds(1.5f);
        playerLegsAnimator.enabled = false;
        playerOnCart = true;
        StartCoroutine(TransitionToNextLevel());
    }

    /// <summary>
    /// Play the level end achievment music 
    /// then load the next scene after waiting a few seconds
    /// </summary>
    /// <returns>The amount of seconds waited</returns>
    IEnumerator TransitionToNextLevel()
    {
        yield return new WaitForSeconds(2f); 
        //get the correct position to spawn the player in the next level (spawn location is the same in every level)
        gameMaster.lastCheckPointPosition = playerSpawn.position;
        //load the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}