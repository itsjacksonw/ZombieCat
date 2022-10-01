using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private Vector2 lastVelocity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bounce")
        {
            float speed = lastVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            transform.GetComponent<Rigidbody2D>().velocity = direction * speed * 1.5f;
        }
    }
}
