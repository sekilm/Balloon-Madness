using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed;

	void Start() 
	{
		target = GameObject.Find("Clown").transform;
	}

	void Update() 
	{
		if(target != null)
			transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;
	}
}   