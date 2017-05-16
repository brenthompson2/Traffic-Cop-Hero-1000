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
		public int EnemiesKilled; // Number of Enemies killed by the player
		public int HumansKilled; // Number of Humans Killed by the player
		public int EnemiesPassed; // Number of Enemies that made it passed the player
		public int NumEnemiesSpawned;
		public int NumHumansSpawned;

		// Private Variables
		private float EnemyCooldown; // Countdown for EnemyDelay
		private float HumanCooldown; // Countdown for EnemyDelay
		private float SpawnY;

		// Public Constants
		public const int EnemyDelay = 5; // Time Between Loading Enemies
		public const int HumanDelay = 5; // Time Between Loading Enemies
		public const int MaxEnemies = 5;
		public const int MaxHumans = 2;

		// Private Constants
		private const float SpawnX = 16.75f;

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
			EnemyCooldown = EnemyDelay;

			// Calculate Spawn Transformation
			Quaternion NewRotation = transform.rotation;
			Vector3 NewPosition = transform.position;
			NewRotation.z = 0;
			NewPosition.x = SpawnX;

			SpawnY = -5;
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
			HumanCooldown = HumanDelay;

			// Calculate Spawn Transformation
			Quaternion NewRotation = transform.rotation;
			Vector3 NewPosition = transform.position;
			NewRotation.z = 90;
			NewPosition.x = SpawnX;
			NewPosition.y = -5; // number between -9 and 9

			SpawnY = 5;
			NewPosition.y = SpawnY; // number between -9 and 9

			// Spawn Human
			Instantiate(HumanPrefab, NewPosition, NewRotation);
			NumHumansSpawned++;
			// Debug.Log ("Human " + NumHumansSpawned + " Spawned!");
		}
	}
}
