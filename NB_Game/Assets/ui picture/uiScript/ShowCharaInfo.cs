using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class ShowCharaInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void showInfo(GameObject gameObject)
    {
        if (gameObject!=null) {
            
            subCharaCopy script = gameObject.GetComponent<subCharaCopy>();
            if (script != null && script.CharacterName != null)
            {
                text.SetText(script.CharacterName);
            }
            else
            {
                text.SetText("");
            }
        }
    }
}
