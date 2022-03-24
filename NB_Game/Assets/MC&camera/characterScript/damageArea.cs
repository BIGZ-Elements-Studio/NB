using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class damageArea:MonoBehaviour
{
    public GameObject follow;
    public TeamObj chara;


    private void Update()
    {
        int direction = -1;
        if (chara.faceRight)
        {
            direction = 1;
        }
       

        transform.position = new Vector3(follow.transform.position.x+1f* direction, follow.transform.position.y, follow.transform.position.z);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
