using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDecay : MonoBehaviour
{

    public PlayerController playerController;

    public int currentState = 0;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }


    void Update()
    {
        playerController.health -= Time.deltaTime * 1.5f;
    }

    public void LoseLegOne(PlayerMovement playerMovement)
    {
        currentState = 1;

        playerMovement.walkSpeed -= 1;
        playerMovement.jumpSpeed -= 3;
    }

    public void LoseLegTwo(PlayerMovement playerMovement)
    {
        currentState = 3;

        playerMovement.walkSpeed -= 1;
        playerMovement.jumpSpeed -= 5;
    }

    public void LoseEarOne(PlayerMovement playerMovement)
    {
        currentState = 2;
    }

    public void LoseEarTwo(PlayerMovement playerMovement)
    {
        currentState = 4;
    }

    public void LoseEyeOne(PlayerMovement playerMovement)
    {
        currentState = 5;
    }


    public void Heal()
    {
        currentState = 0;
    }

}
