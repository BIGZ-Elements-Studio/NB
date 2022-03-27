using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUiScript : TwoDbehavior
{
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        gameObject.layer = 11;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
