using UnityEngine;
using System.Collections;

public class InvisiMaskTest : MonoBehaviour {
	public enum RenderLayer {Geometry = 2000,GeometryP1 = 2001,Transparent = 3000,TransparentP1= 3001,Overlay = 4000,OverlayP1 = 4001};
	public bool applyToAllChildren = false;
	public RenderLayer renLayer;
	public Renderer ren;
	public int renQueue = 2002;
	public int curQueue;

	// Use this for initialization
	void Awake () {
		//Debug.Log(gameObject.name + " skill rendqueue before:" + ren.material.renderQueue);
		curQueue = ren.material.renderQueue;
		ren.material.renderQueue = renQueue;//(int)renLayer;
		//Debug.Log(gameObject.name + " skill rendqueue after:" + ren.material.renderQueue);
		//Debug.Log("RenQueue" + ren.material.renderQueue);
		

		if(applyToAllChildren && transform.childCount>0)
			ApplyChildren(transform);
	}	

	public void ApplyChildren(Transform t)
	{
		Debug.Log("For Parent " + t.gameObject.name);

		Renderer ren;
		ren = t.gameObject.GetComponent<Renderer>();
		if(ren)
			ren.material.renderQueue = renQueue;

		for(int i=0;i<t.childCount;i++)
		{
			
			ren = t.GetChild(i).gameObject.GetComponent<Renderer>();
			
			if(ren)
			{
				Debug.Log("Layer Applied to " + t.GetChild(i).gameObject.name);
				foreach(Material mat in ren.materials)
					mat.renderQueue = renQueue;
			}
			
			if(t.GetChild(i).childCount>0)
			{
				ApplyChildren(t.GetChild(i));
			}
		}
	}

	public void SetQueue(Material mat)
	{
		//Debug.Log("Refreshing");
		curQueue = mat.renderQueue;
		mat.renderQueue = renQueue;
	}
}
