using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    [SerializeField]
    private string loadSceneName;

    [SerializeField]
    private GameObject Spinner;

    private bool canLoad;

    private void Start () {
        UIManager.Instance.FaderOn(false, 1f);
        NetworkManager.Instance.OnLuaInjected(OnLuaInjected);

        StartCoroutine(LoadFirstLevel());
    }
    
    private void OnLuaInjected() {
        canLoad = true;
        Spinner.SetActive(false);
    }

    private IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(2f);
        while (!canLoad) {
            yield return null;
        }
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }
}
