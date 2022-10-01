using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{

    public bool needsHeal = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Brain")
        {
            needsHeal = true;
            Destroy(col.gameObject);
        }
    }
}
