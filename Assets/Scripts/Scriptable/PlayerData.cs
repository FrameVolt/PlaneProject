using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreateScriptable/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int maxScore;
    public List<LeaderboardData> LeaderboardDatas = new List<LeaderboardData>();
    //public Dictionary<int,string> scoresHistory2 = new Dictionary<int, string>();
}

[Serializable]
public struct LeaderboardData : IComparable<LeaderboardData>
{
    public int score;
    public string date;

    public int CompareTo(LeaderboardData x)
    {
        return score.CompareTo(x.score);
    }
}
