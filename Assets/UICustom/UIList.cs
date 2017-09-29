using UnityEngine;
using System.Collections;

public class UIList : UIMan {
	public Transform container;
	public Renderer upArrow,downArrow;
	public bool lockX,lockY,yIsZ, autoColDisable,disableFollow;
	public Collider myCol;
	public float maxLeft, maxRight,maxUp,maxDown;
	public float scrollSpeed = 1;
	[HideInInspector]
	public bool isAutoScrolling,isOverScrolling;
	public bool isScrolling;
	public bool autoInput;
	public float customTouchUpAction = -1;
	//public Vector2 mag;

	public string status = "";
	
	private int maxed = 0;
	public Vector3 delta,lastDelta;
	public bool anchored;
	public int anchorCount;
	public float top;
	private Vector2 axis = new Vector2(0,0);
	private Vector2 hitPos;
	private Vector3 startPos,endPos;
	private float dt,startTime,stopThreshold = 0.01f, resistanceFactor = 0.98f;
	private Vector3 lastPos;
	private int overDirection = 0;
	private float stayedDuration  =0.0f;
	private bool firstScroll = false;




	//Debugging
	public float showDebug;

	public void Start()
	{
		myCol.enabled = true;
		lastPos = Vector3.zero;
		axis = Vector2.zero;
		base.Start();
		if(upArrow && downArrow)
			StartCoroutine("ShowArrows");
				//StartCoroutine("ScrollToPosition",1.0);
	}

	public void OnEnable()
	{
		if(upArrow && downArrow)
			StartCoroutine("ShowArrows");
	}

	public override void OnTouchBegan()
	{
		if(currTouch !=0)
			return;
		// if(isAutoScrolling)
		// 	return;
		//GameGUI.msg("<color=blue>MessageTest</color>");
		if(hitUI.transform.gameObject == gameObject && !beganHere)
			startPos = hitUI.point;
		if(touch)
		{
			if(!lockY)
				axis.y = Input.GetTouch(0).deltaPosition.magnitude/Input.GetTouch(0).deltaTime;
			if(!lockX)
				axis.x = axis.y;
		}else{
			if(!lockX)
				axis.x = Input.GetAxis("Mouse X");
			if(!lockY)
				axis.y = Input.GetAxis("Mouse Y");
		}
		
		OnTouchStayed();
		
		//if(!beganHere)
			beganHere = true;

		if(!isScrolling && beganHere)
		{	
			
			lastPos = startPos;
			startTime = Time.time;
			//isScrolling = true;
		}
		

		curPos = hitUI.point;
		lastPos = hitUI.point;
	}

	public override void OnTouchStayed()
	{
		if(currTouch !=0 || anchored)
			return;
		
		Debug.Log("LISTSTAYED");
		stayedDuration += Time.deltaTime;
		StopCoroutine("ScrollToPosition");
		//StopCoroutine("OverScroll");

		//isScrolling = false;
		//isOverScrolling = false;
		//isAutoScrolling = false;
		disableUIInput = false;
			//isScrolling = false;
	}

