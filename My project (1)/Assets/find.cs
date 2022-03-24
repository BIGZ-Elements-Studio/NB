using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find : MonoBehaviour
{
    public GameObject game;
    public father script;
    void Start()
    {
        script = game.GetComponent<father>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
