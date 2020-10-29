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

    private CharacterController characterControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("Player").GetComponent<CharacterController>();

        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        targetDirection = (target.transform.position - transform.position).normalized * speed;

        //code credit to Sharad Khanna https://stackoverflow.com/questions/49567202/how-to-make-a-2d-sprite-rotate-towards-a-specific-point-while-facing-perpendicul
        var dir = target.transform.position - gameObject.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rigidbody.velocity = new Vector2(targetDirection.x, targetDirection.y);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name);
            characterControllerScript.Respawn();
            Destroy(gameObject);
        }
    }
}
