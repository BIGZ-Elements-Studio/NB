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

    public void BigShake()
    {
        animator.SetTrigger("BigShake");
    }

    public void shake()
    {
        animator.SetTrigger("Shake");
    }
}
