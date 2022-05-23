using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class subCharaCopy : TwoDbehavior
{
    public CharacterInfo thisCharacter;
    protected int attackNum;
    protected ArrayList damageInfo;
    protected int EDamage = 20;
    protected int QInterval = 2;
    protected int EInterval = 5;
    public int QEnergy = 100;
    protected float DashInterval;
    protected float DashCooldown = 16f;

    //change
    protected bool canNormal;
    protected bool canE;
    public float EPassedT;


    protected bool canQ;
    public float QPassedT;


    [SerializeField] protected bool canDash;
    [SerializeField] protected bool CanDashEffect;
    [SerializeField] protected bool CanSmallDashEff;

    protected bool canWalk;


    //movement
    protected float xDirection;
    protected float zDirection;
    protected bool right;

    public float velocity = 5f;


    public TeamObj team;
    [SerializeField] protected GameObject DashDetect;
    protected EAttkDetect Dscript;

    [SerializeField] protected CameraShaker CameraShaker;
    protected range normalAtkRange;
    protected WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

    // normal attack based on the damageInfo, arraylist of damage and interval
    //QProcess(),EProcess(),Dashed(),DashProcess() need modification
    // need to specify damageInfo,QEnergy(Energy require for Q) in awake
    protected void Awake()
    {
        thisCharacter.MaxEnergy = 125;
        canNormal = true;
        canE = true;
        canQ = true;
        EPassedT = 0;
        QPassedT = 0;
        canDash = true;
        CanDashEffect = true;
        CanSmallDashEff = true;
        canWalk = true;

        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        EAttkDetect.successfulDash += Dashed;
        DashInterval = 0.3f;
        CameraShaker = cam.GetComponent<CameraShaker>();
    }

    private void OnEnable()
    {
       
        canNormal = true;
        UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);
        if (!canE)
        {
            UiManager.EShowCD(EInterval, EPassedT);
           // UiE.ShowCD(EInterval, EPassedT);
        }
        else
        {
            UiManager.Ereset();
            //UiE.reset();
        }
    }
    private void Update()
    {
        //controll direction and speed;
        if (canWalk)
        {
            Vector2 direction = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2).normalized;
            if (Input.GetKey(KeyCode.W))
            {
                velocity = 5f;
            }
            else
            {
                velocity = 0;
            }

            if (direction.x < 0)
            {
                right = false;
            }
            else
            {
                right = true;
            }
            xDirection = direction.x;
            zDirection = direction.y;


        }


        //ต๗สิ
        if (Input.GetKeyDown(KeyCode.L))
        {
            Dashed();
        }

        //attack input
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            DashProcess();
        }
        if (Input.GetKey(KeyCode.E) && canE)
        {
            EProcess();
        }
        if (Input.GetKey(KeyCode.Q) && canQ && thisCharacter.currentEnergy >= QEnergy)
        {
            QProcess();
        }
        if (Input.GetMouseButtonDown(0) && canNormal)
        {
            StartCoroutine(normalAttackProcess());
        }
        Vector2 V = new Vector2(xDirection, zDirection).normalized * velocity;
        team.xVelocity = V.x;
        team.zVelocity = V.y;
        team.faceRight = right;
    }

    #region canBe overwrite
    //for cheaking combo and direct to attk 
    protected virtual IEnumerator normalAttackProcess()
    {
        canNormal = false;
        bool goOn = true;
        attackNum = 1;
        int i;
        
        normalAttack();
        while (goOn)
        {
            yield return new WaitForSecondsRealtime((float)((NormalAtkInfo)damageInfo[attackNum-1]).interval);
            goOn = false;
            float j = 0;
            while (j <= 0.5)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    
                    normalAttack();
                    goOn = true;
                    break;
                }
                j += Time.unscaledDeltaTime;
                yield return null;

            }
        }
        canNormal = true;
        yield break;
    }

   
    //for attk according to attackNum 
    protected virtual void normalAttack()
    {
        int i;
        if (attackNum >= damageInfo.Count)
        {
            attackNum = 1;
        }
        else
        {
            attackNum += 1;
        }
        if (right)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        Collider[] hitedEnemy = Physics.OverlapBox(new Vector3(transform.position.x + i * 0.7f, transform.position.y, transform.position.z), new Vector3(0.5f, 0.5f, 0.2f), Quaternion.Euler(0f, 0f, 0f), 7);
        ArrayList enemylist = new ArrayList();
        bool hit = false;
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemylist.Add(enemy);

            }
        }
        foreach (Collider enemy in enemylist)
        {
            hit = true;
            EnemyHealth script = enemy.GetComponent<EnemyHealth>();
            hitTarget(6f, enemy.gameObject);

            script.takeDamage((int)damage(((NormalAtkInfo)damageInfo[attackNum - 1]).damage), 10);
        }
        if (hit)
        {
            CameraShaker.shake();
            changeEnergy(10);
        }
    }

    protected virtual async Task QProcess()
    {
        Debug.Log(1);
        
    }

    protected virtual async Task EProcess()
    {
        
    }

    protected virtual void Dashed()
    {
        
    }

    protected virtual async Task DashProcess()
    {
        
    }

    #endregion

    protected async Task ESetTime()
    {
        EPassedT = 0;
        while (EPassedT < EInterval)
        {
            EPassedT += 0.1f;
            await Task.Delay(100);
        }
        EPassedT = 0;
    }
    protected void hitTarget(float harness, GameObject target)
    {
        if (target.GetComponent<Rigidbody>() != null)
        {
            target.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.transform.position - transform.position) * 1000f * harness, ForceMode.Force);
        }
    }

    protected float damage(int damagePercent)
    {
        return damagePercent;
    }

    protected void changeEnergy(int i)
    {
        thisCharacter.changeEnergy(i);
        UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);
    }

    protected void setEnergy(int i)
    {
        thisCharacter.setEnergy(i);
    }

}

public class NormalAtkInfo{
    public int damage;
    public float interval;
    public NormalAtkInfo(int d, float i)
    {
        damage = d;
        interval = i;
    }
}

public class range{
    Vector3 center;
    Vector3 halfExtent;
    Quaternion roation;
    int layerMask;
    public range(Vector3 c, Vector3 h, Quaternion r)
    {
        center = c;
        halfExtent = h;
        roation = r;
        layerMask = 7;
    }

    public range(Vector3 c, Vector3 h)
    {
        center = c;
        halfExtent = h;
        roation = Quaternion.Euler(0,0,0) ;
        layerMask = 7;
    }

}
