using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    public float tailSwapRate = 3;
    private float timeUntilSwap = 0;
    private HingeJoint2D joint;

    private void Awake()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if(Time.time >= timeUntilSwap)
        {
            var motor = joint.motor;
            motor.motorSpeed *= -1;
            joint.motor = motor;
            timeUntilSwap = Time.time + 1 / tailSwapRate;
        }
    }
}
