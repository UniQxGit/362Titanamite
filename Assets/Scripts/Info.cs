using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {
    public static string username;
    public static string password;
    public static bool loggedIn;
    public static bool loaded;

    public static string blue;

    public static void Init()
    {
        username = "test@gmail.com";
        password = "password";
        loggedIn = false;
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.SetString("username",username);
        PlayerPrefs.SetString("password", password);
        //PlayerPrefs.SetInt("loggedIn",(loggedIn?1:0));
    }

    public static void Load()
    {
        if(!PlayerPrefs.HasKey("Save"))
        {
            loaded = true;
            PlayerPrefs.SetString("username",username);
            PlayerPrefs.SetString("password", password);
            return;
        }

        username = PlayerPrefs.GetString("username");
        password = PlayerPrefs.GetString("password");
        loggedIn = PlayerPrefs.GetInt("loggedIn")==1?true:false;
        loaded = true;
    }
}
