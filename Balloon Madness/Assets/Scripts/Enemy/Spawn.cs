using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject enemy;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	void Start () 
	{
		InvokeRepeating ("Spawning", spawnTime, spawnTime);
	}

	void Spawning () 
	{
		int spIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints [spIndex].position, spawnPoints[spIndex].rotation);
	}
}
