using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OneController : subCharaCopy
{
    public GameObject chasedEnemy;
    public GameObject icon;
    protected  void Awake()
    {
        base.Awake();
        QEnergy = 100;
        damageInfo = new ArrayList();
        damageInfo.Add(new NormalAtkInfo(15,0.1f));
        damageInfo.Add(new NormalAtkInfo(20, 0.1f));
        damageInfo.Add(new NormalAtkInfo(18, 0.1f));
        damageInfo.Add(new NormalAtkInfo(50, 0.1f));
    }
    #region Q
    protected override async Task QProcess()
    {
        canQ = false;
        changeEnergy(-100);
        
       // thisCharacter.currentEnergy = 0;
        GameObject ChasedEnemy = null;
        GameObject Icon = null;

        //yield return new WaitForSecondsRealtime(0.1f);
        await Task.Delay(100);
        object obj = Q();
        if (obj != null)
        {
            Collider coll = (Collider)obj;
            ChasedEnemy = coll.gameObject;
        }

        if (ChasedEnemy != null)
        {
            icon.SetActive(true);
            Icon = Instantiate(icon, ChasedEnemy.transform);
            Icon.layer = 11;
        }
        await Task.Delay(1900);
        canQ = true;
        
    }

    Collider Q()
    {
        int i;
        if (right)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        Collider[] hitedEnemy = Physics.OverlapBox(new Vector3(transform.position.x + i * 0.7f, transform.position.y, transform.position.z), new Vector3(1.1f, 0.5f, 1.1f), Quaternion.Euler(0f, 0f, 0f), 7);

        ArrayList enemylist = new ArrayList();
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemylist.Add(enemy);

            }
        }
        float distance = 10000;
        int index = -1;
        int j = -1;
        foreach (Collider enemy in enemylist)
        {
            j += 1;
            if (Vector3.Distance(enemy.transform.position, transform.position) < distance)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                index = j;
            }
        }

        if (index > -1)
        {
            CameraShaker.shake();
            return (Collider)enemylist[0];
        }
        else
        {
            return null;
        }
    }
    #endregion


    #region E
    protected override async Task EProcess()
    {
        //UiE.GetComponent<showE>().ShowCD(EInterval, 0);
        UiManager.EShowCD(EInterval, 0);
        E();
        canE = false;
        ESetTime();
        await Task.Delay(EInterval * 1000);
        canE = true;
    }
    void E()
    {
        int i;
        if (right)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        Collider[] hitedEnemy = Physics.OverlapBox(new Vector3(transform.position.x + i * 0.7f, transform.position.y, transform.position.z), new Vector3(1.1f, 0.5f, 1.1f), Quaternion.Euler(0f, 0f, 0f), 7);

        ArrayList enemylist = new ArrayList();
        bool hit = false;
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemylist.Add(enemy);
                hitTarget(10f, enemy.gameObject);
            }
        }
        foreach (Collider enemy in enemylist)
        {
            hit = true;
            EnemyHealth script = enemy.GetComponent<EnemyHealth>();
            script.takeDamage(EDamage, 10);

        }
        if (hit)
        {
            CameraShaker.BigShake();
            changeEnergy(25);
            
        }
    }
    #endregion

    #region Dash
    protected override async Task DashProcess()
    {
        Dscript = Instantiate(DashDetect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 0f)).GetComponent<EAttkDetect>();
        canDash = false;
        canWalk = false;
        if (right)
        {
            xDirection = 1;
        }
        else
        {
            xDirection = -1;
        }

        velocity = -15f;

        await Task.Delay(70);
        velocity = 3f;
        Dscript.destroy();
        await Task.Delay((int)(DashInterval * 1000 - 70));
        canWalk = true;
        canDash = true;
    }

    protected override void Dashed()
    {
        if (CanDashEffect)
        {
            DashSuccessfulEffect();
        }
        else if (CanSmallDashEff)
        {
            DashSuccessful();
        }
    }

    private async Task DashSuccessfulEffect()
    {
        CanSmallDashEff = false;
        CanDashEffect = false;
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale / 2;
        await Task.Delay(8 * 1000);
        //yield return new WaitForSecondsRealtime(8f);
        CanSmallDashEff = true;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        await Task.Delay((int)((DashCooldown - 8) * 1000));
        //yield return new WaitForSecondsRealtime(DashCooldown - 8f);
        Time.fixedDeltaTime = 0.02f;
        CanDashEffect = true;
    }
    private async Task DashSuccessful()
    {
        for (float i = 1f; i <= 5f; i++)
        {
            Time.timeScale = 1 - i / 10;
            //yield return new WaitForSecondsRealtime(0.1f);
            await Task.Delay(100);
        }
        for (float i = 1f; i <= 5f; i++)
        {
            Time.timeScale = 0.5f + i / 10;
            //yield return new WaitForSecondsRealtime(0.1f);
            await Task.Delay(100);
        }
        Time.timeScale = 1f;
    }
    #endregion


}
