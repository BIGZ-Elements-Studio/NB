using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamInfo", menuName = "New Team")]
public class TeamObj : ScriptableObject
{
    
    public GameObject OneC;
    public GameObject TwoC;
    public GameObject ThreeC;
    public GameObject FourC;

    public int currentC;

    public float xVelocity;
    public float zVelocity;
    public bool faceRight;

}
