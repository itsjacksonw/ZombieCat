using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed;
    public float jumpSpeed;
    public float launchSpeed;
    public Rigidbody2D body;
    private Vector2 lastVelocity;

    public bool isGrounded;
    public bool isLaunching;
    private bool hasHurt;
    private bool hasHit;

    public GameObject bloodSplat;

    private PlayerController playerController;

    public AudioSource walkSound, jumpSound, wallHit, bounceSound;

    private void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();

        playerController = GetComponent<PlayerController>();
        walkSpeed = playerController.speed;
    }

    private void FixedUpdate()
    {
        lastVelocity = body.velocity;
        if (isLaunching)
        {
            body.freezeRotation = false;
            if (body.velocity.x > 0)
            {
                body.angularVelocity = -200.0f;
            }

            if (body.velocity.x < 0)
            {
                body.angularVelocity = 200.0f;
            }


        }
        else
        {
            body.rotation = 0;
            body.freezeRotation = true;
        }
    }

    public void Walk(float moveX)
    {
        if (!isLaunching) {
            body.velocity = new Vector2(moveX * walkSpeed, body.velocity.y);

            if (moveX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (moveX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (moveX == 0 && walkSound.isPlaying)
            {
                walkSound.Stop();
            }

            if (moveX != 0 && isGrounded)
            {
                playWalkSound();
            }
        }   
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            jumpSound.Play();
        }
    }

    public void Launch()
    {
        if (isGrounded == true)
        {
            jumpSound.Play();
            isLaunching = true;
            Vector3 mousePosition = Vector3.zero;
            if (Camera.main != null)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            } else
            {
                Camera cam = (Camera)FindObjectOfType(typeof(Camera));
                if (cam)
                {
                    mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                }
                
            }
            
            Vector2 direction = mousePosition - transform.position;
            direction = direction.normalized;

            body.AddForce(direction * launchSpeed, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        

        if (collision.gameObject.tag == "Enemy" && isLaunching && !hasHit)
        {


            hasHit = true;
            playerController.health -= 1;
            Instantiate(bloodSplat, new Vector2(body.transform.position.x, body.transform.position.y), Quaternion.identity);


            collision.gameObject.GetComponent<EntityBase>().takeDamage(2f);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            isGrounded = true;
            isLaunching = false;
            hasHurt = false;
            hasHit = false;
        }

        if (collision.gameObject.tag == "Wall" && isLaunching && !hasHurt)
        {
            hasHurt = true;
            playerController.health -= 5;
            Instantiate(bloodSplat, new Vector2(body.transform.position.x, body.transform.position.y), Quaternion.identity);

            wallHit.Play();
        }


        if (collision.gameObject.tag == "Bounce")
        {
            float speed = lastVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            body.velocity = direction * speed * 1.5f;
            bounceSound.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    public void moveTo(Transform pos)
    {
        this.transform.position = pos.position;
    }

    private void playWalkSound()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }



}
