using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPannel : MonoBehaviour {
    [SerializeField]
    private InputField inputField = null;
    private PlayerData playerdata;

    [SerializeField]
    private string loadSceneName;


    private void Awake()
    {
        playerdata = Resources.Load<PlayerData>("PlayerData");
    }

    public void SetPlayerName()
    {
        playerdata.playerName = inputField.text;
        PhotonNetwork.playerName = playerdata.playerName;
        StartCoroutine(Login());

    }

    public IEnumerator Login()
    {
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }
}
