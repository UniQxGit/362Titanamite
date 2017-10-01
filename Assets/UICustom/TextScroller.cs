using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (TextMesh))]
public class TextScroller : MonoBehaviour {
	public float scrollSpeed = 1;
	private Vector3 initialPos;
	private float oStartPosx;
	private float oEndPosx;
	private TextMesh t;
	private string lastText;

	// Use this for initialization
	void Start () {
		t = GetComponent<TextMesh>();
		initialPos = transform.position;
		oStartPosx = transform.position.x;
		oEndPosx = oStartPosx + GetWidth(t);
		transform.position = new Vector3(oStartPosx+GetWidth(t),transform.position.y,transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x + GetWidth(t) > oStartPosx && t.text != "")
		{
			transform.position += Vector3.left * Time.deltaTime * scrollSpeed;
			if(t.text != lastText)
				transform.position = new Vector3(oStartPosx+GetWidth(t),transform.position.y,transform.position.z);
		}else{
			transform.position = new Vector3(oStartPosx+GetWidth(t),transform.position.y,transform.position.z);
		}
		lastText = t.text;
	}

	public void SetText(string s)
	{
		t.text = s;
		transform.position = new Vector3(oStartPosx+GetWidth(t),transform.position.y,transform.position.z);
		lastText = s;
	}

	public static float GetWidth(TextMesh mesh)
	{
	 float width = 0;
	 foreach (char symbol in mesh.text)
	 {
	     CharacterInfo info;
	     if (mesh.font.GetCharacterInfo(symbol, out info, mesh.fontSize, mesh.fontStyle))
	     {
	         width += info.advance;
	     }
	 }
	 return width * mesh.characterSize * 0.1f;
	}
}
