using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private GameMaster gm;
    private GameObject windmillBlades;
    private bool checkpointActivated = false;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        windmillBlades = transform.Find("Windmill Blades").gameObject;
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
            //the respawn position is equal to checkpoint position once player passes checkpoint.
            gm.lastCheckPointPosition = gameObject.transform.position;
            checkpointActivated = true;
        }
    }
}
