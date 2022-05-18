using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    public float smoothTime = 0.03f;
    public float rotateFactor = 14f;
    public float shiftFactor = 4f;
    public bool is2D = false;
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
        TwoDF = 50f;
        ThreeDF = 15f;
        CharaterController.SwitchD += Change;
        is2D = false;

        Change();
    }

    // Update is called once per frame
    void LateUpdate()

    {
        if (!is2D)
        {

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z), ref velocity, smoothTime);
        }
        else
        {

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, Z), ref velocity, smoothTime);
        }
    }

    void To2d()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, Z), speed);
        speed = 1f;

    }
    void To3d()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y + hight + shiftFactor, Z), speed);
    }

    void Change()
    {

        if (ThreeD.value)
        {
            To3d();
        }
        else
        {
            To2d();
        }
        is2D = !ThreeD.value;
    }

}
