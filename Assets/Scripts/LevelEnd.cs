using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private Transform playerSpawn;
    private GameMaster gm;

    private void Start()
    {
        playerSpawn = GameObject.Find("Player Spawn").GetComponent<Transform>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
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
        yield return new WaitForSeconds(1.5f);
        gm.lastCheckPointPosition = playerSpawn.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
