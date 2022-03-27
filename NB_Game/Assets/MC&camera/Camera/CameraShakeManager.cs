using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CameraShakeManager : MonoBehaviour
{

    public ShakeData da;
    private void Start()
    {
       // CameraShakerHandler.Shake(da);
    }
    public void shake()
    {
        CameraShakerHandler.Shake(da);
    }

    public void shake(ShakeData data)
    {
        CameraShakerHandler.Shake(data);
    }
}