	public override void OnTouchMovedAnywhere()
	{
		if(!container || currTouch !=0 || (disableFollow && isAutoScrolling) || isTutorial || disableUIInput)
			return;

		if(touch)
		{
			axis.y = Input.GetTouch(0).deltaPosition.magnitude/ Input.GetTouch(0).deltaTime;
			axis.x = axis.y;
			dt = Time.deltaTime;

		}else{
			//Debug.Log("Moving. Hit " + hitUI.collider.name);
			//Debug.Log("Got Axis!");
			dt = Time.deltaTime;
			axis.x = Input.GetAxis("Mouse X");
			axis.y = Input.GetAxis("Mouse Y");

		}

		if(!lockX)
		{
			if(container.localPosition.x >= (maxRight+0.05f))
			{
				maxed = 1;
			}else if(container.localPosition.x <= (maxLeft-0.05f))
			{
				maxed = -1;
			}else
			{
				maxed =0;
			}
		}

		if(!lockY)
		{
			if(yIsZ)
			{
				if(container.localPosition.z <= (maxDown-0.05f))
				{
					maxed = -1;
				}else if(container.localPosition.z >= (maxUp+0.05f))
				{
					maxed = 1;
				}else
				{
					maxed =0;
				}
			}
			else{
				if(container.localPosition.y <= (maxDown-0.05f))
				{
					maxed = -1;
				}else if(container.localPosition.y >= (maxUp+0.05f))
				{
					maxed = 1;
				}else
				{
					maxed =0;
				}
			}
			
		}

		if(myCol.enabled)
		{
			if(hitUI.collider)
			{
				if(hitUI.transform.gameObject == gameObject && !beganHere && !firstScroll)
				{
					beganHere = true;
					firstScroll = true;
					startPos = hitUI.point; 
					lastPos = startPos;
					//lastPos = hitUI.point;
				}
			}
			
				
			delta = (curPos-lastPos);
			// if(Mathf.Abs(delta.y)<0.01 && Mathf.Abs(delta.y)>0)
			// {
			// 	delta.y = 0;
			// }

			if(beganHere && (!lockY && (Mathf.RoundToInt(Mathf.Abs(delta.y)*100)>0) || (!lockX && (Mathf.RoundToInt(Mathf.Abs(delta.x)*100)>0)) ) && !selectiveInput)
			{
				//Debug.Log(gameObject.name +"delta:" + delta.y + "cP" + curPos.y + "lP" + lastPos.y + "diff" + (curPos.y-lastPos.y));
				
				if(!isTutorial)
				{
					DisableElse();
				}
			}

			if(anchored && (!lockY && delta.y != 0 && Mathf.Abs(hitUI.point.y-startPos.y)>0.03f))
			{
				OnTouchStayed();
				isAutoScrolling = false;
				StopCoroutine("ScrollToPosition");
				StartCoroutine("CustomScroll");
			}
			else if(
				((!lockY && Mathf.Abs(delta.y)>0.01f && delta.y != 0 && Mathf.Abs(hitUI.point.y-startPos.y)>0.03f) || (!lockX && delta.x !=0))
				&& beganHere && ((selectiveInput && !isScrolling) || isScrolling) && !disableFollow
				)
				{
				if(!isScrolling)
					isScrolling = true;	
				stayedDuration = 0.0f;
				if(!lockY)
				{
					container.position += new Vector3(0,delta.y,0);
					if(yIsZ)
					{
						container.localPosition = new Vector3(container.localPosition.x,
							container.localPosition.y,
							Mathf.Clamp(container.localPosition.z,maxDown,maxUp));	
					}
					else
					{
						container.localPosition = new Vector3(container.localPosition.x,
							Mathf.Clamp(container.localPosition.y,maxDown,maxUp),
							container.localPosition.z);	
					}
				}
				if(!lockX)
				{
					container.position += new Vector3(delta.x,0,0);
		       		container.localPosition = new Vector3(Mathf.Clamp(container.localPosition.x,maxLeft,maxRight),
					container.localPosition.y,
					container.localPosition.z);
				}

	       		
	       		status = "Touched and Scrolling:" + delta.y + " cPos:" + container.position.y;
			}else{

				status = "delta.x:" + delta.x + " delta.y:" + delta.y;
			}
			lastPos = curPos;
			if(Mathf.Abs(delta.y)>0.001f)
	       		lastDelta = delta;

		}
	}

	public override void OnTouchEndedAnywhere()
	{	

		if(isAutoScrolling || !container || currTouch !=0 || disableUIInput)
		{
			status = "AutoScrolling";
			return;
		}

		if(isScrolling && !autoInput)
		{
			isScrolling = false;
			endPos = hitUI.point;
			Debug.Log("TouchEndPositions: " + endPos.y + "|" + lastPos.y + "=" +Mathf.Abs(endPos.y-lastPos.y) + "\n" + stayedDuration);
			if(beganHere && (Mathf.Abs(lastDelta.y)>0) && stayedDuration<0.05f) //((!lockY&&Mathf.Abs(hitUI.point.y-startPos.y)>0.2f)||(!lockX && Mathf.Abs(hitUI.point.x-startPos.x)>0.2f))&& 
			{
				if(customTouchUpAction>=0)
				{
					StartCoroutine("CustomScroll");
					Debug.Log("Custom Scroll:" + customTouchUpAction);

				}else if(!disableFollow){
					isOverScrolling = true;
					StartCoroutine("OverScroll",hitUI.point);
					Debug.Log("Custom Scroll:" + customTouchUpAction);
				}
			}
			else
			{
				Debug.Log("Distance: " + Mathf.Abs(hitUI.point.y-startPos.y) + "DeltaY:"+Mathf.Abs(delta.y));
				if(customTouchUpAction>=0)
				{
					StartCoroutine("CustomScroll");
					Debug.Log("Custom Scroll:" + customTouchUpAction);
				}else{
					Debug.Log("Custom Scroll:" + customTouchUpAction);
					Debug.Log("customTOuchUpAction");
				}

			}
			if(!isOverScrolling)
				StartCoroutine("EnableAfter1Frame");
		}else{
			StartCoroutine("EnableAfter1Frame");
		}
		//else{

		endPos = hitUI.point;
		beganHere = false;
		firstScroll = false;
		delta = Vector3.zero;
		lastPos = delta;
		stayedDuration = 0.0f;

	}

