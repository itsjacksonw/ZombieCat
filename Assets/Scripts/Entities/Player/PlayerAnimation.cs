using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    public PlayerDecay playerDecay;
    public Animator playerAnimator;

    private int hurtValue;

    private void Update()
    {
        hurtValue = playerDecay.currentState;
    }

    public void walkAnimation(int currentState)
    {
        switch(hurtValue)
        {
            case 0:
                playerAnimator.Play("walk");
                break;
            case 1:
            case 2:
                playerAnimator.Play("walkh2");
                break;
            case 3:
            case 4:
                playerAnimator.Play("walkh3");
                break;
            case 5:
                playerAnimator.Play("walkh1");
                break;
        }
        
    }

    public void idleAnimation(int currentState)
    {
        

        switch (hurtValue)
        {
            case 0:
                playerAnimator.Play("idle");
                break;
            case 1:
            case 2:
                playerAnimator.Play("idleh1");
                break;
            case 3:
            case 4:
                playerAnimator.Play("idleh2");
                break;
            case 5:
                playerAnimator.Play("idleh3");
                break;
        }
    }

    public void jumpAnimation(int currentState)
    {
        playerAnimator.Play("jump");
    }

    public void fallAnimation(int currentState)
    {
        
        switch (hurtValue)
        {
            case 0:
                playerAnimator.Play("falling");
                break;
            case 1:
            case 2:
                playerAnimator.Play("f1");
                break;
            case 3:
            case 4:
                playerAnimator.Play("f2");
                break;
            case 5:
                playerAnimator.Play("f3");
                break;
        }
    }

    public void attackAnimation(int currentState)
    {
        playerAnimator.Play("Attacking");
    }

    public void deathAnimation(int currentState)
    {

    }
}
