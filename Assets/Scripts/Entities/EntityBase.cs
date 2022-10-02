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
    public float speed = 5;

    public GameObject bloodyParticles;

    public AudioSource takeDamageSound;

    public void Start()
    {
        health = maxHealth;
    }

    public void Update()
    {
        if(this.health <= 0)
        {
            onDeath();
        }

        Move();
    }


    public void takeDamage(float amt)
    {
        health -= amt;
        Instantiate(bloodyParticles, transform.position, Quaternion.identity);
        takeDamageSound.Play();
    }

    public abstract void Move();

    public abstract void onDeath();
}