	public IEnumerator OverScroll(Vector3 v)
	{
		isOverScrolling = true;
		endPos = v;
		v = (v - startPos)/(Time.time - startTime) * Time.deltaTime;
		Debug.Log("End:" + v + "\nStart:" + startPos+ "\nDistance:" + (v - startPos).y + "\nDDistance:" +v.y + "\nDMagnitude:" +v.magnitude);

		if(v.y >0 || v.x>0)
			overDirection = 1;
		else 
			overDirection = -1;

		yield return null;
		while(v.magnitude > stopThreshold && maxed == 0 && isOverScrolling && !beganHere)//
		{
			if(!lockX)
			{
				if(container.localPosition.x >= (maxRight))
				{
					maxed = 1;
					Debug.Log("Maxed" + maxed);
					
				}else if(container.localPosition.x <= (maxLeft))
				{
					maxed = -1;
					Debug.Log("Maxed" + maxed);
				}else
				{
					maxed =0;
				}
			}
			if(!lockY)
			{
				if(container.localPosition.y <= (maxDown))
				{
					maxed = -1;
				}else if(container.localPosition.y >= (maxUp))
				{
					maxed = 1;
				}else
				{
					maxed =0;
				}
			}

			if(!lockY)
			{
				container.localPosition += new Vector3(0,v.y,0);

				if(yIsZ)
				{
					container.localPosition = new Vector3(container.localPosition.x,
						container.localPosition.y,
						Mathf.Clamp(container.localPosition.z,maxDown,maxUp));	
				}
				else
				{
					container.localPosition = new Vector3(container.localPosition.x,
						Mathf.Clamp(container.localPosition.y,maxDown,maxUp),
						container.localPosition.z);	
				}
			}

			if(!lockX)
			{
				container.localPosition += new Vector3(v.x,0,0);
	       		container.localPosition = new Vector3(Mathf.Clamp(container.localPosition.x,maxLeft,maxRight),
				container.localPosition.y,
				container.localPosition.z);
			}

			//Debug.Log(v.y);
			v = v*resistanceFactor;
			yield return null;
		}
		isOverScrolling = false;
		Debug.Log("Done Overscroll");
		if(customTouchUpAction>=0)
		{
			StartCoroutine("CustomScroll");
			Debug.Log("Custom Scroll:" + customTouchUpAction);
			
		}
		StartCoroutine("EnableAfter1Frame");
	}
	
	// NOT PORTABLE
	public void CustomScroll() 
	{
		if(anchored)
		{
			int i;
			float customDelta = delta.y;
			
			float tmp = GetScrollPosY();
			disableFollow = true;
			// Debug.Log("EQUIP ENDING DELTA:" + tmp);
			// Debug.Log("endPos(" + endPos.y +")-startPos(" + startPos.y + ") =" + customDelta  + "Delta: "+ delta);
			// Debug.Log("Position:" + GetCoordinatesAt(tmp).y);
			
			if(anchorCount == 3)
			{
				if(tmp>=.4 && tmp<=0.6)
				{
					if(customDelta<0)
					{
						customTouchUpAction = 0.0f;
					}else 
					if(customDelta>0){
						customTouchUpAction = 1.0f;
					}else{
						customTouchUpAction = 0.5f;
					}
				}
				else if(tmp>.6)
				{
					if(customDelta<-1)
					{
						customTouchUpAction = 0.0f;
					}else if(customDelta<0){
						customTouchUpAction = 0.5f;
					}else{
						customTouchUpAction = 1.0f;
					}
				}
				else if(tmp<.4)
				{
					if(customDelta>0){
						customTouchUpAction = 0.5f;
					}else{
						customTouchUpAction = 0.0f;
					}
				}
			}else if(anchorCount == 2){
				if(tmp>=.5)
				{
					if(customDelta<-1)
					{
						customTouchUpAction = 0.0f;
					}else{
						customTouchUpAction = 1.0f;
					}
				}
				else if(tmp<.5)
				{
					if(customDelta>0){
						customTouchUpAction = 1.0f;
					}else{
						customTouchUpAction = 0.0f;
					}
				}
			}
			
			if(customTouchUpAction<0)
				customTouchUpAction = top;
			StartCoroutine("ScrollToPosition",customTouchUpAction);
		}else{
			Debug.Log("No Anchors");
			StartCoroutine("ScrollToPosition",customTouchUpAction);
			customTouchUpAction = -1;
		}		
	}
	//END

