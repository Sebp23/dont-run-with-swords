using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    /// <summary>
    /// This class makes sure that there is only one instance of the background music
    /// If there is more than one instance, then it will destroy the extra instance
    /// </summary>
    private void Awake()
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        if (musicObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
