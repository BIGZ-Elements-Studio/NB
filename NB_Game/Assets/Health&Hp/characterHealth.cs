using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterHealth : AnyCharaHp
{
    public CharacterInfo thisCharacter;
    public int MaxPoise = 100;
    private void Start()
    {
        thisCharacter.currentHp = thisCharacter.maxHp;
    }
    private void Update()
    {
        increasePoise((int)Time.unscaledDeltaTime * 15);
    }

    private void increasePoise(int amount){
        thisCharacter.poiseHealth += amount;
        if (thisCharacter.poiseHealth > MaxPoise)
        {
            thisCharacter.poiseHealth = MaxPoise;
        }
    }
    public void takeDamage(int damage, int hardness)
    {

            thisCharacter.poiseHealth -= hardness;
            if (thisCharacter.poiseHealth < 0)
            {
                thisCharacter.poiseHealth = 0;
            }
            thisCharacter.currentHp -= damage;
            if (thisCharacter.currentHp < 0)
            {
                die();
                thisCharacter.currentHp = 0;
            }
        }
    

    public void recover(int recover)
    {
        thisCharacter.currentHp += recover;
        if (thisCharacter.currentHp > thisCharacter.maxHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp;
        }
    }

    public void totalrecover()
    {
            thisCharacter.currentHp = thisCharacter.maxHp;
    }

    public void recoverByPercent(int percent)
    {
        thisCharacter.currentHp += thisCharacter.maxHp*percent/100;
        if (thisCharacter.currentHp > thisCharacter.maxHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp;
        }
    }

    public void recoverToPercent(int percent)
    {
        if (thisCharacter.maxHp * percent / 100> thisCharacter.currentHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp * percent / 100;
        }
    }

    void die()
    {

    }


}
