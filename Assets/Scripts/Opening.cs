using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {
    public InputField username;
    public InputField password;

    public GameObject errorUsername;
    public GameObject errorPassword;

<<<<<<< HEAD
    public Animator myAnim;
=======
<<<<<<< HEAD
    public GameObject uiObjects;
=======
    public Animator myAnim;
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
    
    public Renderer bgRen;


	// Use this for initialization
    void Start()
    {
        StartCoroutine("FadeBackground");
    }


    IEnumerator FadeBackground()
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
        float acc = 2.0f;
        Color c;
        while (acc > 0.0f)
        {
            c = bgRen.material.color;
            c.a = acc / 2.0f;
=======
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
        float acc = 1.5f;
        Color c;

        yield return new WaitForSeconds(0.5f);
        myAnim.SetTrigger("Open");
        while (acc > 0.0f)
        {
            c = bgRen.material.color;
            c.a = acc / 1.5f;
<<<<<<< HEAD
=======
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
            bgRen.material.color = c;
            acc -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
<<<<<<< HEAD
=======
<<<<<<< HEAD
        uiObjects.SetActive(true);
=======
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
        
    }

    IEnumerator TransitionOut()
    {
        float acc = 0.0f;
        Color c = new Color();

        myAnim.SetTrigger("Close");
        while (acc < 1.0f)
        {
            c = bgRen.material.color;
            c.a = acc / 1.0f;
            bgRen.material.color = c;
            acc += Time.deltaTime;
            yield return null;
        }

        LoadLevel();
<<<<<<< HEAD
=======
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
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
<<<<<<< HEAD
            StartCoroutine("TransitionOut");
=======
<<<<<<< HEAD
            LoadLevel();
=======
            StartCoroutine("TransitionOut");
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
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
<<<<<<< HEAD
                StartCoroutine("TransitionOut");
=======
<<<<<<< HEAD
                LoadLevel();
=======
                StartCoroutine("TransitionOut");
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
            }
        }

    }

    void UpdateFields()
    {

        Debug.Log("Updated: " + Info.username + " " + Info.password + " ."); 
    }
	
	void LoadLevel(){
        Info.Save();
<<<<<<< HEAD
		Application.LoadLevel("MainPage");
=======
<<<<<<< HEAD
		Application.LoadLevel("Test");
=======
		Application.LoadLevel("MainPage");
>>>>>>> e78b432d4b9470554963661966af13292efcd063
>>>>>>> ed45afee976fea1dadc40d564e608d31ebf0fc73
	}
}
