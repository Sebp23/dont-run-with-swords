using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenuController : MonoBehaviour
{
    [SerializeField]
    private bool isPlay;

    [SerializeField]
    private bool isMainMenu;

    private void OnMouseUp()
    {
        if (isPlay)
        {
            //Load the main game scene if "Play" is clicked
            SceneManager.LoadScene("LevelOne");
            Debug.Log("Scene Loaded!");
        }
        if (isMainMenu)
        {
            //Load the main game scene if "Main Menu" is clicked
            SceneManager.LoadScene("StartMenu");
            Debug.Log("Scene Loaded!");
        }
    }
}
