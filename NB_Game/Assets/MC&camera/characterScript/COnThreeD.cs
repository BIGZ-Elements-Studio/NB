using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COnThreeD : MonoBehaviour
{
    public Rigidbody rb;
    public TeamObj V;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(V.xVelocity, rb.velocity.y,V.zVelocity) ;
    }
}
