using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    public float unkonw  = 0.03f;
    public float rotateFactor=14f;
    public float shiftFactor = 4f;
    public bool is2D = false;
    public Camera cam;
    public GameObject follow;
    public BoolObj ThreeD;

    public float TwoDF;
    public float ThreeDF;

    public float hight = 0.93f;
    public float Z = -15;

    public float speed = 0.09f;

    private Vector3 velocity = Vector3.zero;
    Vector3 Position;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.1f;
        TwoDF =50f;
     ThreeDF=15f;
    CharaterController.SwitchD += Change;
        is2D = ThreeD.value;

        Change();
    }

    // Update is called once per frame
    void LateUpdate()
       
    {
        if (!is2D)
        {
            //transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z);
            //transform.position = Vector3.MoveTowards(transform.position,new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z), speed);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z),ref velocity, unkonw);
        }
        else
        {
            //transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, Z);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, Z), speed);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, Z), ref velocity, unkonw);
        }
    }

    void To2d()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, Z);
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(follow.transform.position.x, follow.transform.position.y, Z), speed);
        cam.orthographic = true;
        cam.fieldOfView = TwoDF;
        speed = 1f;

    }
    void To3d()
    {
        transform.rotation = Quaternion.Euler(rotateFactor, 0f,0f);
        cam.orthographic = false;
        //transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z), speed);
        cam.fieldOfView = ThreeDF;
    }

    void Change()
    {

        if (ThreeD.value) {
            To3d();
        }
        else
        {
            To2d();
        }
        is2D = !ThreeD.value;
    }

}
