using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadBehaviour : MonoBehaviour {

	public float jumpForce = 10f;

	void OnCollisionEnter(Collision other)
	{
		if(other.relativeVelocity.y <= 0)
		{
			Rigidbody rb = other.collider.GetComponent<Rigidbody>();
			if( rb != null) 
			{
				rb.velocity = new Vector2(0,jumpForce);
			}
		}
	} 
}
