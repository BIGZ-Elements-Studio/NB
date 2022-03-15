using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDbehavior : MonoBehaviour
{
    public Transform cam;
    void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
