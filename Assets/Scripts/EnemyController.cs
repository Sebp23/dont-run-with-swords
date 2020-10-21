using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Code tutorial/source for this script: https://www.youtube.com/watch?v=aRxuKoJH9Y0&ab_channel=Blackthornprod

    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;

    private bool movingLeft = true;

    [SerializeField]
    private Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }
}
