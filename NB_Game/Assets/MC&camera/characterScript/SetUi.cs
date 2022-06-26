using UnityEngine;
using UnityEngine.Events;

public class SetUi : MonoBehaviour
{
   
    GameObject canvas;

    [SerializeField] GameObject UiPrefeb;
    [SerializeField] GameObject CanvasPrefeb;


    UiManager UiManager;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            canvas=Instantiate(CanvasPrefeb);
        }
       GameObject g= Instantiate(UiPrefeb, canvas.transform);
        UiManager = g.GetComponent<UiManager>();

        GetComponent<CharaterController>().setButton(g.GetComponent<ChangeButton>().FirstButton, g.GetComponent<ChangeButton>().SecondButton, g.GetComponent<ChangeButton>().ThirdButton);
    }
    public  void showEnergy(int Energy, int MaxEnergy, int QEnergy)
    {
        UiManager?.showEnergy( Energy,  MaxEnergy,  QEnergy);
    }
    public  void showHP(int HP, int MaxHP)
    {
        UiManager?.showHP(HP, MaxHP);
    }


    public void EShowCD(float length, float TimePassed)
    {
        UiManager?.EShowCD(length, TimePassed);
    }


    public void Ereset()
    {
        UiManager?.Ereset();
    }
    }
