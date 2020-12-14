using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //is the game paused?
    public bool isPaused;

    //there should be only 1 instance of the gamemaster when the game is launched
    //The GameMaster instance keeps track of where the player spawns and whether or not the game is paused
    private static GameMaster instance;
    public Vector2 lastCheckPointPosition;

    private void Awake()
    {
        //this makes sure that there is only one instance
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
        //if player hits esc, then pause the game or unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    /// <summary>
    /// If the game is paused, then unpause
    /// If the game is unpaused, then pause
    /// </summary>
    private void PauseGame()
    {
        //pause
        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        //unpause
        else
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}
