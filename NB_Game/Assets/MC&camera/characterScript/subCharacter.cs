using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class subCharacter : TwoDbehavior
{
    public int attackNum;
    ArrayList damage;
    ArrayList interval;
    int QDamage;
    int EDamage=20;
    int QInterval=2;
    int EInterval=3;
    public int QEnergy=100;
    float DashInterval;

    //change
    bool canNormal=true;
    public float NormalPassedT;
    bool canE=true;
    public float EPassedT;
    bool canQ=true;
    public float QPassedT;
    public bool canDash =true;
    public float DashPassedT;
    bool canWalk=true;
    public int currentEne;
    GameObject Icon = null;


    //movement
    int xDirection;
    int zDirection;
    bool right;

    public float velocity = 5f;

    
    public TeamObj team;  
    public GameObject DashDetect;
    public EAttkDetect Dscript;

    public GameObject chasedEnemy;
    public GameObject icon;





    private bool firstDone;
    private bool secondDone;

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
        EAttkDetect.successfulDash += DashSuccessful;
        DashInterval = 0.1f;
        
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
        Gizmos.matrix = Matrix4x4.TRS(new Vector3(transform.position.x + i * 0.7f, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 0f), new Vector3(1.1f, 0.5f, 1.1f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
    private void Update()
    {
        velocity = 5f;
       //walk direction
        if (canWalk)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                xDirection = -1;
                right = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                xDirection = 1;
                right = true;
            }
            else
            {
                xDirection = 0;
                
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                zDirection = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                zDirection = -1;
            }
            else
            {
                zDirection = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                zDirection = 1;
            }
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
            else if (NormalPassedT >= (float)interval[attackNum-1]&& NormalPassedT <= (float)interval[attackNum - 1] + 1f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NormalPassedT = 0;
                    canNormal = false;
                    normalAttack(false);
                }
            }


        }


        if (!canQ)
        {
            QPassedT += Time.unscaledDeltaTime;
            if (QPassedT >0.1&& !firstDone)
            {

                
                chasedEnemy = Q().gameObject;
                firstDone = true;
                if (chasedEnemy!=null)
                {
                    icon.SetActive(true);
                    Icon = Instantiate(icon,chasedEnemy.transform);
                    Icon.layer = 11;
                }


                Debug.Log("Q first "+ secondDone);
            }
            if (QPassedT >= 2&& !secondDone)
            {
                secondDone = true;
                if (chasedEnemy != null)
                { 
                    GameObject.Destroy(Icon);
                    chasedEnemy.GetComponent<EnemyHealth>().takeDamage(50,40);
                } 
            }


            if (QPassedT>=QInterval)
            {
                QPassedT = 0;
                canQ = true;
            }
        }
        if (!canE)
        {
           
            EPassedT += Time.unscaledDeltaTime;
            if (EPassedT >= EInterval)
            {
                EPassedT = 0;
                canE = true;
            }
        }
        if (!canDash)
        {
            DashPassedT += Time.unscaledDeltaTime;

            if (DashPassedT < 0.05f)
            {
                canWalk = false;
                velocity = -20f;
                if (right)
                {
                    xDirection = 1;
                }
                else
                {
                    xDirection = -1;
                }
            }
            if (DashPassedT > DashInterval)
            {
                Dscript.destroy();
                DashPassedT = 0;
                canDash = true;
                canWalk = true;
            }
        }

        //attack input
        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {

            Dscript= Instantiate(DashDetect,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)).GetComponent<EAttkDetect>();
            canDash = false;
        }
        if (Input.GetKey(KeyCode.E)&&canE)
        {
            canE = false;            
            E();

        }
        if (Input.GetKey(KeyCode.Q)&&canQ&&currentEne>=QEnergy)
        {
            canQ = false;
            currentEne = 0;
             firstDone = false;
        secondDone = false;
        }
        if (Input.GetMouseButtonDown(0)&& canNormal)
        {
            canNormal = false;
            normalAttack(true);
        }




        Vector2 V =new Vector2(xDirection,zDirection).normalized*velocity;
        team.xVelocity = V.x;
        team.zVelocity = V.y;
        team.faceRight = right;
    }

   /* private void OnGUI()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xDirection = -1;
            right = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            xDirection = 1;
            right = true;
        }
        else
        {
            xDirection = 0;

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            zDirection = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            zDirection = -1;
        }
        else
        {
            zDirection = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            zDirection = 1;
        }
    }*/


    void normalAttack(bool timeOut)
    {
        if (attackNum >= 5|| timeOut)
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
            Debug.Log(enemy);
            EnemyHealth script = enemy.GetComponent<EnemyHealth>();
            script.takeDamage(EDamage, 10);
        }
        if (hit)
        {
            currentEne += 10;
        }
        Debug.Log(attackNum);
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
        Collider[] hitedEnemy= Physics.OverlapBox(new Vector3(transform.position.x + i * 0.7f, transform.position.y, transform.position.z), new Vector3(1.1f, 0.5f, 1.1f), Quaternion.Euler(0f, 0f, 0f), 7);

        ArrayList enemylist=new ArrayList();
        bool hit=false;
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
            currentEne += 25;
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
        bool hit = false;
        foreach (Collider enemy in hitedEnemy)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemylist.Add(enemy);

            }
        }
        float distance=10000;
        int index = -1;
        int j=-1;
        foreach (Collider enemy in enemylist)
        {
            j += 1;
            if (Vector3.Distance(enemy.transform.position, transform.position)< distance)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                index = j;
            }
            hit = true;
        }

        Debug.Log("enemy List created ");
        if (hit)
        {
            currentEne += 25;
        }

        Debug.Log(index);
        if (index>-1)
        {
            return (Collider)enemylist[0];
        }
        else
        {
            return null;
        }
    }

    void DashSuccessful()
    {


    }





}
