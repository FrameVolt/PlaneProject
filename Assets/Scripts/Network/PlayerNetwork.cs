using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    private PhotonView photonView;
    private int playersInGame;

    //private PlayerMovement currentPlayer;

    private void Awake()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();
        //SceneManager.sceneLoaded += OnSceneFinishedLoaded;

    }

    //private void OnSceneFinishedLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name == "Game")
    //    {
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            MasterLoadedGame();
    //        }
    //        else
    //        {
    //            NonMasterLoadedGame();
    //        }
    //    }
    //}

    //private void MasterLoadedGame()
    //{
    //    photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    //    photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);

    //}

    //private void NonMasterLoadedGame()
    //{
    //    photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);

    //}
    //[PunRPC]
    //private void RPC_LoadGameOthers()
    //{
    //    PhotonNetwork.LoadLevel(1);
    //}
    //[PunRPC]
    //private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    //{
    //    PlayerManagement.Instance.AddPlayerStats(photonPlayer);

    //    playersInGame++;
    //    if (playersInGame == PhotonNetwork.playerList.Length)
    //    {
    //        print("All players are in the game.");
    //        photonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
    //    }
    //}

    //public void NewHealth(PhotonPlayer photonPlayer, int health)
    //{
    //    photonView.RPC("RPC_NewHealth", photonPlayer, health);
    //}

    //[PunRPC]
    //private void RPC_NewHealth(int health)
    //{
    //    if (currentPlayer == null) return;

    //    if (health <= 0)
    //    {
    //        PhotonNetwork.Destroy(currentPlayer.gameObject);
    //    }
    //}

    //[PunRPC]
    //private void RPC_CreatePlayer()
    //{
    //    float randomValue = Random.Range(0, 5);
    //    GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Perfabs", "Player"), Vector3.up * randomValue, Quaternion.identity, 0);
    //    currentPlayer = obj.GetComponent<PlayerMovement>();
    //}
}
