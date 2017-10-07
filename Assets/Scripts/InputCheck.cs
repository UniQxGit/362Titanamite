using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCheck : MonoBehaviour {
	public InputField field;
	public TextMesh t;
	// Use this for initialization
	void Start () {
		
	}

	void CheckTime()
	{
		t.text = field.text;
	}

	void CheckTimeFinished()
	{
		int time = int.Parse(field.text);
		if(time>1200)
		{
			Debug.Log(time/100);
			field.text = (time - (time/100-12)*100).ToString();
		}
		if(field.text.Length==3)
			t.text = field.text.Insert(1,":");
		else if(field.text.Length==4)
			t.text = field.text.Insert(2,":");
		else if(field.text.Length>0)
			t.text = field.text.Insert(field.text.Length,":00");
		else
			t.text = "";
		Debug.Log(t.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
