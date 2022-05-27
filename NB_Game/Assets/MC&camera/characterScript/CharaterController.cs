using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterController : MonoBehaviour
{

    [SerializeField]private Button FirstButton, SecondButton, ThirdButton;
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

    //characternumber,character position
    private Dictionary<int, int> characterPosition;
    private Dictionary<int, GameObject> characters;

    void Start()
    {
        characters = new Dictionary<int, GameObject>();
        characterPosition = new Dictionary<int, int>();
        current = 1;
        team.currentC = 1;
        threeD.value = true;
        twoDS = GetComponent<COnTwoD>();
        Load();
        twoDS.enabled = !threeD.value;
        threeDS.enabled = threeD.value;
        SwithchState();
        FirstButton.onClick.AddListener(delegate { changeByBottom(1); });
        SecondButton.onClick.AddListener(delegate { changeByBottom(2); });
        ThirdButton.onClick.AddListener(delegate { changeByBottom(3); });
        change(2);
        change(1);
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
            change(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            change(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            change(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            change(4);
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
        characters.Add(1, One);
        //chara.Add(1, Instantiate(team.OneC, this.transform));
        Two = Instantiate(team.TwoC, this.transform);
        characters.Add(2, Two);
        //chara.Add(2, Two);
        Three = Instantiate(team.ThreeC, this.transform);
        characters.Add(3, Three);
        //chara.Add(3, Three);
        Four = Instantiate(team.FourC, this.transform);
        characters.Add(4, Four);
        //chara.Add(4, Four);
        GlobalUiManager.setName(1, Two);
        characterPosition.Add(2, 1);
        GlobalUiManager.setName(2, Three);
        characterPosition.Add(3, 2);
        GlobalUiManager.setName(3, Four);
        characterPosition.Add(4, 3);


       

        characters[current].SetActive(true);
    }

    
    void change(int i)
    {
        
        if (i !=current&& !characters[i].GetComponent<characterHealth>().died)
        {
            int beforeCurrent = current;
            current = i;
         
            One.SetActive(false);
            Two.SetActive(false);
            Three.SetActive(false);
            Four.SetActive(false);

            characters[i].SetActive(true);
                team.currentC = i;
            //set name that the bolck i currently was into the character previously in scene

                GlobalUiManager.setName(characterPosition[i], characters[beforeCurrent]);

            //change characterPosition
            //add before current's index to the position of current 
            characterPosition[beforeCurrent] = characterPosition[i];
            characterPosition.Remove(i);
            //characterPosition.Remove(i);
            
        }
        
    }

    private void changeByBottom(int position)
    {
        int Changedcharacter=0;
        //loop through the hashMap to find character at that position
        foreach (int character  in characterPosition.Keys)
        {
            if (characterPosition[character]==position)
            {
                Changedcharacter = character;
            }
        }
        change(Changedcharacter);
    }

    /*void change(int i)
    {
        int beforeCurrent = current;
        current = i;
        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);
        Four.SetActive(false);

        if (i == 1&& !One.GetComponent<characterHealth>().died)
        {
            
                One.SetActive(true);
                team.currentC = 1;
                GlobalUiManager.setName(1, Two);
                GlobalUiManager.setName(2, Three);
                GlobalUiManager.setName(3, Four);
            
        }
        else if (i == 2 && !Two.GetComponent<characterHealth>().died)
        {
            
            Two.SetActive(true);
            team.currentC = 2;
            GlobalUiManager.setName(1, One);
            GlobalUiManager.setName(2, Three);
            GlobalUiManager.setName(3, Four);
        }
        else if (i == 3 && !Three.GetComponent<characterHealth>().died)
        {
            Three.SetActive(true);
            team.currentC = 3;
            GlobalUiManager.setName(1, One);
            GlobalUiManager.setName(2, Two);
            GlobalUiManager.setName(3, Four);
        }
        else if( !Four.GetComponent<characterHealth>().died)
        {
            Four.SetActive(true);
            team.currentC = 4;
            GlobalUiManager.setName(1, One);
            GlobalUiManager.setName(2, Two);
            GlobalUiManager.setName(3, Three);
        }
    }*/
    void SwithchState()
    {
        threeD.value = !threeD.value;
        twoDS.enabled = !threeD.value;
        threeDS.enabled = threeD.value;

        One.GetComponent<subCharaCopy>().enabled= threeD.value;
        Two.GetComponent<subCharaCopy>().enabled = threeD.value ;
        Three.GetComponent<subCharaCopy>().enabled = threeD.value ;
        Four.GetComponent<subCharaCopy>().enabled = threeD.value;
    }

    void LogDictionary(Dictionary<int, GameObject> a)
    {
        Debug.Log("characers");
        foreach (int character in characterPosition.Keys)
        {
            Debug.Log(character+" "+a[character]);
        }
        Debug.Log("/////////////");
    }
    void LogDictionary(Dictionary<int, int> a)
    {
        Debug.Log("charaPosition");
        foreach (int character in characterPosition.Keys)
        {
            Debug.Log(character + " " + a[character]);
        }
        Debug.Log("/////////////");
    }



}
