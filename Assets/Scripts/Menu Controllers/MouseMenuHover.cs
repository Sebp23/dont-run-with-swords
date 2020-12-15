using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMenuHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //set material to white on start
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter()
    {
        //set material to red when mouse enters box collider
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        //set material back to white when mouse leave box collider
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        //set material to red when mouse clicks
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseUp()
    {
        //set material to white
        GetComponent<Renderer>().material.color = Color.white;
    }
}
