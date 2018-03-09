using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UObject = UnityEngine.Object;

public class PoolManager : PersistentSingleton<PoolManager>
{
    private AssetBundle manifestBundle;
    private AssetBundleManifest manifest;


    protected override void Awake()
    {
        base.Awake();
    }

    public void CreateByBundle(string objName, string bundleName, Action<UObject> func) {
        CreateByBundleName(objName, bundleName, func);
    }

    private void CreateByBundleName(string objName, string bundleName, Action<UObject> func)
    {
        if (manifest == null) {
            manifestBundle = AssetBundle.LoadFromFile(Path.Combine(AppConst.DataPath, "StreamingAssets"));
            manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
        }

        string[] depends = manifest.GetAllDependencies(bundleName);
        
        for (int index = 0; index < depends.Length; index++)
        {
            //加载所有的依赖文件;
            StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath + depends[index], 0, null));
        }
        
        StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath + bundleName, 0,
            x => {
            GameObject go = ((AssetBundle)x).LoadAsset<GameObject>(objName);
            if (func != null)
            {
                func(go);
            }
        }));
    }



}
