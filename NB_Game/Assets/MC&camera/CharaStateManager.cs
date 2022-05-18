using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void setActive(bool x)
    {
        Behaviour[] behaviours = GetComponents<Behaviour>();
        foreach (var item in behaviours)
        {
            item.enabled = x;
        }
        GetComponent<SpriteRenderer>().enabled = x;
        GetComponent<CharaStateManager>().enabled = true;

    }

  



}
