using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float theTime;
    void Start()
    {
        theTime = 20f + Time.time;
        
    }

    void FixedUpdate()
    {
        if (Time.time > theTime)
        {
            Destroy(this.gameObject);
        }
    }
}
