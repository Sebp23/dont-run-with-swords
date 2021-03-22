using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSword : MonoBehaviour
{
    [Tooltip("How far the death animation sword is thrown")]
    [SerializeField]
    private float throwHeight = 5f;

    [Tooltip("How fast does sword rotate when falling down")]
    [SerializeField]
    private float deathSwordRotateSpeed = 5f;

    private Rigidbody2D deathSwordRigidbody;
    private AudioSource cameraAudio;
    // Start is called before the first frame update
    void Start()
    {
        deathSwordRigidbody = gameObject.GetComponent<Rigidbody2D>();

        var throwVector = new Vector2(0f, throwHeight);
        deathSwordRigidbody.velocity = new Vector2(throwVector.x, 0f);
        deathSwordRigidbody.AddForce(throwVector, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.right = Vector3.Slerp(gameObject.transform.right, deathSwordRigidbody.velocity.normalized, Time.deltaTime * deathSwordRotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit player");
            deathSwordRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
