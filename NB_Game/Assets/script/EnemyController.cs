using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(jump());
    }

    IEnumerator jump()
    {
        while (true) 
        {
            rb.velocity = new Vector2(0,20);
            yield return new WaitForSeconds(1f);
        }
        
    }
    
}
