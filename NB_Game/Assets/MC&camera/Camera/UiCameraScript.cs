using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCameraScript : MonoBehaviour
{

    GameObject follow;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.Find("MainCamera");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        cam.fieldOfView = follow.GetComponent<Camera>().fieldOfView;
        cam.orthographic = follow.GetComponent<Camera>().orthographic; ;
    }
}
