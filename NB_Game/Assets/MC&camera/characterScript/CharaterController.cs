using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour
{

    //public Dictionary<int, GameObject> chara;

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


    public int current;
    public BoolObj threeD;

    void Start()
    {
        current = 1;
        team.currentC = 1;
        threeD.value = true;
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
            change();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current = 2;
            change();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current = 3;
            change();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            current = 4;
            change();
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
        
        One = Instantiate(team.OneC, this.transform);
        //chara.Add(1, Instantiate(team.OneC, this.transform));
        Two = Instantiate(team.TwoC, this.transform);
        //chara.Add(2, Two);
        Three = Instantiate(team.ThreeC, this.transform);
        //chara.Add(3, Three);
        Four = Instantiate(team.FourC, this.transform);
        //chara.Add(4, Four);
        change();
    }

    void change()
    {
        /*One.GetComponent<CharaStateManager>().setActive(false);
        One.GetComponent<CharaStateManager>().setActive(false);
        Two.GetComponent<CharaStateManager>().setActive(false);
        Three.GetComponent<CharaStateManager>().setActive(false);
        Four.GetComponent<CharaStateManager>().setActive(false);

        if (current == 1)
        {
            One.GetComponent<CharaStateManager>().setActive(true);
            team.currentC = 1;
        }
        else if (current == 2)
        {
            Two.GetComponent<CharaStateManager>().setActive(true);
            team.currentC = 2;
        }
        else if (current == 3)
        {
            Three.GetComponent<CharaStateManager>().setActive(true);
            team.currentC = 3;
        }
        else
        {
            Four.GetComponent<CharaStateManager>().setActive(true);
            team.currentC = 4;
        }*/

        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);
        Four.SetActive(false);

        if (current == 1)
        {
            One.SetActive(true);
            team.currentC = 1;
        }
        else if (current == 2)
        {
            Two.SetActive(true);
            team.currentC = 2;
        }
        else if (current == 3)
        {
            Three.SetActive(true);
            team.currentC = 3;
        }
        else
        {
            Four.SetActive(true);
            team.currentC = 4;
        }
    }

    void SwithchState()
    {
        threeD.value = !threeD.value;
        twoDS.enabled = !threeD.value;
        threeDS.enabled = threeD.value;

        One.GetComponent<subCharacter>().enabled= threeD.value;
        Two.GetComponent<subCharacter>().enabled = threeD.value ;
        Three.GetComponent<subCharacter>().enabled = threeD.value ;
        Four.GetComponent<subCharacter>().enabled = threeD.value;
    }

   /* public GameObject getCharacter(int i)
    {
        if (i == 1)
        {
            return One;
        }
        else if (i == 2)
        {
            return Two;
        }
        else if (i == 3)
        {
            return Three;
        }
        else
        {
            return Four;
        }


    }*/
    


}
