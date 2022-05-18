using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] GameObject PopUptextPrefab;
    
    public static void DoFloatingText(Vector3 Position, int damage)
    {

        EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
        GameObject floatingText = Instantiate(effectsManager.PopUptextPrefab, new Vector3(Position.x, Position.y, Position.z), Quaternion.identity);
        floatingText.layer = 11;
        floatingText.GetComponent<DamagePopUp>().Setup(damage);
        
    }
}
