using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardMenu : MonoBehaviour {
    [SerializeField]
    private GameObject content;

    private PlayerData data;
    private Text[] texts;
     
    private void Awake () {
        data = Resources.Load<PlayerData>("PlayerData");
        texts = content.GetComponentsInChildren<Text>();
    }

    private void Start()
    {
        //data.LeaderboardDatas.Sort();
        //data.LeaderboardDatas.Reverse();
        for (int i = 0; (i < texts.Length) || (i < data.LeaderboardDatas.Count); i++)
        {
            texts[i].text = data.LeaderboardDatas[i].score.ToString() + data.LeaderboardDatas[i].date;
        }
    }
}
