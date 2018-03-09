using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UObject = UnityEngine.Object;

static public class AssetBundleManager
{

    // A dictionary to hold the AssetBundle references
    static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;

    static private bool isLoading;

    static AssetBundleManager()
    {
        dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
    }

    // Class with the AssetBundle reference, url and version
    private class AssetBundleRef
    {
        public AssetBundle assetBundle = null;
        public int version;
        public string url;
        public AssetBundleRef(string strUrlIn, int intVersionIn)
        {
            url = strUrlIn;
            version = intVersionIn;
        }
    };

    // Get an AssetBundle
    private static AssetBundle GetAssetBundle(string url, int version)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;

        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            return abRef.assetBundle;
        else
            return null;
    }

    // Download an AssetBundle
    //public static IEnumerator DownloadAssetBundle(string url, int version)
    //{


    //    string keyName = url + version.ToString();
    //    if (dictAssetBundleRefs.ContainsKey(keyName))
    //        yield return null;
    //    else
    //    {

    //        isLoading = true;
    //        AssetBundleRef abRef = new AssetBundleRef(url, version);

    //        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
    //        while (!request.isDone)
    //        {
    //            yield return null;
    //        }
    //        abRef.assetBundle = request.assetBundle;
    //        dictAssetBundleRefs.Add(keyName, abRef);
    //        isLoading = false;
    //    }
    //}

    public static IEnumerator DownloadAssetBundle(string url, int version, Action<UObject> func)
    {
        while (isLoading)
        {
            yield return null;
        }
        string keyName = url + version.ToString();
        if (dictAssetBundleRefs.ContainsKey(keyName))
        {
            if (func != null)
                func(GetAssetBundle(url, version));
        }
        else
        {

            isLoading = true;
            AssetBundleRef abRef = new AssetBundleRef(url, version);

            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
            while (!request.isDone)
            {
                yield return null;
            }
            abRef.assetBundle = request.assetBundle;
            if (func != null)
                func(request.assetBundle);
            dictAssetBundleRefs.Add(keyName, abRef);
            isLoading = false;
        }
    }

    // Unload an AssetBundle
    public static void Unload(string url, int version, bool allObjects)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
        {
            abRef.assetBundle.Unload(allObjects);
            abRef.assetBundle = null;
            dictAssetBundleRefs.Remove(keyName);
        }
    }
}
