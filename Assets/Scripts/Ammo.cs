using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Tooltip("The max amount of ammo that the player is allowed to have")]
    [SerializeField]
    public float maxAmmo = 5;

    [Tooltip("The current amount of ammo that the player has")]
    [SerializeField]
    public float currentAmmo;

    private TMP_Text ammoText;
    private GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;

        ammoText = GetComponent<TMP_Text>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        //only display ammo if game is not paused
        if (!gameMaster.isPaused)
        {
            ammoText.alpha = 1;
            PrintAmmo();
        }
        else
        {
            ammoText.alpha = 0;
        }
    }

    /// <summary>
    /// Prints the amount of ammo that the player has to the ammo message in the UI canvas
    /// </summary>
    private void PrintAmmo()
    {
        ammoText.text = currentAmmo.ToString();
    }
}
