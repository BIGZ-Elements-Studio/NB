using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showDirection : MonoBehaviour
{
    public TeamObj follow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction =new Vector2 (-follow.xVelocity,follow.zVelocity);
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0,0, angle);
    }
}
