using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleBuilder : Editor {


    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();

    [MenuItem("Assets/Create/Build Asset bundle")]
    static void BuildAB() {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        BuildFileIndex();
        AssetDatabase.Refresh();
        Debug.Log("finish build asset bundle");
    }
    
    static void BuildFileIndex() {
        string resPath = Application.dataPath + "/StreamingAssets/";
        string filePath = resPath + "files.txt";

        if (File.Exists(filePath)) File.Delete(filePath);

        paths.Clear();
        files.Clear();
        GetFiles(resPath);

        FileStream fs = new FileStream(filePath, FileMode.CreateNew);

        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

            string value = file.Replace(resPath, string.Empty);
            Debug.Log("-----------");
            sw.WriteLine(value);
        }

        sw.Close();
        fs.Close();
    }
    
    static void GetFiles(string path) {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);

        foreach (string filename in names) {
            
            string sxt = Path.GetExtension(filename);
            if (sxt.Equals(".meta")) continue;
            Debug.Log(filename);
            files.Add(filename);
        }

        foreach (var item in dirs)
        {
            Debug.Log(item);
            GetFiles(item);
        }
    }
}
