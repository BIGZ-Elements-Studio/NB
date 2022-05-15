using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    public GameObject TalkArea;
    private void Start()
    {
        TalkArea.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        TalkArea.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        TalkArea.SetActive(false);
    }

}
