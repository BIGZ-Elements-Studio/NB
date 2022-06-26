using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] showE EScript;
    [SerializeField]  ShowValue Qscript;
    [SerializeField] showQ showQ;
    [SerializeField] ShowValue HpScript;

   public void showEnergy(int Energy, int MaxEnergy,int QEnergy)
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.Qscript?.showValue(Energy, MaxEnergy);
        UiManager.showQ?.showEnergy(Energy,QEnergy);
    }
    public void showHP(int HP, int MaxHP)
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.HpScript?.showValue(HP, MaxHP);
    }


    public void EShowCD(float length, float TimePassed)
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.EScript?.ShowCD(length, TimePassed);
    }

    
    public void Ereset()
    {
        UiManager UiManager = FindObjectOfType<UiManager>();
        UiManager.EScript?.reset();
    }
}
