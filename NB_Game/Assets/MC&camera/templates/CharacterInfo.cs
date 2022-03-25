using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaInfo", menuName = "New Character")]
public class CharacterInfo : ScriptableObject
{
    public string CharacterName;
    public GameObject Character;
    public int currentHp;
    public int maxHp;

    public int currentEnergy;
    public int MaxEnergy;

    public int EEnergy;
    public int EMax;

    public GameObject Dash;

}
