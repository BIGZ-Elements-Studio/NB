using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COnTwoD : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    public BoxCollider bc;

    public int gravitS;
    public float Jumpheight;
    public float speed;
    public int direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        gravitS = 7;
        Jumpheight = 20f;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }

        if (Input.GetKey(KeyCode.Space) & IsGrounded())
        {
            rb.velocity = new Vector2(direction * speed / Time.timeScale, Jumpheight / Time.timeScale);
        }




    }

    bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, Vector3.down,1f);
        return Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.down, transform.rotation, 0.5f);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(direction * speed / Time.timeScale, rb.velocity.y,0);
    }
}