	public void EnableAfter1Frame()
	{
		if(!isTutorial)
			EnableElse();
		if(!isOverScrolling)
			beganHere = false;
		firstScroll = false;
		//yield return null;
	}

	public IEnumerator ScrollToPosition(float x)
	{
		if(x<0.0f || x>1.0f)
		{
			Debug.LogError(x + ": Invalid Range.. Only values 0-1 are allowed: ");
			isAutoScrolling = false;
			disableUIInput = false;
		}else{
			if(disableFollow)
				disableUIInput = true;
			isAutoScrolling = true;
			bool conditionX = true,conditionY=true;
			Vector2 pos = new Vector2(maxLeft + (x*(maxRight-maxLeft)),maxDown + (x * (maxUp - maxDown)));
			Vector2 direction = new Vector2(1,1);
			delta = Vector3.zero;


			if(!lockY)
			{
				if(pos.y<maxDown )
				{
					Debug.LogError("Error in Calculations!" + pos.y + " MaxUp:" + maxUp + " MaxDown:" + maxDown);
					pos.y = maxDown;
				}else if(pos.y>maxUp)
				{
					Debug.LogError("Error in Calculations!" + pos.y + " MaxUp:" + maxUp + " MaxDown:" + maxDown);
					pos.y = maxUp;
				}
			}else
				pos.y = 0;
			
			if(!lockX)
			{
				if(pos.x<maxLeft)
				{
					Debug.LogError("Error in Calculations!" + pos.y + " MaxUp:" + maxUp + " MaxDown:" + maxDown);
					pos.x = maxLeft;
				}else if(pos.x>maxRight)
				{
					Debug.LogError("Error in Calculations!" + pos.y + " MaxUp:" + maxUp + " MaxDown:" + maxDown);
					pos.x =maxRight;
				}
			}else
				pos.x = 0;

			if(pos.y >0 || pos.x>0)
				overDirection = 1;
			else
				overDirection = -1;

			

			if(lockX)
			{
				direction.x = 0;
				conditionX = false;
			}
			else
			{
				if(container.localPosition.x<=pos.x - .055f )
				{
					direction.x = 1 * Time.deltaTime * (scrollSpeed*2.5f);
					while(container.localPosition.x<=(pos.x - .055f) && beganHere)
					{
						container.localPosition += new Vector3(direction.x,0,0);
						yield return null;
					}
					container.localPosition = new Vector3(pos.x,container.localPosition.y,container.localPosition.z);
				}
				//else if(container.localPosition.x > pos.x+ .05f)
				else if(container.localPosition.x>=pos.x + .05f )
				{
					direction.x = -1 * Time.deltaTime * (scrollSpeed*2.5f);
					while(container.localPosition.x>=pos.x+ .05f && beganHere)
					{
						container.localPosition += new Vector3(direction.x,0,0);
						yield return null;
					}
					container.localPosition = new Vector3(pos.x,container.localPosition.y,container.localPosition.z);
				}
			}

			if(lockY)
			{
				direction.y = 0;
				conditionY = false;
			}
			else{
				if((yIsZ?container.localPosition.z:container.localPosition.y)<=pos.y)
				{
					direction.y = 1 * Time.deltaTime * (scrollSpeed*2.5f);
					if(yIsZ)
					{
						while(container.localPosition.z + direction.y<=pos.y&& beganHere)
						{
							container.localPosition += new Vector3(0,0,direction.y);
							yield return null;
						}
						container.localPosition = new Vector3(container.localPosition.x,container.localPosition.z,pos.y);
					}else{
						while(container.localPosition.y + direction.y<=pos.y&& beganHere)
						{
							container.localPosition += new Vector3(0,direction.y,0);
							yield return null;
						}
						container.localPosition = new Vector3(container.localPosition.x,pos.y,container.localPosition.z);	
					}
				}
				else if((yIsZ?container.localPosition.z:container.localPosition.y)>=pos.y)
				{
					direction.y = -1 * Time.deltaTime * (scrollSpeed*2.5f);
					if(yIsZ)
					{
						while(container.localPosition.z + direction.y>=pos.y&& beganHere)
						{
							container.localPosition += new Vector3(0,0,direction.y);
							yield return null;
						}
						container.localPosition = new Vector3(container.localPosition.x,container.localPosition.z,pos.y);
					}else{
						while(container.localPosition.y + direction.y>=pos.y&& beganHere)
						{
							container.localPosition += new Vector3(0,direction.y,0);
							yield return null;
						}
						container.localPosition = new Vector3(container.localPosition.x,pos.y,container.localPosition.z);	
					}
				}
			}



			isAutoScrolling = false;
			if(disableFollow)
			{
				disableUIInput = false;
				if(anchored)
					disableFollow = false;
			}
		}

		if(selectiveInput)
			StartCoroutine("EnableAfter1Frame");
		
		
	}

