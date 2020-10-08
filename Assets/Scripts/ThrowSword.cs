using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    [SerializeField]
    public float swordVelocityX = 5f;
    [SerializeField]
    public float swordVelocityY = 0f;

    public float swordThrowDirection;

    private CharacterController characterControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Throw();
    }

    void Throw()
    {
        if (characterControllerScript.facingRight)
        {
            swordThrowDirection = swordVelocityX;
        }
        else if (!characterControllerScript.facingRight)
        {
            swordThrowDirection = -swordVelocityX;
        }
    }
}
