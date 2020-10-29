using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupCollision : MonoBehaviour
{
    [SerializeField]
    private float pickupAmount = 1;

    private bool ammoPickedUp = false;

    private Ammo ammoScript;

    // Start is called before the first frame update
    void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Debug.Log("Ammo Detected");
            if (ammoScript.currentAmmo < ammoScript.maxAmmo)
            {
                ammoScript.currentAmmo += pickupAmount;
                Debug.Log(pickupAmount);
            }

            Destroy(collision.gameObject);
        }
    }
}
