using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMessageController : MonoBehaviour
{
    private GameMaster gameMaster;
    private TMP_Text pauseText;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        pauseText = GetComponent<TMP_Text>();

        //disable the text when the scene starts
        pauseText.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //show pause message if the game is paused
        if (gameMaster.isPaused)
        {
            pauseText.alpha = 1;
        }
        //disable pause message if game is unpaused
        else
        {
            pauseText.alpha = 0;
        }
    }
}
