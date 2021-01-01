using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPauseController : MonoBehaviour
{
    [Tooltip("An array of 3D text elements for pause menu")]
    [SerializeField]
    private GameObject[] pauseText;

    private GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        foreach (GameObject textObject in pauseText)
        {
            textObject.layer = 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPaused();
    }

    private void CheckIfPaused()
    {
        if (gameMaster.isPaused)
        {
            foreach (GameObject textObject in pauseText)
            {
                textObject.SetActive(true);
            }
        }

        else
        {
            foreach (GameObject textObject in pauseText)
            {
                textObject.SetActive(false);
            }
        }
    }
}
