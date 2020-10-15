using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    [SerializeField]
    private Transform swordThrowPoint;
    [SerializeField]
    private GameObject swordPrefab;

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
        //sword throwing logic
        Instantiate(swordPrefab, swordThrowPoint.position, swordThrowPoint.rotation);
    }
}
