using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDirector : Singleton<LevelDirector>
{

    #region Fields
    private MainAirplane mainAirPlane;
    private GameObject bossPlane;
    private PlayerData date;
   
    private int score;
    private int maxScore;
    private int playerLifeCount = 3;

    #endregion

    #region Properties
    public int Score {
        get { return score; }
        set {
            score = value;
            if (maxScore < score) {
                date.maxScore = value;
                maxScore = value;
            }
        }
    }
    public int MaxScore{get { return maxScore; } }
    public int PlayerLifeCount { get { return playerLifeCount;} }
    public MainAirplane CurrentAirPlane { get; private set; }
    #endregion

    public Action GameStartAction;
    public Action GameOverAction; 

    #region Massagers
    protected override void Awake() {
        Init();
    }
    private void Start () {
        if (GameStartAction != null)
            GameStartAction();
        if (UIManager.Instance != null)
            UIManager.Instance.FaderOn(false, 1f);
        StartCoroutine(Decorate());
    }
    #endregion

    #region Member Function
    private void Init() {
        mainAirPlane = Resources.Load<MainAirplane>("Prefabs/MainPlane");
        date = Resources.Load<PlayerData>("PlayerData");
        maxScore = date.maxScore;
    }

    private IEnumerator Decorate()
    {
        yield return new WaitForSeconds(2);
        CurrentAirPlane = Instantiate(mainAirPlane, mainAirPlane.transform.position, Quaternion.identity);
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
    }

    private void OnMainPlaneDead() {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(Decorate());
        }
        else {
            GameOver();
        }
    }

    public void GameOver() {
        if (GameOverAction != null)
            GameOverAction();
    }
    #endregion

}
