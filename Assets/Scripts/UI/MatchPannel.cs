using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPannel : Photon.PunBehaviour
{
    [SerializeField]
    private string loadSceneName = "";
    [SerializeField]
    private GameObject content = null;
    [SerializeField]
    private GameObject _playerListingPerfab;
    private GameObject PlayerListingPerfab { get { return _playerListingPerfab; } }

    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings { get { return _playerListings; } }

    public override void OnJoinedRoom()
    {
        print("OnJoindRoom");

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;

        for (int i = 0; i < photonPlayers.Length; i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null) { return; }
        //same person joined room remove it
        PlayerLeftRoom(photonPlayer);

        GameObject playerListingOBJ = Instantiate(PlayerListingPerfab);
        playerListingOBJ.transform.SetParent(content.transform, false);

        PlayerListing playerListing = playerListingOBJ.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        PlayerListings.Add(playerListing);

    }
    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);

        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);

        StartCoroutine(StartBattle());
    }


    public IEnumerator StartBattle()
    {
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }
}

