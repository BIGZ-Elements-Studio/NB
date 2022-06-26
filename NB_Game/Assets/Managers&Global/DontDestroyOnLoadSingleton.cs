using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadSingleton : MonoBehaviour
{
    // Start is called before the first frame update

    private static MonoBehaviour instance;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
