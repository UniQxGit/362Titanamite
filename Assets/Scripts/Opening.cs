using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {
    public InputField username;
    public InputField password;

    public GameObject errorUsername;
    public GameObject errorPassword;

	// Use this for initialization

    void CheckValue()
    {
        errorUsername.SetActive(false);
        errorPassword.SetActive(false);


        if (!Info.loaded)
        {
            Info.username = username.text;
            Info.password = password.text;
            Info.loggedIn = true;
            LoadLevel();
        }
        else
        {
            bool isValid = true;
            if (Info.username != username.text)
            {
                errorUsername.SetActive(true);
                isValid = false;
            }
            if (Info.password != password.text)
            {
                errorPassword.SetActive(true);
                isValid = false;
            }

            if (isValid)
            {
                Info.loggedIn = true;
                LoadLevel();
            }
        }

    }

    void UpdateFields()
    {

        Debug.Log("Updated: " + Info.username + " " + Info.password + " ."); 
    }
	
	void LoadLevel(){
        Info.Save();
		Application.LoadLevel("Test");
	}
}
