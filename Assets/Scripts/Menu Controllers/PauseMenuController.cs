using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField]
    private bool isMainMenu;

    [SerializeField]
    private bool isQuit;

    [SerializeField]
    private bool isResume;

    private GameMaster gameMaster;
    private Renderer buttonRenderer;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        buttonRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnMouseUp()
    {
        if (isMainMenu)
        {
            //unpause
            gameMaster.isPaused = !gameMaster.isPaused;

            //Load the main game scene if "Main Menu" is clicked
            SceneManager.LoadScene("StartMenu");
            Debug.Log("Scene Loaded!");
        }
        if (isResume)
        {
            //unpause
            gameMaster.isPaused = !gameMaster.isPaused;
        }
        if (isQuit)
        {
            //unpause
            gameMaster.isPaused = !gameMaster.isPaused;

            //quit the application if "Quit" is clicked
            Application.Quit();
            Debug.Log("Scene Loaded!");
        }
    }
}
