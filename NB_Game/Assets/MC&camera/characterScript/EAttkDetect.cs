using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EAttkDetect : AnyCharaHp
{

    public delegate void simple();
    public static simple successfulDash;

    public void takeDamage(int damage, int hardness)
    {
        if (successfulDash != null)
        {
            successfulDash();
        }

    }

    public void destroy()
    {
        Destroy(gameObject);

    }
}
