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
            Application.LoadLevel("Test");
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
