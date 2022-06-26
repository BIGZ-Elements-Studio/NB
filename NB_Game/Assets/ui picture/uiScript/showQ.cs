
using UnityEngine;
using UnityEngine.UI;

public class showQ : MonoBehaviour
{
    public Gradient color;
    public Image background;
    private void Awake()
    {
        background = GetComponent<Image>();

    }
    // Update is called once per frame
    public void showEnergy(int Energy,int QEnergy)
    {
        if (QEnergy<Energy)
        {
            background.color = color.Evaluate(1f);
        }
        else
        {
            background.color = color.Evaluate(0);
        }
        


    }
}
