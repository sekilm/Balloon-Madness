using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public GameObject projectile;
	public float speed = 5f;
	float timer;
	public float rate = 1f;

	void Start ()
	{
	}

	void Update () 
	{
		timer += Time.deltaTime;
		if (Input.GetMouseButton(0) && timer >= rate)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
			Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
			Vector2 direction = mousePos - myPos;
			direction.Normalize();
			Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90);
			GameObject knife = (GameObject)Instantiate (projectile, myPos, rotation);
			knife.GetComponent<Rigidbody2D> ().velocity = direction * speed;
			timer = 0f;
		}
	}
}
