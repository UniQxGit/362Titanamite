using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMan : MonoBehaviour {

	//public static List<Object> clicked = new List<Object>();
	public static RaycastHit hitUI;
	public bool didHitUI = false;
	public bool disableUIInput = false;

	public static bool disableUIInputGlobal = false;
	public static int currTouch =0;
	//public int uiLayer = 0;
	public LayerMask uiLayer;
	public Vector3 firstTouchPos, curPos;
	public int touch2Watch = 64;
	public bool beganHere;

	public Camera customRaycastCam;
	public bool selectiveActive = false;
	public bool bypass = false;
	public static bool isTutorial = false;
	public static bool selectiveInput = false;
	public static bool mouse,touch,didHitUIGlobal;
	public static UIMan casting;
	public static int activeLayer = -1;
	public static int inputCount = 0;

	private bool disabling = false;

	public static bool BGM = true;
	public static bool SFX = true;
	// Use this for initialization
	public virtual void Start () {
		#if !UNITY_EDITOR
		touch = true;
		// disableUIInputGlobal = false;
		// disableUIInput = false;
		#else
		if(UnityEditor.EditorApplication.isRemoteConnected)
			touch = true;
		else
			mouse = true;
		#endif
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		//Debug.Log("UIMan is Running");
		//touch input
#if !UNITY_EDITOR
		if(Input.touches.Length <=0) //if there are touches
		{
			inputCount = 0;
			if(!selectiveInput)
				selectiveActive = false;
			OnNoTouches();
		}else{
			inputCount = Input.touches.Length;
			if(!selectiveInput)
				selectiveActive = false;
			if(currTouch == 0)
				curPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			curPos.z = transform.position.z;
			if(disableUIInput)
			{
				if(Input.touches[0].phase == TouchPhase.Ended)
					OnTouchEndedAnywhere();
				return;
			}
			if(bypass || (!disableUIInputGlobal) || (selectiveInput && selectiveActive))
			{
				//if((selectiveInput && !selectiveActive))
				//	return;
				if(casting && casting.uiLayer == uiLayer)
				{
					Debug.Log("ActiveLayer is Wrong");
					return;
				}
				
				foreach(Touch touch in Input.touches)
				{
					currTouch = touch.fingerId;
					//if(currTouch ==0)

					Ray ray = customRaycastCam?customRaycastCam.ScreenPointToRay(touch.position):Camera.main.ScreenPointToRay(touch.position);
					// Raycast to check if there are hits.
					if(Physics.Raycast(ray,out hitUI,Mathf.Infinity,uiLayer))
					{
						casting = this;
						if(hitUI.transform.gameObject == gameObject)
						{

							didHitUI = true;
							didHitUIGlobal=true;
							if(touch.phase == TouchPhase.Began)
							{
								// firstTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
								// if(hitUI.transform.gameObject.name == "List")
								// 	Debug.Log("TouchBegan!");
								OnTouchBegan();
								touch2Watch = currTouch;
							}
							if(touch.phase == TouchPhase.Ended)
							{
								OnTouchEnded();
							}
							if(touch.phase == TouchPhase.Moved)
							{
								// if(hitUI.transform.gameObject.name == "List")
								// 	Debug.Log("TouchMoved!!");
								OnTouchMoved();
							}
							if(touch.phase == TouchPhase.Stationary)
							{
								OnTouchStayed();
							}
						}
					}else{
						didHitUI = false;
						didHitUIGlobal = false;
					}
					if(!isTutorial || (selectiveInput && selectiveActive))
					{
						switch(touch.phase)
						{
							case TouchPhase.Began:
								OnTouchBeganAnywhere();
								break;
							case TouchPhase.Ended:
								OnTouchEndedAnywhere();
								break;
							case TouchPhase.Moved:
								OnTouchMovedAnywhere();
								break;
							case TouchPhase.Stationary:
								OnTouchStayedAnywhere();
								break;
						}
					}
					

				}
				casting = null;
			}
			else{
				// if(gameObject.name == "List_Debug")
				// {
				// 	if(disableUIInputGlobal)
				// 		Debug.Log("Global Disabled!");
				// 	if(disableUIInput)
				// 		Debug.Log("Local Disabled");
				// 	if((selectiveInput && !selectiveActive))
				// 		Debug.Log("Not Selective Active!");
				// }
				// if(!disabling)
				// {
				// 	Debug.Log("Input Disabled by " + gameObject.name);
				// 	disabling = true;
				// }else{
				// 	Debug.Log("Input Re-enabled by " + gameObject.name);
				// 	disabling = false;
				// }
			}
			
		}
#else
		// if(disableUIInputGlobal)
		// {
		// 	Debug.Log("UIINput Disabled GLobal"+ gameObject.name);
		// }if(disableUIInput)
		// 	Debug.Log("UIINput Disabled" + gameObject.name);
		if(!selectiveInput)
			selectiveActive = false;
		// if(disableUIInputGlobal)
		// 	Debug.Log("InputDisabledGlobally!");
		curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		curPos.z = transform.position.z;
		if(disableUIInput)
		{
			if(Input.GetMouseButtonUp(0))
				OnTouchEndedAnywhere();
			return;
		}
		if(bypass || (!disableUIInputGlobal) || (selectiveInput && selectiveActive))
		{
			if(casting && casting.uiLayer == uiLayer)
				return;
			
			Ray ray = customRaycastCam?customRaycastCam.ScreenPointToRay(Input.mousePosition):Camera.main.ScreenPointToRay(Input.mousePosition);		
			//Debug.Log("MouseDown");
			// Raycast to check if there are hits.
			if(Physics.Raycast(ray,out hitUI,Mathf.Infinity,uiLayer))
			{
				casting = this;
				if(hitUI.transform.gameObject == gameObject)
				{
					
					//Debug.Log("HitSomething");
					didHitUI = true;

					didHitUIGlobal = true;
					//Debug.Log("HitUIGlobal:" + didHitUIGlobal);
					if(Input.GetMouseButtonDown(0))
					{
						OnTouchBegan();						
						firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						touch2Watch = currTouch;
					}
					if(Input.GetMouseButtonUp(0))
					{
						OnTouchEnded();
					}
					if(Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
					{
						OnTouchMoved();
					}
					if(Input.GetMouseButton(0) && Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
					{
						OnTouchStayed();
					}
				}
			}else{
				didHitUI = false;
				didHitUIGlobal = false;
				
			}

			if(!isTutorial || (selectiveInput && selectiveActive))
			{
				if(Input.GetMouseButtonDown(0))
				{
					inputCount = 1;
					OnTouchBeganAnywhere();
				}
				if(Input.GetMouseButtonUp(0))
				{
					inputCount = 0;
					OnTouchEndedAnywhere();
				}
				if(Input.GetMouseButton(0))
					OnTouchMovedAnywhere();
			}

				
				//OnTouchStayedAnywhere();
			casting = null;
		}

#endif
	}

	//the default functions, define what will happen if the child doesn't override these functions
	public virtual void OnNoTouches(){}
	public virtual void OnTouchBegan(){}
	public virtual void OnTouchEnded(){}
	public virtual void OnTouchMoved(){}
	public virtual void OnTouchStayed(){}
	public virtual void OnTouchBeganAnywhere(){}
	public virtual void OnTouchEndedAnywhere(){}
	public virtual void OnTouchMovedAnywhere(){}
	public virtual void OnTouchStayedAnywhere(){}
	public virtual void DisableElse(){
		selectiveInput = true;
		selectiveActive = true;
		disableUIInputGlobal = true;
	}
	public virtual void EnableElse(){
		selectiveInput = false;
		disableUIInputGlobal=false;
	}
}
