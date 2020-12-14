using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("How fast the windmill blades will rotate when the player activates the checkpoint")]
    [SerializeField]
    private float windmillBladeRotateSpeed;

    [Tooltip("The sound that will play when the player activates the checkpoint")]
    [SerializeField]
    private AudioClip checkpointSound;

    private bool checkpointActivated = false;

    private Collider2D checkpointCollider;
    private GameMaster gameMaster;
    private GameObject windmillBlades;
    private AudioSource checkpointAudio;

    private void Start()
    {
        //get all necessary components
        checkpointCollider = gameObject.GetComponent<Collider2D>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        windmillBlades = transform.Find("Windmill Blades").gameObject;
        checkpointAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //start rotating the windmill blades when the checkpoint is activates
        if (checkpointActivated)
        {
            windmillBlades.transform.Rotate(Vector3.back * Time.deltaTime * windmillBladeRotateSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointAudio.PlayOneShot(checkpointSound);
            //the respawn position is equal to checkpoint position once player passes checkpoint.
            gameMaster.lastCheckPointPosition = gameObject.transform.position;
            checkpointActivated = true;
            checkpointCollider.enabled = false;
        }
    }
}
