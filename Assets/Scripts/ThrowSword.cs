using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
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
    
    [Tooltip("The number of seconds in between each enemy sword throw")]
    [SerializeField]
    private float enemyThrowCooldown = 2;

    //if the cooldown has finished
    private bool cooldownHasElapsed = true;

    private Ammo ammoScript;
    private EnemyController enemyControllerScript;
    private AudioSource objectAudio; //This is the audio source that will handle the sword throwing sound since this script is used for both player and enemy, the source may differ between the objects
    private GameMaster gameMaster;

    private void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        enemyControllerScript = gameObject.GetComponent<EnemyController>();
        objectAudio = GetComponent<AudioSource>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the object this script is attached to is the player and they click the fire button, and the game is not paused, then throw the sword
        if (gameObject.tag == "Player" && Input.GetButtonDown("Fire1") && !gameMaster.isPaused)
        {
            PlayerThrow();
        }
        //if the object this script is attached to is an enemy knight and they detect the player, and the cooldown is over, and the game is not paused, then throw the sword
        else if (gameObject.tag == "Knight Enemy" && enemyControllerScript.playerDetected && cooldownHasElapsed && !gameMaster.isPaused)
        {
            //reset the cooldown
            cooldownHasElapsed = false;
            StartCoroutine(EnemyThrow());
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
            objectAudio.PlayOneShot(swordThrowSound);
            //sword throwing logic
            Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
            ammoScript.currentAmmo--;
        }   
    }

    /// <summary>
    /// This is the IEnumerator that instantiates the enemy's sword after their cooldown
    /// The velocity of the enemy sword is controlled by the EnemySword.cs script.
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyThrow()
    {
        yield return new WaitForSeconds(enemyThrowCooldown);
        objectAudio.PlayOneShot(swordThrowSound);
        Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
        cooldownHasElapsed = true;

    }
}
