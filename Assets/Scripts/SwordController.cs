using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    private float throwVelocityX;
    [SerializeField]
    private float throwVelocityY;

    private Rigidbody2D rigidbody;
    private ThrowSword throwSwordScript;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        throwSwordScript = GameObject.Find("Player").GetComponent<ThrowSword>();
    }

    // Update is called once per frame
    void Update()
    {
        throwVelocityX = throwSwordScript.swordThrowDirection;

        rigidbody.velocity = new Vector2(throwVelocityX, throwVelocityY);
    }
}
