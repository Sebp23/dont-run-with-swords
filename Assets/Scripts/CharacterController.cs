﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 5f;

    [SerializeField]
    private Transform playerSpawn;

    [SerializeField]
    private GameObject respawnObject;

    public bool facingRight = true;

    public bool playerOnGround = false;

    private Rigidbody2D rigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 input;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        //playerSpawn = GameObject.Find("Player Spawn").GetComponent<GameObject>();
        //respawnObject = GameObject.Find("Respawn Object").GetComponent<GameObject>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0, 0);
        transform.position += movement * Time.deltaTime * movementSpeed;
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= respawnObject.transform.position.y)
        {
            gameObject.transform.position = playerSpawn.transform.position;
        }
        else
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            ChangeSpriteDirection();
        }
    }

    void Jump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);

        if (Input.GetKey(KeyCode.W) && playerOnGround)
        {
            rigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    void ChangeSpriteDirection()
    {
        //Credit: https://answers.unity.com/questions/1604524/character-facing-the-position-of-mouse-cursor-2d-p.html

        if (input.x > 0 && !facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = true;
        }
        else if (input.x < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0);
            facingRight = false;
        }
    }
}
