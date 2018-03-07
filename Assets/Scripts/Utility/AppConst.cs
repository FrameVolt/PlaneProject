using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst{


    public const string AppName = "PlaneProject";
    public const string WebUrl = @"http://127.0.0.1:8081/";
    public const int GameFrameRate = 300;


    public static string DataPath
    {
        get
        {
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/" + AppName + "/";
            }
            else
            {
                return "c:/" + AppName + "/";
            }
        }
    }
}
