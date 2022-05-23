using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaInfo", menuName = "New Character")]
public class CharacterInfo : ScriptableObject
{
    public string CharacterName;
    //public GameObject Character;
    //ÑªÁ¿
    public int currentHp;
    public int maxHp;
    //ÈÍÐÔ
    public int maxPoiseHealth;
    public int poiseHealth;
    //ÎÞµÐ
    public bool Immortal;

    public int EEnergy;
    public int EMax;

    public GameObject Dash;


    public int atk;
    public int def;
    public int MoveSpeedRate;
    //±©»÷
    private int _CritRate;
    public int CritRate
    {
        get
        {
            return _CritRate;
        }
    }
    public int CritDamage;
    //ÀäÈ´±¶ÂÊ
    public int QCoolDownRate=100;
    public int ECoolDownRate=100;
    //³äÄÜ
    public int RechargeRate=100;
    private int _currentEnergy;
    public int currentEnergy
    {
        get
        {
            return _currentEnergy;
        }
    }
    public int MaxEnergy;

    public void changeEnergy(int i)
    {
        _currentEnergy += i;
        if (_currentEnergy < 0)
        {
            _currentEnergy = 0;
        }else if(currentEnergy>MaxEnergy)
        {
            _currentEnergy = MaxEnergy;
        }
    }

    public void setEnergy(int i)
    {
        _currentEnergy = i;
        if (_currentEnergy < 0)
        {
            _currentEnergy = 0;
        }
        else if (currentEnergy > MaxEnergy)
        {
            _currentEnergy = MaxEnergy;
        }
    }

    public void ChangeCritRate(int i)
    {
        _CritRate += i;
        if (_CritRate>100)
        {
            _CritRate = 100;

        }else if (_CritRate<0)
        {
            _CritRate = 0;
        }
    }

}
