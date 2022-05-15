using System.Collections;
using UnityEngine;

public class subCharacter : TwoDbehavior
{
    public CharacterInfo thisCharacter;
    public int attackNum;
    ArrayList damage;
    ArrayList interval;
    int QDamage;
    int EDamage = 20;
    int QInterval = 2;
    int EInterval = 3;
    public int QEnergy = 100;
    float DashInterval;
    float DashCooldown=16f;

    //change
    bool canNormal = true;
    public float NormalPassedT;


    bool canE = true;
    public float EPassedT;


    bool canQ = true;
    public float QPassedT;


    public bool canDash = true;
    public float DashPassedT;
    public bool CanDashEffect;
    public bool CanSmallDashEff;

    bool canWalk = true;


    //movement
    float xDirection;
    float zDirection;
    bool right;

    public float velocity = 5f;


    public TeamObj team;
    public GameObject DashDetect;
    public EAttkDetect Dscript;

    public GameObject chasedEnemy;
    public GameObject icon;

    public CameraShaker CameraShaker;



    private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);
    void Start()
    {
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

        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        EAttkDetect.successfulDash += Dashed;
        DashInterval = 0.3f;
        CanDashEffect = true;

        CameraShaker= GameObject.Find("MainCamera").GetComponent<CameraShaker>();
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
        //attack
        if (!canNormal)
        {
            NormalPassedT += Time.unscaledDeltaTime;
            if (NormalPassedT >= (float)interval[attackNum - 1] + 0.5f)
            {
                NormalPassedT = 0;
                canNormal = true;
            }
            else if (NormalPassedT >= (float)interval[attackNum - 1] && NormalPassedT <= (float)interval[attackNum - 1] + 1f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NormalPassedT = 0;
                    canNormal = false;
                    normalAttack(false);
                }
            }


        }



        //ต๗สิ
        if (Input.GetKeyDown(KeyCode.L))
        {
            Dashed();
        }

        //attack input
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(DashProcess());
        }
        if (Input.GetKey(KeyCode.E) && canE)
        {
            StartCoroutine(EProcess());
        }
        if (Input.GetKey(KeyCode.Q) && canQ && thisCharacter.currentEnergy >= QEnergy)
        {
            StartCoroutine(QProcess());
        }
        if (Input.GetMouseButtonDown(0) && canNormal)
        {
            canNormal = false;
            normalAttack(true);
        }
        Vector2 V = new Vector2(xDirection, zDirection).normalized * velocity;
        team.xVelocity = V.x;
        team.zVelocity = V.y;
        team.faceRight = right;
    }


    void normalAttack(bool timeOut)
    {
        if (attackNum >= 5 || timeOut)
        {
            attackNum = 1;
        }
        else
        {
            attackNum += 1;
        }


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
            CameraShaker.shake();
            thisCharacter.currentEnergy += 10;
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

            }
        }
        foreach (Collider enemy in enemylist)
        {
            hit = true;
            Debug.Log(enemy);
            EnemyHealth script = enemy.GetComponent<EnemyHealth>();
            script.takeDamage(EDamage, 10);
        }
        if (hit)
        {
            CameraShaker.shake();
            thisCharacter.currentEnergy += 25;
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


    void Dashed()
    {
        if (CanDashEffect)
        {
            StartCoroutine(DashSuccessfulEffect());
        }
        else if(CanSmallDashEff)
        {
            StartCoroutine(DashSuccessful());
        }
    }

    IEnumerator DashSuccessful()
    {
        Debug.Log("2");
        for (float i =1f; i<=5f; i++)
        {
            Time.timeScale = 1-i/10;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (float i = 1f; i <= 5f; i++)
        {
            Time.timeScale = 0.5f + i / 10;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 1f;
        yield break;
    }



    IEnumerator DashSuccessfulEffect()
    {
        CanSmallDashEff = false;
        Debug.Log("1");
        CanDashEffect = false;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(8f);
        Debug.Log("1!");
        CanSmallDashEff = true;
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(DashCooldown-8f);
        Debug.Log("1!!");
        CanDashEffect = true;
        yield break;
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

    IEnumerator QProcess()
    {
        canQ = false;
        thisCharacter.currentEnergy = 0;
        int i = 30;
        Debug.Log("Started " + "1");
        GameObject ChasedEnemy = null;
        GameObject Icon = null;

        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log(2);
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


        yield return new WaitForSecondsRealtime(1.9f);
        if (ChasedEnemy != null)
        {
            GameObject.Destroy(Icon);
            ChasedEnemy.GetComponent<EnemyHealth>().takeDamage(50, 40);
        }
        yield return new WaitForSecondsRealtime(QInterval - 0.1f - 1.9f);
        canQ = true;

        Debug.Log(i);
        yield break;
    }

    IEnumerator EProcess()
    {
        E();
        canE = false;
        StartCoroutine(ESetTime());
        yield return new WaitForSecondsRealtime(EInterval);
        canE = true;
        yield break;
    }

    IEnumerator ESetTime()
    {
        EPassedT = 0;
        while (EPassedT < EInterval)
        {
            EPassedT += 0.1f;
            yield return wait;

        }
        EPassedT = 0;
        yield break;

    }

    IEnumerator DashProcess()
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
    }
}
