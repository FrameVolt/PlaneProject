using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Photon.PunBehaviour
{
    [SerializeField]
    private string loadSceneName = "";
    [SerializeField]
    private CanvasGroup mainMenuGroup;
    [SerializeField]
    private CanvasGroup optionGroup;
    [SerializeField]
    private CanvasGroup creditGroup;
    [SerializeField]
    private CanvasGroup leaderboardGroup;
    [SerializeField]
    private CanvasGroup matchGroup;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private bool sendedJoinRandomRoom;

    private void Start()
    {
        UIManager.Instance.FaderOn(false, 1f);
        canvasGroupList.Add(mainMenuGroup);
        canvasGroupList.Add(optionGroup);
        canvasGroupList.Add(creditGroup);
        canvasGroupList.Add(leaderboardGroup);
        canvasGroupList.Add(matchGroup);
        canvasGroupStack.Push(mainMenuGroup);
        DisplayMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc();
        }
    }

    public void Esc()
    {
        if (canvasGroupStack.Count <= 1 || sendedJoinRandomRoom) return;

        canvasGroupStack.Pop();
        DisplayMenu();
    }
    public void MatchButton()
    {
        sendedJoinRandomRoom = true;
        canvasGroupStack.Push(matchGroup);
        DisplayMenu();
        PhotonNetwork.JoinRandomRoom();
 
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = AppConst.MaxPlayersPerRoom }, null);
    }
    //public void StartButton()
    //{
    //    UIManager.Instance.FaderOn(true, 1f);
    //    StartCoroutine(StartLevel());
    //}
    public void OptionButton()
    {
        canvasGroupStack.Push(optionGroup);
        DisplayMenu();
    }
    public void CreditButton()
    {
        canvasGroupStack.Push(creditGroup);
        DisplayMenu();
    }
    public void LeaderboardButton()
    {
        canvasGroupStack.Push(leaderboardGroup);
        DisplayMenu();
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }
    private void DisplayMenu()
    {
        foreach (var item in canvasGroupList)
        {
            item.alpha = 0;
            item.interactable = false;
            item.blocksRaycasts = false;
        }

        if (canvasGroupStack.Count > 0)
        {
            CanvasGroup cg = canvasGroupStack.Peek();
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }


    }
}