	public IEnumerator ScrollToPosition(Vector2 v)
	{
		if(disableFollow)
			disableUIInput = true;

		Vector3 direction = new Vector3(0,0,0);
		if(!lockX)
		{
			if(container.localPosition.x<v.x)
			{
				direction.y = 1 * Time.deltaTime * (scrollSpeed*2.5f);
				while(container.localPosition.y<v.y)
				{
					container.localPosition += direction;
					yield return null;
				}
				container.localPosition = new Vector3(container.localPosition.x,v.x,container.localPosition.z);
			}else{
				direction.y = -1 * Time.deltaTime * (scrollSpeed*2.5f);
				while(container.localPosition.x<v.y)
				{
					container.localPosition += direction;
					yield return null;
				}
				container.localPosition = new Vector3(container.localPosition.x,v.x,container.localPosition.z);
			}
		}
		if(!lockY)
		{
			direction.x = 0;
			if(container.localPosition.y<v.y)
			{
				direction.y = 1 * Time.deltaTime * (scrollSpeed*2.5f);
				while(container.localPosition.y<v.y)
				{
					container.localPosition += direction;
					yield return null;
				}
				container.localPosition = new Vector3(container.localPosition.x,v.y,container.localPosition.z);
			}else{
				direction.y = -1 * Time.deltaTime * (scrollSpeed*2.5f);
				while(container.localPosition.y>v.y)
				{
					container.localPosition += direction;
					yield return null;
				}
				container.localPosition = new Vector3(container.localPosition.x,v.y,container.localPosition.z);
			}
		}

		if(selectiveInput)
			StartCoroutine("EnableAfter1Frame");
		if(disableFollow)
		{
			disableUIInput = false;
		}
		
	}

	public IEnumerator ShowArrows()
	{
		while(true)
		{
			showDebug = GetScrollPosY();
			if(disableUIInput)
			{
				upArrow.enabled = false;
				downArrow.enabled = false;	
			}
			else if(GetScrollPosY()<.1)
			{
				upArrow.enabled = false;
				downArrow.enabled = true;
			}else if(GetScrollPosY()>.9)
			{
				upArrow.enabled = true;
				downArrow.enabled = false;
			}else{
				upArrow.enabled = true;
				downArrow.enabled = true;
			}
			yield return null;
		}
	}

	public Vector3 GetCoordinatesAt(float x)
	{
		return new Vector3(maxLeft + (x*(maxRight-maxLeft)),maxDown + (x * (maxUp - maxDown)),container.position.z);
	}

	public Vector2 CoordToPct(Vector3 v)
	{
		return new Vector2(Mathf.Abs(v.x-maxLeft/maxRight-maxLeft),Mathf.Abs(v.y-maxDown/maxUp-maxDown));
	}

	public float GetScrollPosY()
	{
		return 1.0f-((Mathf.Abs(maxUp - (yIsZ?container.localPosition.z:container.localPosition.y) )/(maxUp - maxDown)));
	}

	public float GetScrollPosX()
	{
		return 1.0f-((Mathf.Abs(maxRight - container.position.x))/(maxRight - maxLeft));
	}
}
