using UnityEngine;
using System.Collections;

public class CustomTextField : UIMan {
	public TextMesh tMesh;
	public string text = "Text";
	public bool isInput;
	public bool processingInput = false;
	public Vector2 pos;
	public MonoBehaviour callBackScript;
	public string callBackMethod = "";
	public float delay;
	public AudioSource sound;


	public static bool isTutorial = false;

	private Collider myCol;

	private TouchScreenKeyboard keyboard;

	public void Start()
	{
		myCol = GetComponent<Collider>();
		TouchScreenKeyboard.hideInput = true;
		if(text!="Text")
			tMesh.text = text;
	}

	public override void OnTouchBegan()
	{
		beganHere = true;
	}

	public override void OnTouchEnded()
	{
		if(!beganHere)
			return;
		// if(hitUI.collider != myCol)
		// 	return;
		text = tMesh.text;
		#if !UNITY_EDITOR
		if(!isInput)
		{
			if(isTutorial)
			{
				text = "";
				tMesh.text = text;
			}
			//UIMan.disableUIInputGlobal = true;
			keyboard = TouchScreenKeyboard.Open(text,TouchScreenKeyboardType.Default);
			
			isInput = true;
			StartCoroutine("CheckInput");
		}
		#else
			if(sound && UIMan.SFX)
			{
				// Debug.LogError("UIMan.SFX: " + UIMan.SFX);
				sound.PlayOneShot(sound.clip);
			}
			isInput = true;
		#endif


	}

	#if UNITY_EDITOR
	void OnGUI()
	{
		if(touch)
			return;
		//Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		if(!touch && isInput)
		{
			text = GUI.TextField(new Rect(pos.x,pos.y,100,20),text,13);
		}

		if (Event.current.keyCode == KeyCode.Return && !processingInput && text.Length >0 && isInput) {
			ProcessInput();
			isInput = false;
		}
	}
	

	void ProcessInput()
	{
		processingInput = true;
		tMesh.text = text;
		//inputText =string.Empty;
		if(callBackScript)
			callBackScript.Invoke(callBackMethod,delay);
		processingInput = false;
		beganHere = false;

	}
	#else

	IEnumerator CheckInput()
	{
		string prev = keyboard.text;
		processingInput = true;
		if(sound && UIMan.SFX)
		{
			// Debug.LogError("UIMan.SFX: " + UIMan.SFX);
			sound.PlayOneShot(sound.clip);
		}
		while (keyboard.active)
		{
			
			if(keyboard.text.Length<13)
				text = keyboard.text;
			tMesh.text = text;
						
			yield return null;
		}
		
		if(text.Length<1)
			tMesh.text = prev;
		text = tMesh.text;
		isInput = false;
		if(callBackScript)
			callBackScript.Invoke(callBackMethod,delay);
		processingInput = false;
		beganHere = false;
		UIMan.disableUIInputGlobal = false;
	}
	#endif

}
