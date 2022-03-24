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
    public COnThreeD threeDS;
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
        threeDS.enabled = threeD.value;
        SwithchState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwithchState();
            if (SwitchD != null)
            {
                SwitchD();
            }
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
        One = Instantiate(team.One.Character, this.transform);
        Two = Instantiate(team.Two.Character, this.transform);
        Three = Instantiate(team.Three.Character, this.transform);
        Four = Instantiate(team.Four.Character, this.transform);
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
            team.current = team.One;
        }
        else if (current == 2)
        {
            Two.SetActive(true);
            team.current = team.Two;
        }
        else if (current == 3)
        {
            Three.SetActive(true);
            team.current = team.Three;
        }
        else
        {
            Four.SetActive(true);
            team.current = team.Four;
        }
    }

    void SwithchState()
    {
        threeD.value = !threeD.value;
        twoDS.enabled = !threeD.value;
        threeDS.enabled = threeD.value;
    }
    


}
