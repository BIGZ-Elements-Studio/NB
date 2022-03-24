using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showQ : MonoBehaviour
{
    public GameObject follow;

    public float smoothTime;
    public TextMeshProUGUI text;
    public Gradient color;
    public Image background;

    float showEn;

    private void Start()
    {
        smoothTime = 0.2f;
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        //background.color = color.Evaluate((float)follow.GetComponentInChildren<subCharacter>().currentEne / follow.GetComponentInChildren<subCharacter>().QEnergy);
        float realshow = (float)follow.GetComponentInChildren<subCharacter>().currentEne / follow.GetComponentInChildren<subCharacter>().QEnergy;
        if(showEn!= realshow)
        {
            showEn += Time.unscaledDeltaTime * (realshow-showEn)/smoothTime;
        }
        
        background.color = color.Evaluate(showEn);
        text.SetText("");
        string output = follow.GetComponentInChildren<subCharacter>().QPassedT.ToString();

        if (output.Length > 3)
        {
            output = output.Substring(0, 3);
            text.SetText(output);
        }
       
    }
}
