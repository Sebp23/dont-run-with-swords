using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private AudioClip levelEndSound;

    private Transform playerSpawn;
    private GameMaster gm;
    private AudioSource cartAudio;
    private AudioSource backgroundMusic;

    private void Start()
    {
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        cartAudio = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(TransitionToNextLevel());
        }
    }

    IEnumerator TransitionToNextLevel()
    {
        backgroundMusic.Pause();
        cartAudio.PlayOneShot(levelEndSound);
        yield return new WaitForSeconds(3f);
        backgroundMusic.Play();
        gm.lastCheckPointPosition = playerSpawn.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
