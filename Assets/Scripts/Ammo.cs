using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    public float maxAmmo = 5;
    
    [SerializeField]
    public float currentAmmo;

    private TMP_Text ammoText;
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 5;

        ammoText = GetComponent<TMP_Text>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            ammoText.alpha = 1;
            PrintAmmo();
        }
        else
        {
            ammoText.alpha = 0;
        }
    }

    private void PrintAmmo()
    {
        ammoText.text = currentAmmo.ToString();
    }
}
