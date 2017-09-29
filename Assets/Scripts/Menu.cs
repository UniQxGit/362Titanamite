using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	public Collider buttonCol;
	private bool opened = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Click()
	{
		if(opened)
			StartCoroutine("Close");
		else
			StartCoroutine("Open");
	}

	IEnumerator Open()
	{
		//9.5
		Vector3 toVector = new Vector3();
		float speed = 5.0f;
		opened = true;
		buttonCol.enabled = false;
		while(transform.position.x > 9.5f)
		{
			transform.position = new Vector3(transform.position.x - Time.deltaTime * speed,transform.position.y,transform.position.z);
			yield return null;
		}
		buttonCol.enabled = true;
		transform.position = new Vector3(9.5f,transform.position.y,transform.position.z);
	}

	IEnumerator Close()
	{
		float speed = 5.0f;
		buttonCol.enabled = false;
		while(transform.position.x < 13.9f)
		{
			transform.position = new Vector3(transform.position.x + Time.deltaTime * speed,transform.position.y,transform.position.z);
			yield return null;
		}
		buttonCol.enabled = true;
		opened = false;
		transform.position = new Vector3(13.9f,transform.position.y,transform.position.z);
	}
}
