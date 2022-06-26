using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System.Threading.Tasks;

public class showE : MonoBehaviour
{
   
    [SerializeField]
    private TextMeshProUGUI text;

    bool running;
    private StringBuilder timerTxtBuilder = new StringBuilder();
    private void Awake()
    {
        running = false;
    }
    IEnumerator UpdateCD(float length,float TimePassed)
    {
        running = true;
        int TotalTime = (int)length * 10;
        int PassedTime = (int)TimePassed * 10;

        int time= TotalTime - PassedTime;
        while (time>=1)
        {

            yield return new WaitForSecondsRealtime(0.1f);
            time -= 1;
            timerTxtBuilder.Length = 0;
            timerTxtBuilder.Append(((double)time) / 10);
            text.SetText(timerTxtBuilder.ToString());
        }
        text.SetText("");
        running = false ;
        yield break;
    }

    public void ShowCD(float length, float TimePassed)
    {
        
            StopAllCoroutines();
        StartCoroutine(UpdateCD(length, TimePassed));
    }

    public void reset()
    {
        StopAllCoroutines();
        text.SetText("");
    }
}
