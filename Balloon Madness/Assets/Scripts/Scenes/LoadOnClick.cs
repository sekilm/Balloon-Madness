using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;
	private GameObject nameDialogue;

	Scene currentScene;

	void Start()
	{
		//nameDialogue = HighscoreManager.FindObjectOfType<GameObject> ();
		//nameDialogue = GameObject.FindGameObjectWithTag ("Name");
		currentScene = SceneManager.GetActiveScene ();
	}
		
	public void LoadScene(int scene)
	{
		
		loadingImage.SetActive (true);
		//if (currentScene.name == "Game Over")
		//	nameDialogue.SetActive (true);
		SceneManager.LoadScene (scene);
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
