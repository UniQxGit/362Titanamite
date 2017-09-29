using UnityEngine;
using System.Collections;

public class CustomAnimations : MonoBehaviour {
	public delegate void Callback();

	public static int playcount =0;

	public static bool stop;

	private static float smoothness = 100;

	public static void Stop()
	{
		stop = true;
		// yield return new WaitForSeconds(1.0f);
		// stop = false;
	}

	public static IEnumerator ColorLerp(Renderer ren,Color a, Color b,float t,int smooth, Callback callback)
	{
		Color aa = new Color(),ab = new Color(); //aa is cache of original color. ab will lerp to next color.
		if(ren.material.HasProperty("_OutlineColor"))
		{
			aa = ren.material.GetColor("_OutlineColor");
			ab = aa;
		}
		// float endTime = Time.time + t;
		// float nextChange = Time.time + ((t*1.0f)/(smooth*1.0f));

		// int changeCount = 1;
		
		//smooth = 100;
		playcount++;
		ren.material.color = a;
		

		// while(Time.time<endTime)
		// {
		// 	if(Time.time > nextChange)
		// 	{
		// 		nextChange = Time.time + (((t*1.0f)/(smooth*1.0f)) * changeCount);
		// 		ren.material.color += (b-a)/smooth*1.0f;
		// 		changeCount++;
		// 	}
		// 	yield return null;
		// }
		for(int i =0;i<smooth;i++)
		{
			ren.material.color += (b-a)/smooth*1.0f; 
			if(ren.material.HasProperty("_OutlineColor"))
			{
				ab += (b-aa)/smooth*1.0f;
				ren.material.SetColor("_OutlineColor", ab);
			}
			yield return new WaitForSeconds( (t*1.0f)/(smooth*1.0f) * Time.deltaTime);
		}
		ren.material.color = b;
		playcount--;
		if(callback!=null)
			callback();
	}

	public static IEnumerator ColorLerp(SpriteRenderer ren,Color a, Color b,float t,int smooth, Callback callback)
	{
		// float endTime = Time.time + t;
		// float nextChange = Time.time + ((t*1.0f)/(smooth*1.0f));

		// int changeCount = 1;
		
		//smooth = 100;
		playcount++;
		ren.color = a;
		// while(Time.time<endTime)
		// {
		// 	if(Time.time > nextChange)
		// 	{
		// 		nextChange = Time.time + (((t*1.0f)/(smooth*1.0f)) * changeCount);
		// 		ren.material.color += (b-a)/smooth*1.0f;
		// 		changeCount++;
		// 	}
		// 	yield return null;
		// }
		for(int i =0;i<smooth;i++)
		{
			ren.color += (b-a)/smooth*1.0f;
			yield return new WaitForSeconds( (t*1.0f)/(smooth*1.0f) * Time.deltaTime);
		}
		ren.color = b;
		playcount--;
		if(callback!=null)
			callback();
	}

	// public static IEnumerator ColorLerp(Renderer ren,Color a, Color b,float t,int smooth, Callback callback)
	// {
	// 	// float endTime = Time.time + t;
	// 	// float nextChange = Time.time + ((t*1.0f)/(smooth*1.0f));

	// 	// int changeCount = 1;
		
	// 	//smooth = 100;
	// 	playcount++;
	// 	ren.material.color = a;
	// 	// while(Time.time<endTime)
	// 	// {
	// 	// 	if(Time.time > nextChange)s
	// 	// 	{
	// 	// 		nextChange = Time.time + (((t*1.0f)/(smooth*1.0f)) * changeCount);
	// 	// 		ren.material.color += (b-a)/smooth*1.0f;
	// 	// 		changeCount++;
	// 	// 	}
	// 	// 	yield return null;
	// 	// }
	// 	for(int i =0;i<smooth;i++)
	// 	{
	// 		ren.material.color += (b-a)/smooth*1.0f;
	// 		yield return new WaitForSeconds( (t*1.0f)/(smooth*1.0f) * Time.deltaTime);
	// 	}
	// 	ren.material.color = b;
	// 	playcount--;
	// 	if(callback!=null)
	// 		callback();
	// }

	public static IEnumerator Position(Transform trans,Vector3 a,Vector3 b,float t,Callback callback)
	{
		// float endTime = Time.time + t;
		// float nextChange = Time.time + ((t*1.0f)/(smooth*1.0f));

		// int changeCount = 1;
		
		//smooth = 100;
		playcount++;
		trans.position = a;
		//Debug.Log("Time:" + t + " Interval:" + (t*1.0f)/(smooth));

		// while(Time.time<endTime)
		// {
		// 	if(Time.time > nextChange)
		// 	{
		// 		trans.position += (b-a)/smooth*1.0f;
		// 		nextChange = Time.time + (((t*1.0f)/(smooth*1.0f)) * changeCount);
		// 		changeCount++;
		// 	}
		// 	yield return null;
		// }
		float acc =0.0f;
		Vector3 targetVal = b-a;
		while(acc<t && !stop)
		{
			trans.position = a + (b-a)*(acc/t);
			acc += Time.deltaTime;
			yield return null;
		}

		if(stop)
			stop = false;
		else
			trans.position = b;
		// for(int i =0;i<smooth;i++)
		// {
		// 	if(stop)
		// 		yield break;
		// 	trans.position += (b-a)/smooth*1.0f;
		// 	yield return new WaitForSeconds( (t*1.0f)/(smooth) * Time.deltaTime);
		// }
		
		playcount--;
		if(callback!=null)
			callback();
	}

	public static IEnumerator Scale(Transform trans,Vector3 a,Vector3 b,float t,int smooth,Callback callback)
	{
		// float endTime = Time.time + t;
		// float nextChange = Time.time + ((t*1.0f)/(smooth*1.0f));

		// int changeCount = 1;

		//smooth = 100;
		playcount++;
		trans.localScale = a;
		Debug.Log("Time:" + t + " Interval:" + (t*1.0f)/(smooth));
		// while(Time.time<endTime)
		// {
		// 	if(Time.time > nextChange)
		// 	{
		// 		trans.localScale += (b-a)/smooth*1.0f;
		// 		nextChange = Time.time + (((t*1.0f)/(smooth*1.0f)) * changeCount);
		// 		changeCount++;
		// 	}
		// 	yield return null;
		// }

		for(int i =0;i<smooth;i++)
		{
			trans.localScale += (b-a)/smooth*1.0f;
			yield return new WaitForSeconds( (t*1.0f)/(smooth) * Time.deltaTime);
		}
		trans.localScale = b;
		playcount--;
		if(callback!=null)
			callback();
	}
}
