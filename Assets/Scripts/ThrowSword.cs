using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    [SerializeField]
    private Transform swordThrowPoint;
    [SerializeField]
    private GameObject swordPrefab;

    private Ammo ammoScript;

    private void Start()
    {
        ammoScript = GameObject.Find("Ammo Message").GetComponent<Ammo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }

    void Throw()
    {
        if (ammoScript.currentAmmo <= ammoScript.maxAmmo && ammoScript.currentAmmo > 0)
        {
            //sword throwing logic
            Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
            ammoScript.currentAmmo--;
        }
        
        
    }
}
