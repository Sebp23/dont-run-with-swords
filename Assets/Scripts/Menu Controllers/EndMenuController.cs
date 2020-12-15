using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    [SerializeField]
    private bool isMainMenu;

    [SerializeField]
    private bool isQuit;

    [SerializeField]
    private bool isCredits;

    private void OnMouseUp()
    {
        if (isMainMenu)
        {
            //Load the main game scene if "Main Menu" is clicked
            SceneManager.LoadScene("StartMenu");
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
