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
        rb.velocity = new Vector3(V.xVelocity/Time.timeScale, 0,V.zVelocity/Time.timeScale);
    }
}
