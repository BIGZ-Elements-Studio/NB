using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TwoCharacter : subCharaCopy
{
    public GameObject chasedEnemy;
    public GameObject icon;
    protected void Awake()
    {
        base.Awake();
        CharacterName = "blue";
        QEnergy = 50;
        damageInfo = new ArrayList();
        damageInfo.Add(new NormalAtkInfo(13, 0.1f));
        damageInfo.Add(new NormalAtkInfo(15, 0.1f));
        damageInfo.Add(new NormalAtkInfo(30, 0.1f));
        damageInfo.Add(new NormalAtkInfo(10, 0.1f));
    }
    #region Q
    protected override async Task QProcess()
    {
        canQ = false;
        changeEnergy(QEnergy*-1);

        // thisCharacter.currentEnergy = 0;
        
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
        bool hit=false;
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemy.GetComponent<EnemyHealth>().takeDamage(150,15);
                hit = true;
                hitTarget(15f, enemy.gameObject);
            }
        }
        if (hit)
        {
            CameraShaker.BigShake();

        }

        await Task.Delay(100);
        canQ = true;
    }

    
    #endregion


    #region E
    protected override async Task EProcess()
    {

        UiManager.EShowCD(EInterval, 0);
       
        E();
        canE = false;
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
                hit = true;
                enemylist.Add(enemy);
                EnemyHealth script = enemy.GetComponent<EnemyHealth>();
                script.takeDamage(EDamage, 10);
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
            changeEnergy(15);

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

    #region
    protected override IEnumerator normalAttackProcess()
    {
        canNormal = false;
        bool goOn = true;
        attackNum = 1;
        int i;
       
        normalAttack();
        while (goOn)
        {
            yield return new WaitForSecondsRealtime((float)((NormalAtkInfo)damageInfo[attackNum - 1]).interval);
            goOn = false;
            float j = 0;
            while (j <= 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (attackNum==3) {
                        float PressTime = 0;
                        bool LongPress = true;
                        while (PressTime <= 0.2)
                        {
                            if (Input.GetMouseButtonUp(0))
                            {
                                
                                normalAttack();
                                LongPress = false;

                            }
                            PressTime += Time.unscaledDeltaTime;
                            yield return null;
                        }
                        if (LongPress)
                        {
                            Debug.Log("LongPress");

                        }
                    }
                    else
                    {
                        
                        normalAttack();
                    }
                }
                

                j += Time.unscaledDeltaTime;
                yield return null;
            }
               
        }
        canNormal = true;
        yield break;
    }
    #endregion


}
