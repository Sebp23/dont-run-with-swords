using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool isPaused;

    private static GameMaster instance;
    public Vector2 lastCheckPointPosition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        else
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}
