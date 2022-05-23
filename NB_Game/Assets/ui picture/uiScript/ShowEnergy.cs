using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowEnergy : MonoBehaviour
{
    Slider thisSlider;
    private void Start()
    {
        thisSlider = GetComponent<Slider>();
    }
    public void showEnenergy(int Energy, int MaxEnengry)
    {
        thisSlider.value = (float)Energy / (float)MaxEnengry;
    }
}
