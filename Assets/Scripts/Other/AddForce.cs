using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{

    Rigidbody2D rb;
    float dirX;
    float dirY;
    float torque;

    void Start()
    {
        dirX = Random.Range(-8, 8);
        dirY = Random.Range(5, 12);
        torque = Random.Range(5, 15);
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(dirX, dirY), ForceMode2D.Impulse);
        rb.AddTorque(torque, ForceMode2D.Force);

        Object.Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
