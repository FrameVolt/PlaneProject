using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LevelDirector : Singleton<LevelDirector>
{

    #region Fields
    //private MainAirplane mainAirPlane;
    //private TankEnemy tankPerfab;
    //private NormalEnemy normalEnemyPerfab;

    //private BossEnemy bossEnemyPerfab;

    private GameObject mainAirPlane;
    private GameObject tankPerfab;
    private GameObject normalEnemyPerfab;
    private GameObject bossEnemyPerfab;

    private PlayerData data;

    private int score;
    private int maxScore;
    private int playerLifeCount = 3;
    private PhotonView photonView;
    private int playersInGame;
    #endregion

    #region Properties
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (maxScore < score)
            {
                data.maxScore = value;
                maxScore = value;
            }
        }
    }
    public int MaxScore { get { return maxScore; } }
    public int PlayerLifeCount { get { return playerLifeCount; } }
    public MainAirplane CurrentAirPlane { get; private set; }
    #endregion

    public Action GameStartAction;
    public Action GameOverAction;
    public Action GameWinAction;
    #region Massagers
    protected override void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        Init();
        if (GameStartAction != null)
            GameStartAction();
        if (UIManager.Instance != null)
            UIManager.Instance.FaderOn(false, 1f);
        StartCoroutine(Decorate());
    }
    #endregion

    #region Member Function
    private void Init()
    {
        //mainAirPlane = Resources.Load<GameObject>("Prefabs/MainPlane");
        //bossEnemyPerfab = Resources.Load<GameObject>("Prefabs/Enemys/Boss");
        //normalEnemyPerfab = Resources.Load<GameObject>("Prefabs/Enemys/NormalEnemy");
        //tankPerfab = Resources.Load<GameObject>("Prefabs/Enemys/Tank");

        // PoolManager.Instance.CreateByBundle("MainPlane", "perfabs.unity3d", delegate (UnityEngine.Object go) { mainAirPlane = (GameObject)go; });
        PoolManager.Instance.CreateByBundle("MainPlane", "perfabs.unity3d", go => { mainAirPlane = (GameObject)go; });
        PoolManager.Instance.CreateByBundle("Boss", "perfabs.unity3d", delegate (UnityEngine.Object go) { bossEnemyPerfab = (GameObject)go; });
        PoolManager.Instance.CreateByBundle("NormalEnemy", "perfabs.unity3d", delegate (UnityEngine.Object go) { normalEnemyPerfab = (GameObject)go; });
        PoolManager.Instance.CreateByBundle("Tank", "perfabs.unity3d", delegate (UnityEngine.Object go) { tankPerfab = (GameObject)go; });
        data = Resources.Load<PlayerData>("PlayerData");
        maxScore = data.maxScore;
    }

    private IEnumerator Decorate()
    {
        yield return new WaitForSeconds(2);
        Instantiate(normalEnemyPerfab, normalEnemyPerfab.transform.position, Quaternion.identity);
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
      
        yield return new WaitForSeconds(10);
        Instantiate(tankPerfab, tankPerfab.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bossEnemyPerfab, bossEnemyPerfab.transform.position, Quaternion.identity);
    }
    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayerManagement.Instance.AddPlayerStats(photonPlayer);

        playersInGame++;
        if (playersInGame == PhotonNetwork.playerList.Length)
        {
            int id1 = PhotonNetwork.AllocateViewID();
            photonView.RPC("RPC_SpawPlayer", PhotonTargets.AllBuffered, id1);
        }
    }
    [PunRPC]
    private void RPC_SpawPlayer(int id1)
    {
        CurrentAirPlane = Instantiate(mainAirPlane, mainAirPlane.transform.position, Quaternion.identity).GetComponent<MainAirplane>();
        PhotonView nView = CurrentAirPlane.GetComponentInChildren<PhotonView>();
        nView.viewID = id1;
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
    }
    //private void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    print("OnPhotonInstantiate");
    //}

    private IEnumerator RebornPlayer()
    {
        yield return new WaitForSeconds(2);
        CurrentAirPlane = Instantiate(mainAirPlane, mainAirPlane.transform.position, Quaternion.identity).GetComponent<MainAirplane>();
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
    }


    private void OnMainPlaneDead()
    {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(RebornPlayer());
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (GameOverAction != null)
            GameOverAction();

        AddHistoryScore();
    }


    public void GameWin()
    {
        if (GameWinAction != null)
        {
            GameWinAction();
        }
        StartCoroutine(BackToMenu());
    }

    private void AddHistoryScore()
    {
        if (Score <= 0) return;

        if (data.LeaderboardDatas.Count >= 10)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (Score > data.LeaderboardDatas[i].score)
                {
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = score;
                    leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
                    data.LeaderboardDatas.Add(leaderboardData);
                    break;
                }
            }
            if (data.LeaderboardDatas.Count > 10)
                data.LeaderboardDatas.RemoveAt(data.LeaderboardDatas.Count - 2);
        }
        else
        {
            LeaderboardData leaderboardData = new LeaderboardData();
            leaderboardData.score = score;
            leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
            data.LeaderboardDatas.Add(leaderboardData);
        }
    }


    public IEnumerator BackToMenu(float delayTime = 2.0f)
    {
        AddHistoryScore();
        yield return new WaitForSecondsRealtime(delayTime);
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSecondsRealtime(1);
        LoadSceneManager.LoadScene(1);
    }

    #endregion

}
