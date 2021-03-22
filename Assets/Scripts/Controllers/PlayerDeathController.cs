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
        //TODO get rid of this magic number
        int i = 0;
        while (i < 8)
        {
            gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * playerDeathRotateSpeed, Space.Self);
            yield return new WaitForFixedUpdate();
            Debug.Log("Loop Number: " + i);
            i++;
        }
        yield return new WaitForSeconds(0.1f);
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //playerBoxCollider.enabled = false;
        playerThrowSwordScript.PlayerDeathThrow();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
