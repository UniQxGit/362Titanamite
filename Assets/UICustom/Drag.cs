using UnityEngine;
using System.Collections;

public class Drag : UIMan {
	public float triggerDelay = 1;
	public float triggerTime;
	public Transform dragger;
	public bool isTriggered, isLocked, fadeOnTrigger, hasTarget,disabled,switching;
	public Renderer myRen,disableArt;
	public BoxCollider myCol;
	public Vector3 oPos;
	public Vector3 startPos;
	public int slotNum;
	public AudioSource sound_err;

	public static Drag dragging;

	[HideInInspector]
	public Vector3 ocSize, ocSize1;
	public override void Start()
	{
		triggerDelay = 0.15f;
		base.Start();
		if(!dragger)
			dragger = transform;
		ocSize = myCol.size;
		//startPos = transform.position;
	}	

	public override void OnTouchBegan()
	{
		triggerTime = Time.time + triggerDelay;
		oPos = transform.position;
		
	}

	public override void OnTouchMovedAnywhere()
	{
		if(isTriggered && !disabled)
		{
			Debug.Log("Moved");
			Vector3 tempV = curPos - transform.position;
			float moveSpeed = Time.deltaTime * (Mathf.Abs(tempV.x) + Mathf.Abs(tempV.y)) * 10.0f; 
			transform.position += tempV.normalized * moveSpeed;
		}
	}

	public override void OnTouchStayed()
	{

		if(Time.time>triggerTime && !isTriggered && !dragging)
		{
			Debug.Log("Triggering");
			DisableElse();
			startPos = disableArt.transform.position;
			if(!disabled)
			{
				if(!hasTarget)
				{
					disableArt.enabled = false;
					//startPos = transform.position;
				}
				if(isLocked)
				{
					dragger.parent = transform;
				}
				if(fadeOnTrigger)
					myRen.material.color = new Color(myRen.material.color.r,myRen.material.color.g,myRen.material.color.b,.5f);
				myRen.enabled = true;
				myCol.size = new Vector3(ocSize.x,ocSize.y,ocSize.z*.2f);
			}else
				sound_err.Play();
			AfterTrigger();
			isTriggered = true;
			dragging = this;
			
			Debug.Log("TRIGGERED DRAG!!");
		}
	}

	public override void OnTouchEndedAnywhere()
	{
		if(fadeOnTrigger)
			myRen.material.color = new Color(myRen.material.color.r,myRen.material.color.g,myRen.material.color.b,1.0f);

		disableArt.enabled = true;
		if(isTriggered)
		{	
			if(!hasTarget)
			{
				myRen.enabled = false;
				transform.position = startPos;
			}else{
				isLocked = true;
				Debug.Log(disableArt.gameObject.name + " oPos:" + oPos);
				transform.position = oPos;
			}
			isTriggered = false;
		}
		myCol.size = new Vector3(ocSize.x,ocSize.y,ocSize.z);
		dragging = null;
		EnableElse();
	}

	public void Reset()
	{
		transform.position = startPos;
		// while(myRen.material.color.a<1.0f) //change return type back to IENumerator if using this code
		// {
		// 	myRen.material.color += new Color(0,0,0,0.1f);
		// 	yield return null;
		// }
		myRen.material.color = new Color(myRen.material.color.r,myRen.material.color.g,myRen.material.color.b,1.0f);
		myRen.enabled = false;
		isLocked = false;
		isTriggered = false;
		myCol.enabled = true;
		disableUIInputGlobal = false;

	}	

	public virtual void AfterTrigger(){}	
}
