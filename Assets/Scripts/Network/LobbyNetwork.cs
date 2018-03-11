using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : Photon.PunBehaviour
{

    private PlayerData playerdata;
    private void Awake()
    {
        playerdata = Resources.Load<PlayerData>("PlayerData");
    }

	void Start () {
        print("connect to servers.");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}
	
	
	public override void OnConnectedToMaster () {
        print("connected to Master.");
        PhotonNetwork.automaticallySyncScene = false;
        //PhotonNetwork.playerName = playerdata.playerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
	}

    public override void OnJoinedLobby() {
        print("On joined lobby");
        //if(!PhotonNetwork.inRoom)
        //MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();

    }
}
