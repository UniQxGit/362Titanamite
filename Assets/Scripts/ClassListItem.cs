using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassListItem : MonoBehaviour {
    public Renderer tagRen;
    public TextMesh nameText;
    public TextMesh timeText;
    public TextMesh roomText;


    public Color tagColor;
    public string name;
    public string time;
    public string room;

	// Use this for initialization
	void Start () {
        tagRen.material.color = tagColor;
        nameText.text = name;
        timeText.text = time;
        roomText.text = room;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
