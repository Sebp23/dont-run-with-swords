using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowSword : MonoBehaviour
{
    [Tooltip("The position in the game space from where the sword is instantiated")]
    [SerializeField]
    private Transform swordThrowPoint;

    [Tooltip("The prefab that is used when the sword is instantiated")]
    [SerializeField]
    private GameObject swordPrefab;

    [Tooltip("The prefab that is used for the sword that is instantiated for death animation")]
    [SerializeField]
    private GameObject deathSwordPrefab;

    [Tooltip("The player's sword")]
    [SerializeField]
    private GameObject playerSword;

    [Tooltip("The sound used when the sword is thrown")]
    [SerializeField]
    private AudioClip swordThrowSound;

    private Ammo ammoScript;
    private CharacterController characterControllerScript;
    private AudioSource objectAudio; //This is the audio source that will handle the sword throwing sound since this script is used for both player and enemy, the source may differ between the objects
    private GameMaster gameMaster;

    private void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        characterControllerScript = GetComponent<CharacterController>();
        objectAudio = GetComponent<AudioSource>();
        playerSword = transform.Find("PlayerBlade").gameObject;
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player clicks the fire button, and the game is not paused, then throw the sword
        if (Input.GetButtonDown("Fire1") && !gameMaster.isPaused)
        {
            Debug.Log("Fire button clicked");
            PlayerThrow();
        }
    }

    /// <summary>
    /// This method is what instantiates the sword if the player ammo is above 0
    /// The velocity of the sword is controlled by the Sword.cs script
    /// </summary>
    void PlayerThrow()
    {
        if (ammoScript.currentAmmo <= ammoScript.maxAmmo && ammoScript.currentAmmo > 0)
        {
            Debug.Log("Sword about to be thrown");

            objectAudio.PlayOneShot(swordThrowSound);
            //sword throwing logic
            Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
            ammoScript.currentAmmo--;

            Debug.Log("Sword thrown");
        }   
    }

    public void PlayerDeathThrow()
    {
        Debug.Log("Sword about to be thrown");

        objectAudio.PlayOneShot(swordThrowSound);
        //sword throwing logic
        Instantiate(deathSwordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
        characterControllerScript.PlayerOnGround = true;
        characterControllerScript.playerSword.SetActive(false);

        Debug.Log("Sword thrown");
    }
}
