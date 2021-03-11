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
    private GameObject playerSword;
    private Animator playerLegsAnimator;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        characterControllerScript = gameObject.GetComponent<CharacterController>();
        playerLegsAnimator = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public IEnumerator BeginPlayerDeathEvent()
    {
        yield return new WaitForEndOfFrame();

        int i = 0;
        while (i < 11) //gameObject.transform.rotation.z >= -90)//!playerRotateComplete)
        {
            gameObject.transform.Rotate(Vector3.back * Time.deltaTime * playerDeathRotateSpeed);
            yield return new WaitForEndOfFrame();
            Debug.Log("Loop Number: " + i);
            i++;

            //gameObject.transform.Rotate(Vector3.back * Time.deltaTime * playerDeathRotateSpeed);
            //if (gameObject.transform.rotation.z <= -90)
            //{
            //    playerRotateComplete = true;
            //    //yield return new WaitForEndOfFrame();
            //    Debug.Log("Animation Complete");
            //}
            //else
            //{
            //    playerRotateComplete = false;
            //    gameObject.transform.Rotate(Vector3.back * Time.deltaTime * playerDeathRotateSpeed);
            //}




        }
        gameMaster.isPaused = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void BeginPlayerDeathProcess()
    //{
    //    while (gameObject.transform.rotation.z <= 90)
    //    {
    //        gameObject.transform.Rotate(Vector3.back * Time.deltaTime * playerDeathRotateSpeed);

    //        //if(gameObject.transform.rotation.z <= -90 || gameObject.transform.rotation.z >= 90)
    //        //{
    //        //    playerRotateComplete = true;
    //        //}
    //        //else
    //        //{
    //        //    playerRotateComplete = false;
    //        //}
    //    }
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
