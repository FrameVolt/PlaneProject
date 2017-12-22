using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
    [SerializeField]
    private string loadSceneName;
    

    private void Start () {
        UIManager.Instance.FaderOn(false, 1f);
        StartCoroutine(LoadFirstLevel());
    }


    private IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(loadSceneName);
    }
}
