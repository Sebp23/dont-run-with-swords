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

    private CanvasGroup canvasGroup;
    private TMP_Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 5;

        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PrintAmmo();
    }

    private void PrintAmmo()
    {
        messageText.text = currentAmmo.ToString();
    }
}
