using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using UnityEngine.Networking;
using System;

public class NetworkManager : PersistentSingleton<NetworkManager>
{

    private static LuaEnv luaEnv = null;

    private Action OnLuaInjectedCallback;
    public void OnLuaInjected(Action luaInjected) {
        OnLuaInjectedCallback += luaInjected;
    }
    protected override void Awake () {
        base.Awake();
        StartCoroutine(OnUpdateResource());
        luaEnv = new LuaEnv();
    }
	
    IEnumerator OnUpdateResource()
    {
        print("OnUpdateResource");
        string webListUrl = AppConst.WebUrl + "files.txt";

        UnityWebRequest request = UnityWebRequest.Get(webListUrl);
        yield return request.SendWebRequest();
        if (!Directory.Exists(AppConst.DataPath))
        {
            Directory.CreateDirectory(AppConst.DataPath);
        }
        File.WriteAllBytes(AppConst.DataPath + "files.txt", request.downloadHandler.data);

        string filesText = request.downloadHandler.text;
        string[] files = filesText.Split('\n');
        for (int i = 0; i < files.Length; i++)
        {

            if (string.IsNullOrEmpty(files[i])) continue;
            string localfile = (AppConst.DataPath + files[i]).Trim();
            string path = Path.GetDirectoryName(localfile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string webPath = AppConst.WebUrl + files[i];
            yield return StartCoroutine(Load(webPath, localfile));
        }

        print("All assetbundle loaded");
        InitAllLuaScripts();
    }

    private IEnumerator Load(string webPath, string localPath)
    {
        UnityWebRequest request = UnityWebRequest.Get(webPath);
        yield return request.SendWebRequest();
        DownloadHandler handler = request.downloadHandler;
        File.WriteAllBytes(localPath, handler.data);
    }

    private void InitAllLuaScripts()
    {
        string luaScriptsPath = Path.Combine(AppConst.DataPath, "LuaScripts");
        if (!Directory.Exists(luaScriptsPath))
        {
            Directory.CreateDirectory(luaScriptsPath);
        }
        string[] fileNames = Directory.GetFiles(luaScriptsPath);

        for (int i = 0; i < fileNames.Length; i++)
        {
            byte[] bytes = File.ReadAllBytes(fileNames[i]);
            luaEnv.DoString(System.Text.Encoding.UTF8.GetString(bytes));
        }
        print("All lua injected");

        if (OnLuaInjectedCallback != null) {
            OnLuaInjectedCallback();
        }
    }
}
