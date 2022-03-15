using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamInfo", menuName = "New Team")]
public class TeamObj : ScriptableObject
{
    public CharacterInfo One;
    public CharacterInfo Two;
    public CharacterInfo Three;
    public CharacterInfo Four;

}
