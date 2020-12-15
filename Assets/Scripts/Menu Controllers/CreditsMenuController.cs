using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenuController : MonoBehaviour
{
    [SerializeField]
    private bool isMainMenu;

    private void OnMouseUp()
    {
        if (isMainMenu)
        {
            //Load the main game scene if "Main Menu" is clicked
            SceneManager.LoadScene("StartMenu");
            Debug.Log("Scene Loaded!");
        }
    }
}
