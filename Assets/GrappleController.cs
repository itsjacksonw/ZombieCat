using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    public LineRenderer line;
    public SpringJoint2D distJoint;

    // Start is called before the first frame update
    void Start()
    {
        distJoint.enabled = false;
        line.enabled = false;
    }

    private void Update()
    {
        if (distJoint.enabled)
        {
            line.SetPosition(1, transform.position);
        }
    }

    public void Grapple()
    {
        Vector2 mousePoint = (Vector2)getCam().ScreenToWorldPoint(Input.mousePosition);
        line.SetPosition(0, mousePoint);
        line.SetPosition(1, transform.position);
        distJoint.connectedAnchor = mousePoint;
        distJoint.enabled = true;
        line.enabled = true;
    }

    public void Ungrapple()
    {
        distJoint.enabled = false;
        line.enabled = false;
    }

    private Camera getCam()
    {
        if(Camera.main!= null)
        {
            return Camera.main;
        } else
        {
            return (Camera)FindObjectOfType(typeof(Camera));
        }
    }
}
