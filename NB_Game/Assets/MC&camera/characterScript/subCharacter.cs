using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subCharacter : TwoDbehavior
{
    public int attackNum;
    ArrayList damage;
    ArrayList interval;
    int QDamage;
    int EDamage;
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


    
    //movement
    int xDirection;
    int zDirection;
    bool right;

    public float velocity = 5f;

    //
    public TeamObj team;


     void Start()
    {
        interval = new ArrayList();
        interval.Add(0.1f);
        interval.Add(0.1f);
        interval.Add(0.15f);
        interval.Add(0.16f);
        interval.Add(0.3f);
        Debug.Log("hihi");
        Debug.Log(GameObject.Find("MainCamera"));

        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        
        DashInterval = 0.1f;
        
    }
    private void Update()
    {
        velocity = 5f;
       
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




        if (!canNormal)
        {
            NormalPassedT += Time.unscaledDeltaTime;
            if (NormalPassedT >= (float)interval[attackNum])
            {
                NormalPassedT = 0;
                canNormal = true;
            }
        }
        if (!canQ)
        {
            QPassedT += Time.unscaledDeltaTime;
            if (QPassedT>=QInterval)
            {
                QPassedT = 0;
                canQ = true;
            }
        }
        if (!canE)
        {
            if (EPassedT < 0.5f)
            {
               // E();
            }
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
                velocity = 20f;
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
                DashPassedT = 0;
                canDash = true;
                canWalk = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {
            canDash = false;
        }
        if (Input.GetKey(KeyCode.E)&&canE)
        {
            canE = false;
            currentEne += 25;

            E();

        }
        if (Input.GetKey(KeyCode.Q)&&canQ&&currentEne>=QEnergy)
        {
            canQ = false;
            currentEne = 0;
            
        }
        if (Input.GetMouseButton(0)&& canNormal)
        {
            canNormal = false;
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


    void normalAttackCombo(bool timeOut)
    {
        if (attackNum>5|| timeOut)
        {
            attackNum = 0;
        }
        else
        {
            attackNum += 1;
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
            i = 0;
        }
        Collider[] hitedEnemy= Physics.OverlapBox(new Vector3(transform.position.x+i*1.7f,transform.position.y,transform.position.z),new Vector3(1.1f,0.5f,1.1f),Quaternion.Euler(0f,0f,0f) ,7);

        foreach(Collider enemy in hitedEnemy)
        {

            if (enemy.GetComponent<EnemyHealth>() == null)
            {
                Debug.Log(enemy);
            }
        }

    }
    void Q()
    {

    }





}
