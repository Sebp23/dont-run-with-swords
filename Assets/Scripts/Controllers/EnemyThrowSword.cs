using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowSword : MonoBehaviour
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

    [Tooltip("How long should the enemy knight's tell last before the sword is thrown")]
    [SerializeField]
    private float enemyKnightTellSeconds = 0.5f;

    //if the cooldown has finished
    private bool cooldownHasElapsed = true;

    private EnemyController enemyControllerScript;
    private AudioSource objectAudio; //This is the audio source that will handle the sword throwing sound since this script is used for both player and enemy, the source may differ between the objects
    private GameObject enemyKnightThrowTellSprite;
    private GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {

        enemyControllerScript = gameObject.GetComponent<EnemyController>();
        objectAudio = GetComponent<AudioSource>();
        enemyKnightThrowTellSprite = transform.Find("EnemyKnightThrowTell").gameObject;
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        enemyKnightThrowTellSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if the enemy knight detects the player, and the cooldown is over, and the game is not paused, then throw the sword
        if (enemyControllerScript.playerDetected && cooldownHasElapsed && !gameMaster.isPaused)
        {
            //reset the cooldown
            cooldownHasElapsed = false;
            StartCoroutine(EnemyThrowCooldown());
        }
        //stop the sword throw process if the player is not detected and reset the cooldown
        else if (!enemyControllerScript.playerDetected && !gameMaster.isPaused)
        {
            StopAllCoroutines();
            cooldownHasElapsed = true;
        }
    }


    /// <summary>
    /// This IEnumerator is the cooldown time before the enemy throws their sword
    /// It then calls on the IEnumerator to throw the sword
    /// </summary>
    /// <returns>Number of seconds before the cooldown</returns>
    IEnumerator EnemyThrowCooldown()
    {
        yield return new WaitForSeconds(enemyThrowCooldown);
        StartCoroutine(ThrowEnemySword());

    }

    /// <summary>
    /// This IEnumerator throws the sword
    /// It also shows an exclamation mark (the tell) for a certain number of seconds before the sword is thrown
    /// </summary>
    /// <returns></returns>
    IEnumerator ThrowEnemySword()
    {
        enemyKnightThrowTellSprite.SetActive(true);
        yield return new WaitForSeconds(enemyKnightTellSeconds);
        objectAudio.PlayOneShot(swordThrowSound);
        Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
        cooldownHasElapsed = true;
        enemyKnightThrowTellSprite.SetActive(false);
    }
}
