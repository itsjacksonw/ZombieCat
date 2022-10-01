using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{


    private bool mouseEnter = false;

    void Update()
    {

        if (Input.GetButton("Fire1") && mouseEnter)
        {
            Application.Quit();
        }


    }

    

    void OnMouseEnter()
    {
        mouseEnter = true;
    }

    void OnMouseExit()
    {
        mouseEnter = false;
    }

}
