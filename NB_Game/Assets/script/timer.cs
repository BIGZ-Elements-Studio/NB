using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class timer : MonoBehaviour
{
   
    public TextMeshProUGUI text;
    

    public double time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(QProcess());
    }

    void Update()
    {
        
            text.SetText(time.ToString());

    }

    IEnumerator QProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            time += 0.1;
            
        }
        
       
    }

}
