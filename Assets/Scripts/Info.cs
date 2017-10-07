using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {
<<<<<<< HEAD
=======
    public struct Class {
        public string name;
        public string professor;
        public int from;
        public int to;
        public string location;
        public string number;
        public string description;
        public bool[] days;

        Class(string s,string p,int f,int t,string l,bool[] d,string n,string desc)
        {
            name = s;
            professor = p;
            from = f;
            to = t;
            location = l;
            days = d;
            number = n;
            description = desc;
        }

        Class(string s,int f,int t,string l,bool[] d,string n)
        {
            name = s;
            professor = "";
            from = f;
            to = t;
            location = l;
            days = d;
            number = n;
            description = "";
        }
    }

>>>>>>> e78b432d4b9470554963661966af13292efcd063
    public static string username;
    public static string password;
    public static bool loggedIn;
    public static bool loaded;

<<<<<<< HEAD
    public static string blue;

=======
<<<<<<< HEAD
=======
    public static string blue;

    public static List<Class> myList = new List<Class>();
    public static Class[] schedule;

>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
    public static void Init()
    {
        username = "test@gmail.com";
        password = "password";
        loggedIn = false;
    }

    public static void Save()
    {
<<<<<<< HEAD
        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.SetString("username",username);
        PlayerPrefs.SetString("password", password);
        //PlayerPrefs.SetInt("loggedIn",(loggedIn?1:0));
=======
        int i=0;
        int ii=0;

        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.SetString("username",username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.SetInt("loggedIn",(loggedIn?1:0));

        PlayerPrefs.SetInt("classCount", schedule.Length);
        for(i=0;i<schedule.Length;i++)
        {
            PlayerPrefs.SetString("className" + i,schedule[i].name);
            PlayerPrefs.SetString("classProfessor" + i,schedule[i].professor);
            PlayerPrefs.SetString("classLocation" + i,schedule[i].location);

            PlayerPrefs.SetInt("classFrom" + i,schedule[i].from);
            PlayerPrefs.SetInt("classTo" + i,schedule[i].to);

            PlayerPrefs.SetInt("classDaysCount", schedule[i].days.Length);
            for(ii=0;ii<schedule[i].days.Length;ii++)
            {
                PlayerPrefs.SetInt("classDays" + ii,(schedule[i].days[ii]?1:0));    
            }
            
        }

>>>>>>> e78b432d4b9470554963661966af13292efcd063
    }

    public static void Load()
    {
<<<<<<< HEAD
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
=======

        if(!PlayerPrefs.HasKey("Save"))
        {
            loaded = true;
            //Temporary Until Database Implementation
            //{
                PlayerPrefs.SetString("username",username);
                PlayerPrefs.SetString("password", password);
            //}
            return;
        }

        int i=0;
        int ii=0;

        username = PlayerPrefs.GetString("username");
        password = PlayerPrefs.GetString("password");
        loggedIn = PlayerPrefs.GetInt("loggedIn")==1?true:false;

        //int classCount = ; 
        int classDaysCount =0;
        schedule = new Class[PlayerPrefs.GetInt("classCount")];
        for(i=0;i<schedule.Length;i++)
        {
            schedule[i].name = PlayerPrefs.GetString("className" + i);
            schedule[i].professor = PlayerPrefs.GetString("classProfessor" + i);
            schedule[i].location = PlayerPrefs.GetString("classLocation" + i);


            schedule[i].from = PlayerPrefs.GetInt("classFrom" + i);
            schedule[i].to = PlayerPrefs.GetInt("classTo" + i);

            schedule[i].days = new bool[PlayerPrefs.GetInt("classDaysCount")];
            for(ii=0;ii<schedule[i].days.Length;ii++)
            {
                schedule[i].days[ii] = PlayerPrefs.GetInt("classDays" + ii)==1?true:false;    
            }
        }

>>>>>>> e78b432d4b9470554963661966af13292efcd063
        loaded = true;
    }
}
