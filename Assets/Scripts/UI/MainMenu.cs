using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string loadSceneName;
    [SerializeField]
    private CanvasGroup mainMenuGroup;
    [SerializeField]
    private CanvasGroup optionGroup;
    [SerializeField]
    private CanvasGroup creditGroup;
    [SerializeField]
    private CanvasGroup leaderboardGroup;


    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private void Start()
    {
        UIManager.Instance.FaderOn(false, 1f);
        canvasGroupList.Add(mainMenuGroup);
        canvasGroupList.Add(optionGroup);
        canvasGroupList.Add(creditGroup);
        canvasGroupList.Add(leaderboardGroup);
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
        if (canvasGroupStack.Count <= 1) return;

        canvasGroupStack.Pop();
        DisplayMenu();
    }
    public void StartButton()
    {
        UIManager.Instance.FaderOn(true, 1f);
        StartCoroutine(StartLevel());
    }
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
