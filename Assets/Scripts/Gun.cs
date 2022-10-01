using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Animator animator;
    Vector3 mouseDirection;
    float mouseAngle;
    public Camera main;

   void FixedUpdate()
    {
        mouseDirection = Input.mousePosition;
        mouseDirection = main.ScreenToWorldPoint(mouseDirection);
        mouseDirection.z = 0.0f;
        mouseDirection = mouseDirection - transform.position;
        mouseDirection = mouseDirection.normalized;


        mouseAngle = (Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg) - 0f;
        Quaternion rotation = Quaternion.Euler(0, 0, mouseAngle);

        transform.rotation = rotation;

        Direction();
    }

    void Direction()
    {
        if (mouseAngle < 90 && mouseAngle > -90)
        {
            animator.Play("GunRight");
        }
        if (mouseAngle > 90 || mouseAngle < -90)
        {
            animator.Play("GunLeft");
        }
    }
}
