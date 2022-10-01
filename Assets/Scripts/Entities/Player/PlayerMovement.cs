using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed;
    public float jumpSpeed;
    public float launchSpeed;
    public Rigidbody2D body;

    public bool isGrounded;
    public bool isLaunching;
    private bool hasHurt;

    public GameObject bloodSplat;

    private PlayerController playerController;

    private void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();

        playerController = GetComponent<PlayerController>();
        walkSpeed = playerController.speed;
    }

    private void FixedUpdate()
    {
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
        }   
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    public void Launch()
    {
        if (isGrounded == true)
        {
            isLaunching = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            direction = direction.normalized;

            body.AddForce(direction * launchSpeed, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isLaunching = false;
            hasHurt = false;
        }

        if (collision.gameObject.tag == "Wall" && isLaunching && !hasHurt)
        {
            hasHurt = true;
            playerController.health -= 5;
            Instantiate(bloodSplat, new Vector2(body.transform.position.x, body.transform.position.y), Quaternion.identity);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

    }



}
