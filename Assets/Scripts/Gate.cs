using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public GameObject GameManager;
    GameManager gm;

    public AudioSource gateSound;

    void Start()
    {
        gm = GameManager.GetComponent<GameManager>();
    }



    void Update()
    {
        

        if (gm.kills == 4 && (this.gameObject.tag == "gate1"))
        {
            gateSound.Play();
            Destroy(this.gameObject);
        }

        if (gm.kills == 7 && (this.gameObject.tag == "gate2"))
        {
            gateSound.Play();
            Destroy(this.gameObject);
        }

        if (gm.kills == 16 && (this.gameObject.tag == "gate3"))
        {
            gateSound.Play();
            Destroy(this.gameObject);
        }

        if (gm.kills == 19 && (this.gameObject.tag == "gate4"))
        {
            gateSound.Play();
            Destroy(this.gameObject);
        }

        if (gm.kills == 23 && (this.gameObject.tag == "gate5"))
        {
            gateSound.Play();
            Destroy(this.gameObject);
        }
    }
}
