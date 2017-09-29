using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPage : MonoBehaviour {
	public string url = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Open()
	{
		Debug.Log("Clicked");
		Application.OpenURL(url);
	}
}
