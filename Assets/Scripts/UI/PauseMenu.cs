using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string loadSceneName;
    [SerializeField] private AudioMixerSnapshot paused, unpaused;
    [SerializeField] private CanvasGroup pauseGroup;
    [SerializeField] private CanvasGroup settingGroup;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private void Start()
    {
        canvasGroupList.Add(pauseGroup);
        canvasGroupList.Add(settingGroup);
        DisplayMenu();
    }

    private void Lowpass()
    {
        if (GameManager.Instance.TimeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }
    
    public void Esc()
    {
        if (canvasGroupStack.Count == 0)
        {
            Pause();
        }
        else
        {
            if (canvasGroupStack.Count > 0)
            {
                canvasGroupStack.Pop();
                if (canvasGroupStack.Count == 0)
                    UnPause();
            }
        }
        DisplayMenu();
    }
    public void Pause()
    {
        Lowpass();
        GameManager.Instance.Pause();
        canvasGroupStack.Push(pauseGroup);
        DisplayMenu();
    }
    public void UnPause()
    {
        Lowpass();
        GameManager.Instance.Pause();
        if (canvasGroupStack.Count > 0)
            canvasGroupStack.Pop();
        DisplayMenu();
    }

    public void Setting()
    {
        canvasGroupStack.Push(settingGroup);
        DisplayMenu();
    }

    public void Exit()
    {
        //#if UNITY_EDITOR
        //        EditorApplication.isPlaying = false;
        //#else
        //		Application.Quit();
        //#endif
        //Pause();
        //UIManager.Instance.FaderOn(true, 1f);
        //StartCoroutine(StartLevel());
        LevelDirector.Instance.StartCoroutine(LevelDirector.Instance.BackToMenu(0));
    }
    //private IEnumerator StartLevel()
    //{
    //    yield return new WaitForSeconds(1f);
    //    LoadSceneManager.LoadScene(loadSceneName);

    //}
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
