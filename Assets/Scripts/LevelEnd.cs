using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private AudioClip levelEndSound;
    [SerializeField]
    private float cartSpeed;
    [SerializeField]
    private float wheelRotateSpeed;
    [SerializeField]
    private float levelEndJumpHeight;

    public bool levelComplete = false;
    public bool playerOnCart = false;
    public bool transitionIntialized = false;

    [SerializeField]
    private Transform playerObject;
    [SerializeField]
    private Animator playerLegsAnimator;
    [SerializeField]
    private GameObject cartWheelRight;
    [SerializeField]
    private GameObject cartWheelLeft;

    private Transform playerSpawn;
    private GameMaster gm;
    private AudioSource cartAudio;
    private AudioSource backgroundMusic;
    private CharacterController characterControllerScript;

    private void Start()
    {
        playerOnCart = false;
        levelComplete = false;
        transitionIntialized = false;
        playerObject = GameObject.Find("Player").GetComponent<Transform>();
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        cartAudio = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();
        playerLegsAnimator.enabled = true;
    }

    private void FixedUpdate()
    {
        if (levelComplete && playerOnCart)
        {
            cartWheelRight.transform.Rotate(Vector3.back * Time.deltaTime * wheelRotateSpeed);
            cartWheelLeft.transform.Rotate(Vector3.back * Time.deltaTime * wheelRotateSpeed);
            gameObject.transform.position += Vector3.right * Time.deltaTime * cartSpeed;
            playerObject.transform.position += Vector3.right * Time.deltaTime * cartSpeed;
        }
    }

    private void PlayerJumpOnCart()
    {
        var jumpVector = new Vector2(0f, levelEndJumpHeight);
        characterControllerScript.playerRB.velocity = new Vector2(jumpVector.x, 0f);
        characterControllerScript.playerRB.AddForce(jumpVector, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !levelComplete)
        {
            levelComplete = true;
            transitionIntialized = true;
            PlayerJumpOnCart();
        }
    }

    public IEnumerator WaitToMove()
    {
        transitionIntialized = false;
        yield return new WaitForSeconds(1.5f);
        playerLegsAnimator.enabled = false;
        playerOnCart = true;
        StartCoroutine(TransitionToNextLevel());
    }

    IEnumerator TransitionToNextLevel()
    {
        backgroundMusic.Pause();
        cartAudio.PlayOneShot(levelEndSound);
        yield return new WaitForSeconds(2f);
        backgroundMusic.Play();
        gm.lastCheckPointPosition = playerSpawn.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
