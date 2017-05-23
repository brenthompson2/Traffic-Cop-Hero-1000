using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brendan Thompson
// 05/15/17
// EnemySpawner.cs Script

// Handles the spawning of Enemies and Humans

public class EnemySpawner : MonoBehaviour {

	//Global Variables
		// Public Variables
		public GameObject EnemyPrefab; // The Enemy
		public GameObject HumanPrefab; // The Human
		public int NumEnemiesSpawned;
		public int NumHumansSpawned;
		public int MaxEnemies = 5;
		public int MaxHumans = 2;
		public float EnemyMaxDelay = 5; // Time Between Loading Enemies
		public float EnemyMinDelay = 1; // Time Between Loading Enemies
		public float HumanMaxDelay = 5; // Time Between Loading Enemies
		public float HumanMinDelay = 1; // Time Between Loading Enemies

		// Private Variables
		private float SpawnY;
		private float EnemyCooldown; // Countdown for EnemyDelay
		private float HumanCooldown; // Countdown for EnemyDelay

		// Public Constants

		// Private Constants
		private const float SpawnX = 16.75f;
		private const float maxY = 3.3f;
		private const float minY = -8f;

	// Use this for initialization
	void Start () {
		EnemyCooldown = 1f;
		HumanCooldown = 2f;
	}
	
	// Update is called once per frame
	void Update () {

		// Spawn Enemies
		EnemyCooldown -= Time.deltaTime;
		if ((NumEnemiesSpawned < MaxEnemies) && (EnemyCooldown <= 0)){
			// Debug.Log ("Spawning Enemy!");
			EnemyCooldown = Random.Range(EnemyMinDelay, EnemyMaxDelay);

			// Calculate Spawn Transformation
			Quaternion NewRotation = transform.rotation;
			Vector3 NewPosition = transform.position;
			NewRotation.z = 0;
			NewPosition.x = SpawnX;

			SpawnY = Random.Range(minY, maxY);
			NewPosition.y = SpawnY; // number between -9 and 9

			// Spawn Enemy
			Instantiate(EnemyPrefab, NewPosition, NewRotation);
			NumEnemiesSpawned++;
			// Debug.Log ("Enemy " + NumEnemiesSpawned + " Spawned!");
		}

		// Spawn Humans
		HumanCooldown -= Time.deltaTime;
		if ((NumHumansSpawned < MaxHumans) && (HumanCooldown <= 0)){
			// Debug.Log ("Spawning Human!");
			HumanCooldown = Random.Range(HumanMinDelay, HumanMaxDelay);

			// Calculate Spawn Transformation
			Quaternion NewRotation = transform.rotation;
			Vector3 NewPosition = transform.position;
			// NewRotation.z = 0;
			NewPosition.x = SpawnX;
			NewPosition.y = -5; // number between -9 and 9

			SpawnY = Random.Range(-7.5f, 4f);
			NewPosition.y = SpawnY; // number between -9 and 9

			// Spawn Human
			Instantiate(HumanPrefab, NewPosition, NewRotation);
			NumHumansSpawned++;
			// Debug.Log ("Human " + NumHumansSpawned + " Spawned!");
		}
	}
}
