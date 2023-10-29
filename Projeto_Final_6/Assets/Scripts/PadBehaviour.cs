using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadBehaviour : MonoBehaviour 
{
	//Jump
	public float jumpForce = 10f;
    public AudioSource[] sound;

    void OnCollisionEnter2D(Collision2D other)
	{
		if(other.relativeVelocity.y <= 0)
		{
			Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
			//checks if a Rigidbody was found
			if(rb != null) 
			{
				rb.velocity = new Vector2(0,jumpForce);

                //SFX 
                sound[0].Play();
            }
		}
	} 
}
