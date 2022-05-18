using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    public void SmoothSlowdon(float timescale)
    {
        Time.timeScale = timescale;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale / 2;
    }
    public void RoughSlowdon(float timescale)
    {
        Time.timeScale = timescale;
    }
    public void RoughSlowdonByFactor(float percent)
    {

    }
}
