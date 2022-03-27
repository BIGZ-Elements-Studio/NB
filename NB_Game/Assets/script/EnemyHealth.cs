using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHp;
    int maxHp=10000;
    int CurrentPoise = 100;
    int Maxpoise = 100;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void takeDamage(int damage, int hardness)
    {
        CurrentPoise -= hardness;
        if (CurrentPoise<0)
        {
            CurrentPoise = 0; 
        }
        currentHp -= damage;
        if (currentHp <= 0)
        {
            die();
            currentHp = 0;
        }
    }

    public void recover(int recover)
    {
        currentHp += recover;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void totalrecover()
    {
        currentHp = maxHp;
    }

    public void recoverByPercent(int percent)
    {
        currentHp += maxHp * percent / 100;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void recoverToPercent(int percent)
    {
        if (maxHp * percent / 100 > currentHp)
        {
            currentHp = maxHp * percent / 100;
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
