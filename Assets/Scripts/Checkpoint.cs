using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private AudioClip checkpointSound;

    private bool checkpointActivated = false;

    private Collider2D checkpointCollider;
    private GameMaster gm;
    private GameObject windmillBlades;
    private AudioSource checkpointAudio;

    private void Start()
    {
        checkpointCollider = gameObject.GetComponent<Collider2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        windmillBlades = transform.Find("Windmill Blades").gameObject;
        checkpointAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (checkpointActivated)
        {
            windmillBlades.transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointAudio.PlayOneShot(checkpointSound);
            //the respawn position is equal to checkpoint position once player passes checkpoint.
            gm.lastCheckPointPosition = gameObject.transform.position;
            checkpointActivated = true;
            checkpointCollider.enabled = false;
        }
    }
}
