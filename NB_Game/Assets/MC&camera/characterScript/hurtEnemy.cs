using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class hurtEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        hurtAfterTime();
    }

    async Task hurtAfterTime()
    {
        await Task.Delay(1900);
        EnemyHealth Target = transform.parent.gameObject.GetComponent<EnemyHealth>();
        if (Target != null)
        {
            Target.takeDamage(50, 40);
        }
        Destroy(gameObject);
    }
}
