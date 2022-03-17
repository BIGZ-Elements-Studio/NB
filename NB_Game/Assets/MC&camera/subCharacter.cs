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
    int QInterval;
    int EInterval;
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

    //movement
    int xDirection;
    int zDirection;
    bool right;

    public float velocity = 5f;

    //
    public TeamObj team;


     void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
        DashInterval = 0.015f;
        
    }
    private void Update()
    {

    velocity = 5f;
       /* if (Input.GetKey(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }*/
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

            if (DashPassedT < 0.06f)
            {
                canWalk = false;
                velocity = 20f;
                if (right)
                {
                    xDirection = 1;
                }
                Debug.Log("dashing");
            }
            if (DashPassedT > DashInterval)
            {
                DashPassedT = 0;
                canDash = true;
                canWalk = true;
                Debug.Log("dash finised");
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {
            canDash = false;
            Debug.Log("Inputted");
        }
        if (Input.GetKey(KeyCode.E)&&canE)
        {
            canE = false;
        }
        if (Input.GetKey(KeyCode.Q)&&canQ)
        {
            canQ = false;
        }




        Vector2 V =new Vector2(xDirection,zDirection).normalized*velocity;
        team.xVelocity = V.x;
        team.zVelocity = V.y;
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
    void E()
    {

    }
    void Q()
    {

    }





}
