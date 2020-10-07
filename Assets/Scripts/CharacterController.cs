using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 5f;

    public bool playerOnGround = false;

    private Rigidbody2D rigidbody;
    private Vector2 input;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void Jump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);

        if (Input.GetKey(KeyCode.W) && playerOnGround)
        {
            rigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }
}
