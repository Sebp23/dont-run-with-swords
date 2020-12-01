using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private AudioClip enemyKnightDeathSound;

    [SerializeField]
    private AudioClip enemyDeathSound;

    private Rigidbody2D rigidbody;
    private AudioSource cameraAudio;

    // Start is called before the first frame update
    void Start()
    {
        cameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * speed;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Knight Enemy")
        {
            if(other.tag == "Knight Enemy")
            {
                cameraAudio.PlayOneShot(enemyKnightDeathSound);
            }
            else
            {
                cameraAudio.PlayOneShot(enemyDeathSound);
            }
            Debug.Log(other.name);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
