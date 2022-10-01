using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnimator;

    public void walkAnimation(int currentState)
    {
        playerAnimator.Play("walk");
    }

    public void idleAnimation(int currentState)
    {
        playerAnimator.Play("idle");
    }

    public void jumpAnimation(int currentState)
    {
        playerAnimator.Play("jump");
    }

    public void fallAnimation(int currentState)
    {
        playerAnimator.Play("falling");
    }

    public void attackAnimation(int currentState)
    {
        playerAnimator.Play("Attacking");
    }

    public void deathAnimation(int currentState)
    {

    }
}
