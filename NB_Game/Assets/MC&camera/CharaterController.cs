using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{
    public float velocity = 0f;
    public Rigidbody rb;
    public Vector2 direction;
    public TeamObj team;

    //team member
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Four;

    public COnTwoD twoDS;
    //switch
    public delegate void simpleD();
    public static simpleD SwitchD;

    public int current=1;
    public BoolObj threeD;

    void Start()
    {
        twoDS = GetComponent<COnTwoD>();
        Load();
        twoDS.enabled = !threeD.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwithchState();
            if (SwitchD != null)
            {
                SwitchD();
                Debug.Log("changed");
            }
            Debug.Log("Inputed");
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current = 1;
            chage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current = 2;
            chage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current = 3;
            chage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            current = 4;
            chage();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction.Normalize();
        rb.velocity = new Vector3(direction.x,rb.velocity.y,direction.y) ;
    }

    void Load()
    {
        Instantiate(team.One.Character, this.transform);
        One = GameObject.Find(team.One.CharacterName);
        Instantiate(team.Two.Character, this.transform);
        Two = GameObject.Find(team.Two.CharacterName);
        Instantiate(team.Three.Character, this.transform);
        Three = GameObject.Find(team.Three.CharacterName);
        Instantiate(team.Four.Character, this.transform);
        Four = GameObject.Find(team.Four.CharacterName);

        chage();
    }

    void chage()
    {
        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);
        Four.SetActive(false);

        if (current == 1)
        {
            One.SetActive(true);
        }
        else if (current == 2)
        {
            Two.SetActive(true);
        }
        else if (current == 3)
        {
            Three.SetActive(true);
        }
        else
        {
            Four.SetActive(true);
        }
    }

    void SwithchState()
    {
        threeD.value = !threeD.value;
        twoDS.enabled = !threeD.value;
    }
    


    }
