using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {
    public InputField username;
    public InputField password;

    public GameObject errorUsername;
    public GameObject errorPassword;

    public GameObject uiObjects;
    
    public Renderer bgRen;


	// Use this for initialization
    void Start()
    {
        StartCoroutine("FadeBackground");
    }


    IEnumerator FadeBackground()
    {
        float acc = 2.0f;
        Color c;
        while (acc > 0.0f)
        {
            c = bgRen.material.color;
            c.a = acc / 2.0f;
            bgRen.material.color = c;
            acc -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        uiObjects.SetActive(true);
    }

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
