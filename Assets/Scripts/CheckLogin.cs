using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Info.Init();
        Info.Load();

        if (Info.loggedIn)
        {
<<<<<<< HEAD
            Application.LoadLevel("Test");
=======
            Application.LoadLevel("MainPage");
>>>>>>> e78b432d4b9470554963661966af13292efcd063
        }
        else
        {
            Application.LoadLevel("Opening");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
