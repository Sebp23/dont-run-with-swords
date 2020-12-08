using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMessageController : MonoBehaviour
{
    private GameMaster gm;
    private TMP_Text pauseText;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        pauseText = GetComponent<TMP_Text>();

        //disable the text when the scene starts
        pauseText.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isPaused)
        {
            pauseText.alpha = 1;
        }
        else
        {
            pauseText.alpha = 0;
        }
    }
}
