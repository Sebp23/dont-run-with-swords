using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    [Tooltip("The max amount of ammo that the player is allowed to have")]
    [SerializeField]
    public float maxAmmo = 5;

    [Tooltip("The current amount of ammo that the player has")]
    [SerializeField]
    public float currentAmmo;

    [Tooltip("The ammo sprite next to ammo counter.")]
    [SerializeField]
    private CanvasGroup ammoSprite;

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
            ammoSprite.alpha = 1;
            PrintAmmo();
        }
        else
        {
            ammoText.alpha = 0;
            ammoSprite.alpha = 0;
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
