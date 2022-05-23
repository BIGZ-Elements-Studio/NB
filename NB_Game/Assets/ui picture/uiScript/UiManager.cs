using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]  GameObject ECoolDown;
    [SerializeField]  GameObject QSlider;
    showE EScript;
     ShowEnergy Qscript;
    [SerializeField] GameObject Q;
    showQ showQ;
    private void Awake()
    {
        EScript = ECoolDown.GetComponent<showE>();
        Qscript = QSlider.GetComponent<ShowEnergy>();
        showQ = Q.GetComponent<showQ>();
    }

   
    public static void showEnergy(int Energy, int MaxEnergy,int QEnergy)
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.Qscript.showEnenergy(Energy, MaxEnergy);
        UiManager.showQ.showEnergy(Energy,QEnergy);
    }

    public static void EShowCD(float length, float TimePassed)
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.EScript.ShowCD(length, TimePassed);
    }

    
    public static void Ereset()
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.EScript.reset();
    }
}
