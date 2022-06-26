using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUiManager : MonoBehaviour
{
    [SerializeField] GameObject First;
    [SerializeField] GameObject Second;
    [SerializeField] GameObject Third;


    //[SerializeField] GameObject Fourth;
    public static void setName(int i, GameObject obj)
    {
        GlobalUiManager GlobalUiManager = FindObjectOfType<GlobalUiManager>();
        if (i==1)
        {
            GlobalUiManager.First?.GetComponent<ShowCharaInfo>().showInfo(obj);
        }
        else if (i==2)
        {
            GlobalUiManager.Second?.GetComponent<ShowCharaInfo>().showInfo(obj);
        }
        else 
        {
            GlobalUiManager.Third?.GetComponent<ShowCharaInfo>().showInfo(obj);
        }
        
    }
}
