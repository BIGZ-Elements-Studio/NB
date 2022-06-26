using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class subCharaCopy : TwoDbehavior
{
   
    private characterHealth characterHealth;
    public CharacterInfo thisCharacter;

    //normal atk
    protected int attackNum;
    protected ArrayList damageInfo;
    //e
    protected int EDamage = 20;
    protected int EInterval = 5;
    //q
    protected int QInterval = 2;
    protected int QEnergy = 100;
    //dash
    protected float DashInterval;
    protected float DashCooldown = 16f;
    [SerializeField] protected bool canDash;
    [SerializeField] protected bool CanDashEffect;
    [SerializeField] protected bool CanSmallDashEff;
    //change
    protected bool canNormal;
    protected bool canE;
    public float EPassedT;
    protected bool canQ;
    public float QPassedT;
    protected bool canWalk;

    public string CharacterName;
   


    //movement
    protected float xDirection;
    protected float zDirection;
    protected bool right;

    public float velocity = 5f;


    public TeamObj team;
    [SerializeField] protected GameObject DashDetect;
    protected EAttkDetect Dscript;

    [SerializeField] protected CameraShaker CameraShaker;
    protected WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

    protected SetUi UiManager;

    // normal attack based on the damageInfo, arraylist of damage and interval
    //QProcess(),EProcess(),Dashed(),DashProcess() need modification
    // need to specify damageInfo,QEnergy(Energy require for Q) in awake
    protected void Awake()
    {
        characterHealth = GetComponent<characterHealth>();
        characterHealth.Hited.AddListener(BeenHited);
        thisCharacter.maxHp = 1;
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
        UiManager = GetComponentInParent <SetUi>();
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
        bool hit = false;
        
        foreach (Collider enemy in hitedEnemy)
        {
            EnemyHealth script = enemy.gameObject.GetComponent<EnemyHealth>();
            if (script!=null) {
                hit = true;
                hitTarget(6f, enemy.gameObject);

                script.takeDamage((int)damage(((NormalAtkInfo)damageInfo[attackNum - 1]).damage), 10);
            }
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

    protected virtual void BeenHited(int remainingPoise)
    {
       
    }

    #endregion

    
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

        UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy, QEnergy);
        
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


