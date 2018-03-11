using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject Spinner;

    [SerializeField]
    private CanvasGroup loginPannelGroup;
    [SerializeField]
    private CanvasGroup logoPannelGroup;

    private bool luaInjected;
    private bool joinedLobby;

    private void Start()
    {
        UIManager.Instance.FaderOn(false, 1f);

        NetworkManager.Instance.OnLuaInjected(OnLuaInjected);
        StartCoroutine(ShowLoginPannel());
    }

    private void OnLuaInjected()
    {
        luaInjected = true;
    }
    void OnJoinedLobby()
    {
        joinedLobby = true;
    }
    private IEnumerator ShowLoginPannel()
    {
        yield return new WaitForSeconds(2f);
        while (!luaInjected || !joinedLobby)
        {
            yield return null;
        }
        Spinner.SetActive(false);
        DisplayMenu();
    }

    private void DisplayMenu()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.To(() => logoPannelGroup.alpha, x => logoPannelGroup.alpha = x, 0, 1).OnComplete(() =>
        {
            logoPannelGroup.interactable = false;
            logoPannelGroup.blocksRaycasts = false;
        }));
        mySequence.Append(DOTween.To(() => loginPannelGroup.alpha, x => loginPannelGroup.alpha = x, 1, 1).OnComplete(() =>
        {
            loginPannelGroup.interactable = true;
            loginPannelGroup.blocksRaycasts = true;
        }));
    }
}
