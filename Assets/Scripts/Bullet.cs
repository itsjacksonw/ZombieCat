using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    private int bounces = 0;

    public AudioSource bounceSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        lastVelocity = rb.velocity;

        if (bounces >= 4)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "gate1")
        {
            float speed = lastVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speed;
            if (collision.gameObject.tag == "Wall")
            {
                bounceSound.Play();
                bounces = bounces + 1;
            }
        }
    }
}
