
using UnityEngine;
using UnityEngine.UI;

public class showDashCoolDown : MonoBehaviour
{
    public Gradient color;
    public Image background;
    private void Start()
    {
        background = GetComponent<Image>();

    }
    // Update is called once per frame
    public void showDashint(int Energy, int QEnergy)
    {
        if (QEnergy < Energy)
        {
            background.color = color.Evaluate(1f);
        }
        else
        {
            background.color = color.Evaluate(0);
        }



    }
}
