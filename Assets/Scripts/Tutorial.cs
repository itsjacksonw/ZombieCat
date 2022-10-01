using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    private float timeTracker = 100000000;



    public Camera main;
    public Camera second;

    void Update()
    {
        if (Time.time > timeTracker)
        {
            StartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            main.enabled = false;
            second.enabled = true;
            timeTracker = Time.time + 3.0f;

        }
    }

    void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }


}
