using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static bool sfx = true;
    public static bool music = true;
    public static float mouseSensibility = 0.5f;
    //public static string gameMode = "normal";
    public static string gameMode = "survival";
}
