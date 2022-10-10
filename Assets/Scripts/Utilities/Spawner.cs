using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toGenerate;
    [SerializeField]
    private float generationRate = 2;
    [SerializeField]
    private float nextGenerateTime = 0;

    public void Update()
    {

        if (Time.time >= nextGenerateTime)
        {
            Instantiate(toGenerate, transform.position, Quaternion.identity);
            nextGenerateTime = Time.time + 1 / generationRate;
        }
            
    }
}
