using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "CreateScriptable/GameSetting", order = 2)]
public class GameSetting : ScriptableObject
{
    public float musicValume;
    public float effectValume;
}
