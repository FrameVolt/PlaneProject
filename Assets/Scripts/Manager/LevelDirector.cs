using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDirector : Singleton<LevelDirector>
{

    #region Fields
    private MainAirplane mainAirPlane;
    private TankEnemy tankPerfab;
    private NormalEnemy normalEnemyPerfab;

    private BossEnemy bossEnemyPerfab;
    private PlayerData data;
   
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
                data.maxScore = value;
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
    public Action GameWinAction;
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
        bossEnemyPerfab = Resources.Load<BossEnemy>("Prefabs/Enemys/Boss");
        normalEnemyPerfab = Resources.Load<NormalEnemy>("Prefabs/Enemys/NormalEnemy");
        tankPerfab = Resources.Load<TankEnemy>("Prefabs/Enemys/Tank");
        data = Resources.Load<PlayerData>("PlayerData");
        maxScore = data.maxScore;
    }

    private IEnumerator Decorate()
    {
        yield return new WaitForSeconds(2);
        Instantiate(normalEnemyPerfab, normalEnemyPerfab.transform.position, Quaternion.identity);
        CurrentAirPlane = Instantiate(mainAirPlane, mainAirPlane.transform.position, Quaternion.identity);
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
        yield return new WaitForSeconds(10);
        Instantiate(tankPerfab, tankPerfab.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bossEnemyPerfab, bossEnemyPerfab.transform.position, Quaternion.identity);
    }

    private IEnumerator RebornPlayer()
    {
        yield return new WaitForSeconds(2);
        CurrentAirPlane = Instantiate(mainAirPlane, mainAirPlane.transform.position, Quaternion.identity);
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
    }


    private void OnMainPlaneDead() {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(RebornPlayer());
        }
        else {
            GameOver();
        }
    }

    public void GameOver() {
        if (GameOverAction != null)
            GameOverAction();

        AddHistoryScore();
    }


    public void GameWin() {
        if (GameWinAction != null) {
            GameWinAction();
        }
        StartCoroutine(BackToMenu());
    }

    private void AddHistoryScore() {
        if (Score <= 0) return;

        if (data.LeaderboardDatas.Count > 10)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (Score > data.LeaderboardDatas[i].score)
                {
                    data.LeaderboardDatas.RemoveAt(i);
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = score;
                    leaderboardData.date = "11";
                    data.LeaderboardDatas.Add(leaderboardData);
                }
            }
        }
        else {
            LeaderboardData leaderboardData = new LeaderboardData();
            leaderboardData.score = score;
            leaderboardData.date = "11";
            data.LeaderboardDatas.Add(leaderboardData);
        }
    }


    public IEnumerator BackToMenu() {
        AddHistoryScore();
        yield return new WaitForSeconds(2);
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1);
        LoadSceneManager.LoadScene(1);
    }

    #endregion

}
