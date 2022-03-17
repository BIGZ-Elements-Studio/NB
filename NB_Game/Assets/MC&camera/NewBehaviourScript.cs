using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, follow.transform.position.y), 0.01f);
    }
}
