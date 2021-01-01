using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuCharacterController : MonoBehaviour
{
    [Tooltip("How high the player jumps")]
    [SerializeField]
    private float jumpHeight = 5f;

    private int playerWalking = Animator.StringToHash(nameof(playerWalking));
    private int playerOnGroundAnimParam = Animator.StringToHash(nameof(playerOnGround));
    private bool playerOnGround = false;

    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set
        {
            playerOnGround = value;
            legsAnimator.SetBool(playerOnGroundAnimParam, playerOnGround);
        }
    }

    public Rigidbody2D playerRigidbody;
    private Animator legsAnimator;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        legsAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnGround)
        {
            PlayerJump();
        }
    }

    void PlayerJump()
    {
        var jumpVector = new Vector2(0f, jumpHeight);
        playerRigidbody.velocity = new Vector2(jumpVector.x, 0f);
        playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
        legsAnimator.SetBool(playerWalking, false);
    }
}
