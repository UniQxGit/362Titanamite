using UnityEngine;
using System.Collections;

public class CustomButton : UIMan {
	public MonoBehaviour invokingScript;
	public string method = "";
	public float delay = 0;
	public InvisiMaskTest queue;

	public AudioSource sound;

	public Material matActive;
	public Material matNormal;
	public bool isNormal,isActive;
	public Renderer myRen;
	public bool retainColor = false;
	public bool onTouchDown = false;

	public bool forceActive,forceNormal,disableAutoMat;

	public override void Start()
	{
		if(!myRen)
			myRen = gameObject.GetComponent<Renderer>();
		if(queue)
			queue.SetQueue(matNormal);
		if(queue)
			queue.SetQueue(matActive);	

	}

	public override void OnNoTouches()
	{
		if(!forceNormal && matNormal && !isNormal && !disableAutoMat)
		{
			myRen.material = matNormal;
			isNormal = true;
			isActive = false;

		}
	}

	public override void OnTouchBegan()
	{
		beganHere = true;
		if(hitUI.transform.gameObject.name == gameObject.name && !forceActive && matActive && !disableAutoMat)
		{
			myRen.material = matActive;
			isNormal = false;
			isActive = true;

			if(invokingScript && onTouchDown)
				invokingScript.Invoke(method,delay);
		}
	}

	public override void OnTouchMovedAnywhere()
	{
		if(!beganHere)
			return;
		if(hitUI.collider)
		{
			if(hitUI.transform.gameObject != gameObject && !forceNormal && matNormal && !disableAutoMat)
			{
				myRen.material = matNormal;
				isNormal = true;
				isActive = false;
			}
		} 

		if(!didHitUI && !forceNormal && matNormal && !disableAutoMat){
			myRen.material = matNormal;
			isNormal = true;
			isActive = false;
		}
	}

	public override void OnTouchStayedAnywhere()
	{
		if(!didHitUI && !forceNormal && matNormal && !disableAutoMat)
		{
			myRen.material = matNormal;
			isNormal = true;
			isActive = false;
		}
	}

	public override void OnTouchEnded()
	{
		if(!beganHere)
			return;

		//Debug.Log("Detected Click");
		if(hitUI.transform.gameObject.name == gameObject.name)
		{
			if(!forceNormal && matNormal && !disableAutoMat)
			{
				myRen.material = matNormal;
				isNormal = true;
				isActive = false;
			}
			if(invokingScript && !onTouchDown)
				invokingScript.Invoke(method,delay);
			// else
			// 	Debug.Log("No Invoking Script!");
			if(sound && UIMan.SFX)
			{
				// Debug.LogError("UIMan.SFX: " + UIMan.SFX);
				sound.PlayOneShot(sound.clip);
			}
			forceNormal = false;
			forceActive = false;
		}

		beganHere = false;	
	}

	void Test()
	{
		//Debug.Log("Test Successful");
	}

	public void SetControlState(string state)
	{
		Color c = Color.white;
		if(myRen.material.HasProperty("_Color"))
			c = myRen.material.color;
		switch(state)
		{
			case "NORMAL":
				if(!matNormal)
					return;
				forceActive = true;
				myRen.material = matNormal;
				if(retainColor && myRen.material.HasProperty("_Color"))
					myRen.material.color = c;
				break;
			case "ACTIVE":
				if(!matActive)
					return;
				forceNormal = true;
				myRen.material = matActive;
				if(retainColor && myRen.material.HasProperty("_Color"))
					myRen.material.color = c;
				break;
		}
	}

	public void MatAutoSet()
	{
		forceActive = false;
		forceNormal = false;
	}
}
