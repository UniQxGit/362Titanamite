using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedulePage : MonoBehaviour {

    public Renderer bg;

	// Use this for initialization
	void Start () {
        StartCoroutine("TransitionIn");
	}

    IEnumerator TransitionIn()
    {
        float acc = 1.0f;
        Color c = new Color();
        while (acc > 0.0f)
        {
            c = bg.material.color;
            c.a = acc/1.0f;
            bg.material.color = c;
            acc -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TransitionOut()
    {
        float acc = 0.0f;
        Color c = new Color();
        while (acc < 1.0f)
        {
            c = bg.material.color;
            c.a = acc / 1.0f;
            bg.material.color = c;
            acc += Time.deltaTime;
            yield return null;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
