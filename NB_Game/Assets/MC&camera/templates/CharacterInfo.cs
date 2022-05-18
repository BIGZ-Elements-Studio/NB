using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaInfo", menuName = "New Character")]
public class CharacterInfo : ScriptableObject
{
    public string CharacterName;
    //public GameObject Character;
    //Ѫ��
    public int currentHp;
    public int maxHp;
    //����
    public int maxPoiseHealth;
    public int poiseHealth;
    //�޵�
    public bool Immortal;

    public int EEnergy;
    public int EMax;

    public GameObject Dash;


    public int atk;
    public int def;
    public int MoveSpeedRate;
    //����
    public int CritRate;
    public int CritDamage;
    //��ȴ����
    public int QCoolDownRate=100;
    public int ECoolDownRate=100;
    //����
    public int RechargeRate=100;
    public int currentEnergy;
    public int MaxEnergy;
}
