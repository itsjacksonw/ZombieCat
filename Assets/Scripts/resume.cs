using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resume : MonoBehaviour
{

    
    private bool mouseEnter = false;

    void Update()
    {
       
        if (Input.GetButton("Fire1") && mouseEnter)
        {
            ResumeGame();
        }

        
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
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
