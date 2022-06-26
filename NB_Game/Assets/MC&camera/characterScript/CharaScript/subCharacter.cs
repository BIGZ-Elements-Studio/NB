using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class subCharacter : TwoDbehavior
{
    public CharacterInfo thisCharacter;
    private int attackNum;
    ArrayList damage;
    ArrayList interval;
    int EDamage = 20;
    int QInterval = 2;
    int EInterval = 5;
    public int QEnergy = 100;
    float DashInterval;
    float DashCooldown=16f;

    //change
    bool canNormal = true;
    bool canE = true;
    public float EPassedT;


    bool canQ = true;
    public float QPassedT;


    [SerializeField] private bool canDash = true;
    [SerializeField] private bool CanDashEffect;
    [SerializeField] private bool CanSmallDashEff;

    bool canWalk = true;

    UiManager UiManager;
    //movement
    float xDirection;
    float zDirection;
    bool right;

    public float velocity = 5f;


    public TeamObj team;
    [SerializeField] private GameObject DashDetect;
    private EAttkDetect Dscript;

    public GameObject chasedEnemy;
    public GameObject icon;

    [SerializeField]private CameraShaker CameraShaker;

    [SerializeField] private GameObject UiE;



    private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);
    void Start()
    {
        UiE = GameObject.Find("E");
        interval = new ArrayList();
        interval.Add(0.1f);
        interval.Add(0.1f);
        interval.Add(0.15f);
        interval.Add(0.16f);
        interval.Add(0.3f);


        damage = new ArrayList();
        damage.Add(15);
        damage.Add(10);
        damage.Add(20);
        damage.Add(25);
        damage.Add(41);

        thisCharacter.MaxEnergy = 125;
        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        EAttkDetect.successfulDash += Dashed;
        DashInterval = 0.3f;
        CanDashEffect = true;

        CameraShaker= cam.GetComponent<CameraShaker>();
    }
    void Awake()
    {
        UiE = GameObject.Find("E");
        thisCharacter.MaxEnergy = 125;
    }

    void OnDrawGizmos()
    {
        int i = 0;
        if (right)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        Gizmos.matrix = Matrix4x4.TRS(new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 0f), new Vector3(25f, 5f, 15f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

    }

    private void OnEnable()
    {
        UiManager?.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);

        if (!canE)
        {
            UiE.GetComponent<showE>().ShowCD(EInterval, EPassedT);
        }
        else
        {
            UiE.GetComponent<showE>().reset();
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
            StartCoroutine(normalAttackProcess(true));
        }
        Vector2 V = new Vector2(xDirection, zDirection).normalized * velocity;
        team.xVelocity = V.x;
        team.zVelocity = V.y;
        team.faceRight = right;
    }

   
    //for cheaking combo and direct to attk 
    IEnumerator normalAttackProcess(bool first)
    {
        canNormal = false;
        bool goOn=true; 
        attackNum = 1;
        normalAttack(true);
        while (goOn)
        {
            yield return new WaitForSecondsRealtime((float)interval[attackNum - 1]);
            goOn = false;
            for (float j = 0; j <= 0.5 * 30; j++)
            {

                if (Input.GetMouseButton(0))
                {
                    
                    if (attackNum >= 5)
                    {
                        attackNum = 1;
                    }
                    else
                    {
                        attackNum += 1;
                    }
                    normalAttack(false);
                    goOn = true;
                    break;
                }
                yield return new WaitForSecondsRealtime(1f / 30f);
            }
        }
        canNormal = true;
        yield break;
    }

    //for attk according to attackNum 
    void normalAttack(bool timeOut)
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
            
            script.takeDamage((int)damage[attackNum-1], 10);
        }
        if (hit)
        {
            CameraShaker.shake();
            thisCharacter.changeEnergy(10);
            UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);
        }
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
            thisCharacter.changeEnergy(25);
            UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);
        }
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


    

    void findEnemy()
    {
        Collider[] hitedEnemy = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(25f, 5f, 15f), Quaternion.Euler(0f, 0f, 0f), 7);

        ArrayList enemylist = new ArrayList();
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemylist.Add(enemy);
            }
        }

        if (enemylist[0]!=null)
        {
            Collider obj = (Collider)enemylist[0];
            chasedEnemy = obj.gameObject;
        }
        

    }

    async Task QProcess()
    {
        canQ = false;
        thisCharacter.changeEnergy(-100);
        UiManager.showEnergy(thisCharacter.currentEnergy, thisCharacter.MaxEnergy,QEnergy);
        int i = 30;
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

    private async Task EProcess()
    {
        UiE.GetComponent<showE>().ShowCD(EInterval,0);
        E();
        canE = false;
        ESetTime();
        await Task.Delay(EInterval*1000);
        canE = true;
    }


    private async Task ESetTime()
    {
        EPassedT = 0;
        while (EPassedT < EInterval)
        {
            EPassedT += 0.1f;
            await Task.Delay(100);
        }
        EPassedT = 0;
    }

    void hitTarget(float harness, GameObject target)
    {
        if (target.GetComponent<Rigidbody>() != null)
        {
            target.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.transform.position - transform.position) * 1000f*harness, ForceMode.Force);
        }
    }


    public async Task DashProcess()
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
        await Task.Delay((int)(DashInterval*1000 - 70));
        canWalk = true;
        canDash = true;
    }

    void Dashed()
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
        await Task.Delay((int)((DashCooldown - 8)*1000));
        //yield return new WaitForSecondsRealtime(DashCooldown - 8f);
        Time.fixedDeltaTime = 0.02f;
        CanDashEffect = true;
    }

    /*IEnumerator EProcess()
    {
        E();
        canE = false;
        StartCoroutine(ESetTime());
        yield return new WaitForSecondsRealtime(EInterval);
        canE = true;
        yield break;
    }*/

    /*IEnumerator ESetTime()
    {
        EPassedT = 0;
        while (EPassedT < EInterval)
        {
            EPassedT += 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);

        }
        EPassedT = 0;
        yield break;

    }*/

    /*IEnumerator DashSuccessful()
    {
        Debug.Log("2");
        for (float i = 1f; i <= 5f; i++)
        {
            Time.timeScale = 1 - i / 10;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (float i = 1f; i <= 5f; i++)
        {
            Time.timeScale = 0.5f + i / 10;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 1f;
        yield break;
    }*/

    /* IEnumerator DashSuccessfulEffect()
     {
         CanSmallDashEff = false;
         CanDashEffect = false;
         Time.timeScale = 0.2f;
         Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale / 2;
         yield return new WaitForSecondsRealtime(8f);
         CanSmallDashEff = true;
         Time.timeScale = 1f;
         Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale / 2;
         yield return new WaitForSecondsRealtime(DashCooldown - 8f);
         Time.fixedDeltaTime = 0.02f;
         CanDashEffect = true;
         yield break;
     }*/

    /*IEnumerator DashProcess()
   {
       Dscript = Instantiate(DashDetect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 0f)).GetComponent<EAttkDetect>();
       canDash = false;
       canWalk = false;
       if(right)
               {
           xDirection = 1;
       }else
       {
           xDirection = -1;
       }

       velocity = -15f;

       yield return new WaitForSecondsRealtime(0.07f);
       velocity = 3f;
       Dscript.destroy();
       yield return new WaitForSecondsRealtime(DashInterval- 0.07f);
       canWalk = true;
       canDash = true;
       yield break;
   }*/
}
