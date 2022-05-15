using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    
    public void shake()
    {
        Debug.Log("2");
        animator.SetTrigger("Shake");
    }
}
