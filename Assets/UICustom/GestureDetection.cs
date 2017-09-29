using UnityEngine;
using System.Collections;

public class GestureDetection : UIMan {
	
	public MonoBehaviour invokingScript;
	
	public string 	method_swipe_up2 = "";
	
	public float 	max_swipe_dist = 300;
	public float 	min_swipe_dist = 50;
	public float	swipe_timeout  = 1;
	
	private bool 	is_swipe = false;

	private float	start_time;
	private Vector3 start_pos1;
	private Vector3 start_pos2;

	private Vector3 last_pos1;
	private Vector3 last_pos2;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		if(Application.platform != RuntimePlatform.Android && 
			Application.platform != RuntimePlatform.IPhonePlayer )
		this.enabled = false;

		last_pos1 = -Vector3.one;
		last_pos2 = -Vector3.one;
	}
	
	public override void OnTouchBeganAnywhere()
	{
		if(Input.touchCount!=2)
			return;

		//Debug.Log("Started Debug Began");
		is_swipe = true;
		start_time = Time.time;
		start_pos1 = Input.touches[0].position;
		start_pos2 = Input.touches[1].position;	
	}

	public override void OnTouchMovedAnywhere()
	{
		if(Input.touchCount!=2)
			return;
		//Debug.Log("Started Debug Moved");

		last_pos1 = Input.touches[0].position;
		last_pos2 = Input.touches[1].position;

		// if (Mathf.Abs((curPos.y-start_pos1.y) + (Input.touches[1].position.y-start_pos2.y)) > max_swipe_dist*2)
		// 	is_swipe = false;
	}

	public override void OnTouchStayedAnywhere()
	{
		if(Input.touchCount!=2)
			return;
		//Debug.Log("Started Debug Stayed");
		//is_swipe = false;
	}

	public override void OnTouchEndedAnywhere()
	{
		// if(Input.touchCount!=2)
		// 	return;
		//Debug.Log("Started Debug Ended");
		//Debug.Log("Touches: " + Input.touchCount);
		float swipe_time = Time.time - start_time;
		float swipe_dist1 = (curPos - start_pos1).magnitude;

		if(is_swipe && (swipe_time < swipe_timeout) && (swipe_dist1 > min_swipe_dist))
		{
			
			float swipe_dir1 = Mathf.Sign(last_pos1.y - start_pos1.y);
			float swipe_dir2 = Mathf.Sign(last_pos2.y - start_pos1.y);

			if(swipe_dir1+swipe_dir2 > 0 && last_pos2.y > start_pos2.y)
			{
				if(invokingScript)
					invokingScript.Invoke(method_swipe_up2,0);
				else
					Debug.Log("No Invoking Script!");
			}

		//	Debug.Log("Input Cleared");
			last_pos1 = -Vector3.one;
			last_pos2 = -Vector3.one;	
		}

		if(Input.touchCount == 0)
		{
			//Debug.Log("Input Cleared");
			last_pos1 = -Vector3.one;
			last_pos2 = -Vector3.one;			
		}

	}

}
