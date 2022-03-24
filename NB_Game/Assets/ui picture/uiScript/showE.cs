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
        string output = follow.GetComponentInChildren<subCharacter>().EPassedT.ToString();
        if (output.Length > 3)
        {
            output= output.Substring(0, 3);
            text.SetText(output);
        }
        

        //m_TextComponent.text = follow.current.Character.GetComponent<subCharacter>().EPassedT.ToString().Substring(0,3);
    }
}
