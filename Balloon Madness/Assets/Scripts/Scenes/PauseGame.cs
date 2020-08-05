using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public Transform canvas;

	void Start()
	{
		Time.timeScale = 1f;
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
			Pause ();
	}

	public void Pause()
	{
		if (canvas.gameObject.activeInHierarchy == false) 
		{
			Time.timeScale = 0f;
			canvas.gameObject.SetActive (true);
		} else
		{
			Time.timeScale = 1f;
			canvas.gameObject.SetActive (false);
		}
	}
}
