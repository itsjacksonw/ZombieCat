using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    private float timeTracker = 100000000;
    private bool mouseEnter = false;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite oldSprite;

    public Camera main;
    public Camera second;

    void Update()
    {
        if (mouseEnter)
        {
            spriteRenderer.sprite = newSprite;
        }

        if (!mouseEnter)
        {
            spriteRenderer.sprite = oldSprite;
        }

        if (Input.GetButton("Fire1") && mouseEnter)
        {
            StartGame();
        }

        if (Time.time > timeTracker)
        {
            
        }
    }

    void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Tutorial");
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
