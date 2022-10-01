using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    private PlayerMovement playerMovement;
    private PlayerDecay playerDecay;
    private PlayerAnimation playerAnimation;
    private PlayerEat playerEat;

    public float maxHealth = 100.0f;
    public float health;
    public Slider healthBar;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerDecay = GetComponent<PlayerDecay>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerEat = GetComponent<PlayerEat>();

        health = maxHealth;
        
    }

    void Update()
    {
        ProcessInputs();

        CheckDecayState();

        healthBar.value = health / maxHealth;

        if (health < 1)
        {
            Death();
        }

    }
    void ProcessInputs()
    {
        /* Directional Input */
        float moveX = Input.GetAxisRaw("Horizontal");


        /* Walking */
        playerMovement.Walk(moveX);

        if (moveX == 0 && playerMovement.body.velocity.y == 0)
        {
            playerAnimation.idleAnimation(playerDecay.currentState);
        }

        else if (playerMovement.body.velocity.y == 0)
        {
            playerAnimation.walkAnimation(playerDecay.currentState);
        }

        else if (playerMovement.body.velocity.y < 0)
        {
            playerAnimation.fallAnimation(playerDecay.currentState);
        }


        /* Jumping */
        if (Input.GetButtonDown("Jump")) {

            playerMovement.Jump();

        }

        /* Launch */
        if (Input.GetButtonDown("Launch"))
        {
            playerMovement.Launch();
        }

        
        
        

    }

    void CheckDecayState()
    {

        if (playerEat.needsHeal)
        {
            playerEat.needsHeal = false;

            playerMovement.jumpSpeed = 18;
            playerMovement.walkSpeed = 6;

            playerDecay.Heal();
            health = maxHealth;
        }



        if (health < 75)
        {
            if (playerDecay.currentState == 0)
            {
                playerDecay.LoseLegOne(playerMovement);
            }
        }
        if (health < 50)
        {
            if (playerDecay.currentState == 1)
            {
                playerDecay.LoseEarOne(playerMovement);
            }
        }
        if (health < 25)
        {
            if (playerDecay.currentState == 2)
            {
                playerDecay.LoseLegTwo(playerMovement);
            }

            if (health < 15)
            {
                if (playerDecay.currentState == 3)
                {
                    playerDecay.LoseEarTwo(playerMovement);
                }
            }
            if (health < 10)
            {
                if (playerDecay.currentState == 4)
                {
                    playerDecay.LoseEyeOne(playerMovement);
                }
            }
            if (health <= 0)
            {
                Death();
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("you died");
    }

}
