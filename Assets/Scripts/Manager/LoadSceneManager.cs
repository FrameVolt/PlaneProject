using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager {

    //public const string LoadingScreenSceneName = "LoadingScreen";
    
    public static void LoadScene(string name)
    {
      
        LoadScene(SceneUtility.GetBuildIndexByScenePath(name));
    }

    public static void LoadScene(int index)
    {
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        //if (LoadingScreenSceneName != null)
        //{
        //    SceneManager.LoadScene(LoadingScreenSceneName);
        //}
        SceneManager.LoadScene(index);
        GameManager.Instance.UnPause();
    }

    //private IEnumerator OnLoadingScene(AsyncOperation asyncOperation) {
    //    yield return null;
    //}
}
