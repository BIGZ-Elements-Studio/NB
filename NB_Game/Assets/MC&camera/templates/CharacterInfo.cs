using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaInfo", menuName = "New Character")]
public class CharacterInfo : ScriptableObject
{
    public string CharacterName;
    //public GameObject Character;
    //血量
    public int currentHp;
    public int maxHp;
    //韧性
    public int maxPoiseHealth;
    public int poiseHealth;
    //无敌
    public bool Immortal;

    public int EEnergy;
    public int EMax;

    public GameObject Dash;


    public int atk;
    public int def;
    public int MoveSpeedRate;
    //暴击
    public int CritRate;
    public int CritDamage;
    //冷却倍率
    public int QCoolDownRate=100;
    public int ECoolDownRate=100;
    //充能
    public int RechargeRate=100;
    public int currentEnergy;
    public int MaxEnergy;
}
