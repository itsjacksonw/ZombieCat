using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Sprite notHover, hover;
    public SpriteRenderer screen;

    private void OnMouseEnter()
    {
        screen.sprite = hover;
    }

    private void OnMouseExit()
    {
        screen.sprite = notHover;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
