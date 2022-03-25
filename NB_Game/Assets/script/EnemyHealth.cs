using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int currentHp;
    int maxHp;
    private void Start()
    {
        currentHp = maxHp;
    }

    public void takeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
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

    }
}
