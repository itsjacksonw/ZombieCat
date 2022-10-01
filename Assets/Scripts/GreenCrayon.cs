using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCrayon : MonoBehaviour
{

    private Vector3 direction;
    public Transform player;
    private Rigidbody2D body;
    public float speed = 1.0f;
    public GameObject GreenBall;

    private float nextShotTime = 2.0f;
    public float shootSpeed;
    public float shootCooldown;

    public float dis;
    public int health = 3;

    public GameObject GameManager;
    GameManager gm;

    public AudioSource hurtSound;
    public AudioSource deathSound;
    public AudioSource shootSound;

    void Start()
    {
        gm = GameManager.GetComponent<GameManager>();
        body = transform.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction.Normalize();

        if (health <= 0)
        {
            gm.kills = gm.kills + 1;
            Death();
        }

        Rotate();

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < dis)
        {
            

            body.velocity = new Vector2(direction.x * speed, direction.y * speed);

            if (Time.time > nextShotTime)
            {
                Shoot();
            }
        }

        if (distance > dis)
        {
            body.velocity = new Vector2(direction.x * 0, direction.y * 0);
        }
        
    }

    void Rotate()
    {
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = rotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Damage(1);
        }
    }

    void Damage(int damage)
    {
        health = health - damage;
        hurtSound.Play();
    }

    void Death()
    {

        deathSound.Play();
        Destroy(this.gameObject);
    }

    void Shoot()
    {
        GameObject shot;

        shootSound.Play();
        
        int num = Random.Range(0, 5);
        if (num == 1)
        {
            shootCooldown = shootCooldown - 0.1f;
        }
        if (num == 2)
        {
            shootCooldown = shootCooldown - 0.05f;
        }
        if (num == 3)
        {
            shootCooldown = shootCooldown - 0f;
        }
        if (num == 4)
        {
            shootCooldown = shootCooldown + 0.1f;
        }

        if (shootCooldown <= 0)
        {
            shootCooldown = 0.01f;
        }
        nextShotTime = Time.time + shootCooldown;

        shot = Instantiate(GreenBall, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shootSpeed, ForceMode2D.Impulse);
    }

}
