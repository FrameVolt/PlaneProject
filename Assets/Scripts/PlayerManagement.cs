using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : Singleton<PlayerManagement> {

    
    private PhotonView photonView;
    [SerializeField]
    private List<PlayerStats> playerStatsList = new List<PlayerStats>();

    protected override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();

    }

    public void AddPlayerStats(PhotonPlayer photonPlayer)
    {
        int index = playerStatsList.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index == -1) {
            playerStatsList.Add(new PlayerStats(photonPlayer, 30));
        }

    }

    //public void ModifyHealth(PhotonPlayer photonPlayer, int value)
    //{
    //    int index = playerStatsList.FindIndex(x => x.PhotonPlayer == photonPlayer);
    //    if (index != -1)
    //    {
    //        PlayerStats playerStats = playerStatsList[index];
    //        playerStats.Health += value;
    //        PlayerNetwork.Instance.NewHealth(photonPlayer, playerStats.Health);
    //    }
    //}
}
[System.Serializable]
public class PlayerStats {
    public PlayerStats(PhotonPlayer photonPlayer, int health) {
        PhotonPlayer = photonPlayer;
        this.Health = health;
    }

    public readonly PhotonPlayer PhotonPlayer;
    public int Health;
}
