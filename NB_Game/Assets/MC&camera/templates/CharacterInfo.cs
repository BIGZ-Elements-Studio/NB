using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaInfo", menuName = "New Character")]
public class CharacterInfo : ScriptableObject
{
    public string CharacterName;
    public GameObject Character;
    public int attckNum;
    public int currentHp;
    public int maxHp;
    public ArrayList Damage = new ArrayList();
    public ArrayList InterVal = new ArrayList();

    public ArrayList QDamage = new ArrayList();
    public ArrayList QInterVal = new ArrayList();

    public ArrayList EDamage = new ArrayList();
    public ArrayList EInterVal = new ArrayList();
}
