using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CameraController : MonoBehaviour
{
    public float rotateFactor=14f;
    public float shiftFactor = 4f;
    public bool is2D = false;
    public Camera cam;
    public GameObject follow;
    public BoolObj ThreeD;
    //field of view 
    public float TwoDF;
    public float ThreeDF;


    // Start is called before the first frame update
    void Start()
    {
        //speed = 0.1f;
        TwoDF =50f;
     ThreeDF=20f;
    CharaterController.SwitchD += Change;
        is2D = ThreeD.value;

        Change();
    }

    // Update is called once per frame

    void To2d()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        cam.orthographic = true;
        cam.fieldOfView = TwoDF;

    }
    void To3d()
    {
        transform.rotation = Quaternion.Euler(rotateFactor, 0f,0f);
        cam.orthographic = false;
      
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
