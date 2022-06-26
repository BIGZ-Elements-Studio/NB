using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class characterHealth : AnyCharaHp
{
    public UnityEvent<int> Hited;
    
    public CharacterInfo thisCharacter;
    public int MaxPoise = 100;
    public bool died;

    SetUi UiManager;

    private void Start()
    {
        UiManager = GetComponentInParent<SetUi>();
        died = false;
        thisCharacter.currentHp = thisCharacter.maxHp;
        setHp();

    }
    private void Update()
    {
        if (!thisCharacter. die) {
            increasePoise((int)Time.unscaledDeltaTime * 15);
        }
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
            if (thisCharacter.poiseHealth <= 0)
            {
                thisCharacter.poiseHealth = 0;
            }
            thisCharacter.currentHp -= damage;
            if (thisCharacter.currentHp <= 0)
            {
                die();
                thisCharacter.currentHp = 0;
            }
        if (Hited == null)
        {
            Hited.Invoke(thisCharacter.poiseHealth);
        }
        setHp();

       
    }
    

    public override void recover(int recover)
    {
        thisCharacter.currentHp += recover;
        if (thisCharacter.currentHp > thisCharacter.maxHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp;
        }
       
        setHp();
    }

    public override void totalrecover()
    {
            thisCharacter.currentHp = thisCharacter.maxHp;
       
        setHp();
    }

    public override void recoverByPercent(int percent)
    {
        thisCharacter.currentHp += thisCharacter.maxHp*percent/100;
        if (thisCharacter.currentHp > thisCharacter.maxHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp;
        }
        
        setHp(); 
    }

    public override void recoverToPercent(int percent)
    {
        if (thisCharacter.maxHp * percent / 100> thisCharacter.currentHp)
        {
            thisCharacter.currentHp = thisCharacter.maxHp * percent / 100;
        }
        
        setHp();
    }

    public override void die()
    {
        died = true;
    }

    void setHp()
    {
        if (gameObject.activeInHierarchy==true)
        {
            UiManager.showHP(thisCharacter.currentHp,thisCharacter.maxHp);
        }
        
    }

    
}
