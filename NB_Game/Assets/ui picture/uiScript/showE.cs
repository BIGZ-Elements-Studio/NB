using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class showE : MonoBehaviour
{
    public GameObject follow;
    public TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        text.SetText("");
        string output;
        if (follow.GetComponentInChildren<CharaterController>().team.currentC==1)
         {
              output = follow.GetComponentInChildren<CharaterController>().One.GetComponent<subCharacter>().EPassedT.ToString();
         }
         else if (follow.GetComponentInChildren<CharaterController>().team.currentC == 2)
         {
              output = follow.GetComponentInChildren<CharaterController>().Two.GetComponent<subCharacter>().EPassedT.ToString();
         }
         else if (follow.GetComponentInChildren<CharaterController>().team.currentC == 3)
         {
              output = follow.GetComponentInChildren<CharaterController>().Three.GetComponent<subCharacter>().EPassedT.ToString();
         }
         else
         {
              output = follow.GetComponentInChildren<CharaterController>().Four.GetComponent<subCharacter>().EPassedT.ToString();
         }
        if (output.Length > 3)
        {
            output = output.Substring(0, 3);
        }
        text.SetText(output);
    }
}
