using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;

    private Rigidbody2D rigidbody;
    private Transform target;
    private Vector2 targetDirection;
    //private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        targetDirection = (target.transform.position - transform.position).normalized * speed;
        Vector3 relativePos = target.transform.position - gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = gameObject.transform.rotation.x;
        rotation.y = gameObject.transform.rotation.y;
        gameObject.transform.rotation = rotation;
        rigidbody.velocity = new Vector2(targetDirection.x, targetDirection.y);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(hitInfo.gameObject);
        Destroy(gameObject);
    }
}
