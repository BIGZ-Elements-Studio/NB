using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float rotateFactor=14f;
    public float shiftFactor = 4f;
    private bool is3D = false;
    public Camera cam;
    public GameObject follow;
    public BoolObj ThreeD;

    // Start is called before the first frame update
    void Start()
    {
        ThreeD.value= true;
        CharaterController.SwitchD += Change; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void To3d()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y- shiftFactor, transform.position.z) ;
        cam.orthographic = true;
    }
    void To2d()
    {
        transform.rotation = Quaternion.Euler(rotateFactor, 0f,0f);
        cam.orthographic = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + shiftFactor, transform.position.z);
    }

    void Change()
    {
        if (is3D) {
            To2d();
        }
        else
        {
            To3d();
        }
        is3D = !is3D;
    }

}
