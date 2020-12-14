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

    [Tooltip("The sound used when the sword is thrown")]
    [SerializeField]
    private AudioClip swordThrowSound;
    
    private Ammo ammoScript;
    private AudioSource objectAudio; //This is the audio source that will handle the sword throwing sound since this script is used for both player and enemy, the source may differ between the objects
    private GameMaster gameMaster;

    private void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        objectAudio = GetComponent<AudioSource>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the object this script is attached to is the player and they click the fire button, and the game is not paused, then throw the sword
        if (gameObject.tag == "Player" && Input.GetButtonDown("Fire1") && !gameMaster.isPaused)
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
}
