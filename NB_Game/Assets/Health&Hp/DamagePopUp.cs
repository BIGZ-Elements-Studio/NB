using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        StartCoroutine(process());
    }
    IEnumerator process()
    {

        
            
            yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
        yield  break;


    }
    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
    }
}
