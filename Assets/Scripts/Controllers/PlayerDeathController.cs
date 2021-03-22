using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathController : MonoBehaviour
{
    [Tooltip("The speed at which the players rotates when they die")]
    [SerializeField]
    private float playerDeathRotateSpeed;

    [Tooltip("The height at which the player's sword is thrown into the sky")]
    [SerializeField]
    private float playerDeathSwordThrowHeight;

    [Tooltip("This is how many times a loop happens to determine the player's rotation during animation. It is good for fine-tuning the rotation to make the Z-Rotation as close to 90 as possible. The player should be flat on the ground.")]
    [SerializeField]
    private int rotationLoopNumberTotal;
    
    public bool playerRotateComplete = false;
    private bool playerDeathAnimationComplete;

    private GameMaster gameMaster;
    private CharacterController characterControllerScript;
    private PlayerThrowSword playerThrowSwordScript;
    private GameObject playerSword;
    private Animator playerLegsAnimator;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        characterControllerScript = gameObject.GetComponent<CharacterController>();
        playerThrowSwordScript = gameObject.GetComponent<PlayerThrowSword>();
        playerLegsAnimator = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator BeginPlayerDeathEvent()
    {
        yield return new WaitForEndOfFrame();
        characterControllerScript.state = CharacterController.playerState.still;

        //the current loop number
        int rotationLoopNumberCurrent = 0;
        while (rotationLoopNumberCurrent < rotationLoopNumberTotal)
        {
            gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * playerDeathRotateSpeed, Space.Self);
            yield return new WaitForFixedUpdate();
            Debug.Log("Loop Number: " + rotationLoopNumberCurrent);
            rotationLoopNumberCurrent++;
        }
        yield return new WaitForSeconds(0.1f);
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerThrowSwordScript.PlayerDeathThrow(); //scene reload happens at end of trigger event when DeathSword trigger collides with player box collider. This is in DeathSword.cs
    }
}
