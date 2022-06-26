using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public enum scene
    {
        SampleScene,
    }
    public static void load(scene s)
    {
        SceneManager.LoadScene(s.ToString());
    }
}
