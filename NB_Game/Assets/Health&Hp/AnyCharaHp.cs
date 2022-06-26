using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AnyCharaHp : MonoBehaviour
{
    public virtual void takeDamage(int damage) { }

    public virtual void recover(int recover) { }
    public virtual void totalrecover() { }


    public virtual void recoverByPercent(int percent) { }

    public virtual void recoverToPercent(int percent) { }


    public virtual void die() { }
    
}
