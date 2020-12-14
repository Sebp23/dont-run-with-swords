using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupCollision : MonoBehaviour
{
    [Tooltip("How much ammo this pickup contains for the player")]
    [SerializeField]
    private float pickupAmount = 1;
    [Tooltip("What sound is played when the player picks up ammo")]
    [SerializeField]
    private AudioClip ammoPickupSound;

    private Ammo ammoScript;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
        playerAudio = GetComponent<AudioSource>();
    }

    //when the player walks over an ammo pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Debug.Log("Ammo Detected");
            if (ammoScript.currentAmmo < ammoScript.maxAmmo)
            {
                playerAudio.PlayOneShot(ammoPickupSound);
                ammoScript.currentAmmo += pickupAmount;
                Debug.Log(pickupAmount);

                Destroy(collision.gameObject);
            }
        }
    }
}
