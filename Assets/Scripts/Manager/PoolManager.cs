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


    public void CreateByBundle(string objName, string bundleName, Action<UObject> func) {
        StartCoroutine(CreateByBundleName(objName, bundleName, func));
    }

    private IEnumerator CreateByBundleName(string objName, string bundleName, Action<UObject> func)
    {
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(Path.Combine(AppConst.DataPath, "StreamingAssets"), 0));
        manifestBundle = AssetBundleManager.GetAssetBundle(Path.Combine(AppConst.DataPath, "StreamingAssets"), 0);
        manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");


        string[] depends = manifest.GetAllDependencies(bundleName);

        AssetBundle[] dependsAssetbundle = new AssetBundle[depends.Length];
        for (int index = 0; index < depends.Length; index++)
        {
            //加载所有的依赖文件; 
            yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath + depends[index], 0));
            dependsAssetbundle[index] = AssetBundleManager.GetAssetBundle(AppConst.DataPath + depends[index], 0);
        }
        
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath + bundleName, 0));
        AssetBundle bundle = AssetBundleManager.GetAssetBundle(AppConst.DataPath + bundleName, 0);
        if (bundle != null)
        {
            GameObject go = bundle.LoadAsset<GameObject>(objName);
            if (func != null) {
                func(go);
            }
        }
    }

}
