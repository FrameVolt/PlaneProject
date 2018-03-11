using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {

    public PhotonPlayer PhotonPlayer { get; private set; }

    [SerializeField]
    private Text _playerName;
    public Text PlayerName { get { return _playerName; } }

    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        this.PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;
    }
}
