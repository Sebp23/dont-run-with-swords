using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    [SerializeField]
    private Transform swordThrowPoint;
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private AudioClip swordThrowSound;

    private bool waitedThreeSeconds = true;

    private Vector3 playerPosition;

    private Ammo ammoScript;
    private EnemyController enemyControllerScript;
    private AudioSource objectAudio;

    private void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        enemyControllerScript = gameObject.GetComponent<EnemyController>();
        objectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player" && Input.GetButtonDown("Fire1"))
        {
            PlayerThrow();
        }
        else if (gameObject.tag == "Knight Enemy" && enemyControllerScript.playerDetected == true && waitedThreeSeconds == true)
        {
            waitedThreeSeconds = false;
            StartCoroutine(EnemyThrow());
        }
    }

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

    IEnumerator EnemyThrow()
    {
        yield return new WaitForSeconds(2);
        objectAudio.PlayOneShot(swordThrowSound);
        Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
        waitedThreeSeconds = true;

    }
}
