using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst{


    public const string AppName = "YaoPlane";
    public const string WebUrl = @"http://127.0.0.1:8080/";
    public const int GameFrameRate = 300;
    public const int MaxPlayersPerRoom = 2;
    public static bool DebugMode = true;

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

    public static bool isDebugMode = true;

}
