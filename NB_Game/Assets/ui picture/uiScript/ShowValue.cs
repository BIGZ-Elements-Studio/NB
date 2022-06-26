using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowValue : MonoBehaviour
{
    Slider thisSlider;
    private void Awake()
    {
        thisSlider = GetComponent<Slider>();
    }
    public void showValue(int Energy, int MaxEnengry)
    {
        thisSlider.value = (float)Energy / (float)MaxEnengry;
    }
}
