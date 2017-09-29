using UnityEngine;
using System.Collections;

public class MyAnimations : MonoBehaviour {
	private static float smoothness = 100;
	public static IEnumerator ColorLerp(Renderer ren,Color a, Color b,float t,int smooth)
	{
		ren.material.color = a;
		for(int i =0;i<smooth;i++)
		{
			ren.material.color += (b-a)/smooth*1.0f;
			yield return new WaitForSeconds(t/smooth*1.0f);
		}
		ren.material.color = b;
	}
}
