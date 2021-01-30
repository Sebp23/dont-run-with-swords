using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    private float cloudSpeed = 1.0f;

    private float cloudPositionX;
    private float cloudPositionY;
    private float cloudStartPositionX;

    private Transform cloudPosition;
    private Transform cloudStartPosition;
    private Transform cloudEndPosition;

    private Vector2 cloudStartVector;

    // Start is called before the first frame update
    void Start()
    {
        cloudPosition = gameObject.transform;
        cloudStartPosition = GameObject.Find("CloudStart").GetComponent<Transform>();
        cloudEndPosition = GameObject.Find("CloudEnd").GetComponent<Transform>();
        cloudPositionY = gameObject.transform.position.y;
        cloudStartPositionX = cloudStartPosition.position.x;
        cloudStartVector = new Vector2(cloudStartPosition.position.x, cloudPosition.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        cloudPositionX = gameObject.transform.position.x;
        
        if (cloudPositionX >= cloudEndPosition.position.x)
        {
            gameObject.transform.position = cloudStartVector;
            
        }
        else
        {
            transform.Translate(Vector2.right * cloudSpeed * Time.deltaTime);
        }
    }

    //private void RespawnCloud()
    //{
    //    cloudPosition = cloudStartPosition.position.x;
    //}
}
