using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Enemy") 
		{
			Destroy (other.gameObject);
			Destroy (this.gameObject);
			Score.score++;
		}
		if (other.transform.tag == "Wall") 
		{
			Destroy (this.GetComponent<Rigidbody2D> ());
			Destroy (this.GetComponent<Collider2D> ());
		}
	}
}
