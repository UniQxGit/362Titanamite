using UnityEngine;
using System.Collections;

public class ListCuttoff : MonoBehaviour {
	public Collider cutOffCollider;
	public Collider myCol;
	private Renderer myRen;

	void Start()
	{
		//transform.parent.GetComponentInChildren<Collider>();
		myRen = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if((myRen.bounds.center.y-myRen.bounds.extents.y < cutOffCollider.bounds.min.y)||(myRen.bounds.center.y-myRen.bounds.extents.y > cutOffCollider.bounds.max.y))
		{
			myCol.enabled = false;
			myRen.enabled = false;
		}else
		{
			myRen.enabled = true;
			myCol.enabled = true;
		}
	}
}
