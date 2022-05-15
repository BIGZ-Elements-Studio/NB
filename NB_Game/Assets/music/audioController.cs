using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioSource BGM;
    public AudioClip normal;
    public AudioClip ttk;
    public bool isTtk;

    private void Start()
    {
        //scriptController.Switch += changeBGM;
        changeBGM();
    }

    private void changeBGM()
    {

        isTtk = !isTtk;
        BGM.Stop();
        if (isTtk)
        {
            BGM.clip = ttk;
        }
        else
        {
            BGM.clip = normal;
        }

        BGM.Play();
    }
}
