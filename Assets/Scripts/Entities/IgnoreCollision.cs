using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private LayerMask ignore;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ignore == (ignore | (1 << collision.gameObject.layer)))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
