using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

    private PlayerData playerdata;

    private void Awkake()
    {
        playerdata = Resources.Load<PlayerData>("PlayerData");
    }

	void Start () {
        print("connect to servers.");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}
	
	
	void OnConnectedToMaster () {
        print("connected to Master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = playerdata.playerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
	}

    void OnJoinedLobby() {
        print("On joined lobby");
        //if(!PhotonNetwork.inRoom)
        //MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();

    }
}
