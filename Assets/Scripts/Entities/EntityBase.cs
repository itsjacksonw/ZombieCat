using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{


    //Base Stats
    [SerializeField]
    public float maxHealth = 100F;
    public float health;
    [SerializeField]
    protected int attackPower = 2;
    [SerializeField]
    public int speed = 5;

    public void Start()
    {
        health = maxHealth;
    }

    public void Update()
    {
        if (this.health <= 0)
        {
            onDeath();
        }
        Move();
    }

    public void takeDamage(int amt)
    {
        Debug.Log("Taking damage");
        health -= amt;
    }

    public abstract void Move();

    public abstract void onDeath();
}
