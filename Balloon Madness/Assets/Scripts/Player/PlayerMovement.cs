using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;
	Animator anim;
	float someScale;

	void Start()
	{
		anim = GetComponent<Animator> ();
		someScale = transform.localScale.x;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
			anim.SetBool ("Walking", true);
		
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime, Space.World);
			transform.localScale = new Vector2(someScale, transform.localScale.y);
		} 
		else if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (-Vector2.right * speed * Time.deltaTime, Space.World);
			transform.localScale = new Vector2(-someScale, transform.localScale.y);
		} 
		else if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (Vector2.up * speed * Time.deltaTime, Space.World);
		}
		else if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (-Vector2.up * speed * Time.deltaTime, Space.World);
		}
		else
			anim.SetBool ("Walking", false);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.tag == "Enemy")
		{
			Destroy (this.gameObject);
			SceneManager.LoadScene ("Game Over");
		}
	}
}
