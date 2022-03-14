using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// test  your method here using keyboad(use keys that won't be used in game,dont use things such as AWSD)
//only for test purpose, will be remove after finish 
//!!do not make anything depends on this script!!
public class Tests : MonoBehaviour
{

    public delegate void simpleD();
    public static simpleD SwitchD;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //haichau test camera movement 

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SwitchD != null)
            {
                SwitchD();
                Debug.Log("changed");
            }
            Debug.Log("Inputed");
        }
    }
}
