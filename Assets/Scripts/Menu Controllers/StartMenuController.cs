using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    private bool isPlay;

    [SerializeField]
    private bool isTutorial;

    [SerializeField]
    private bool isQuit;

    [SerializeField]
    private bool isCredits;

    private void OnMouseUp()
    {
        if (isPlay)
        {
            //Load the main game scene if "Play" is clicked
            SceneManager.LoadScene("LevelOne");
            Debug.Log("Scene Loaded!");
        }
        if (isTutorial)
        {
            //Load the Tutorial scene if "How to Play" is clicked
            SceneManager.LoadScene("HowToPlay");
            Debug.Log("Scene Loaded!");
        }
        if (isCredits)
        {
            //load the credits scene if "Credits" is clicked
            SceneManager.LoadScene("Credits");
            Debug.Log("Scene Loaded!");
        }
        if (isQuit)
        {
            //quit the application if "Quit" is clicked
            Application.Quit();
            Debug.Log("Scene Loaded!");
        }
    }
}
