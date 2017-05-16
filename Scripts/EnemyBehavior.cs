using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brendan Thompson
// 05/15/17
// EnemyBehavior.cs Script

public class EnemyBehavior : MonoBehaviour {

	// Public Variables
		public int Health = 20; // Current Health for the Enemy
		public GameObject GMObject; // To access Game Manager
		public GameObject ESObject; // To access Enemy Spawner

	// Private Variables
		private string CollisionTag;
		private float StopTimer;
		private float YieldTimer;
		private GameplayManager GMScript; // To call functions that update score //(Script type is filename) 
		private EnemySpawner ESScript; // To Call functions that update numSpawned
		private string EnemyType;

	// Public Constants
		public const float MaxMovementSpeed = 3f; // the f enforces the float
		public const float YieldMoveSpeed = 1f;
		public const int BulletDamage = 10; // Damage inflicted by a bullet
		public const int StopDamage = 2; // Damage inflicted by a stopper
		public const int YieldDamage = 2; // Damage inflicted by a yielder
		public const float StopDuration = 2f;
		public const float YieldDuration = 2f;
		

	// Private Constants
		private const int LeftBorder = -17;
		

	// Use this for Initialization
	void Start () {
		GMObject = GameObject.Find("Gameplay Manager");
		GMScript = GMObject.GetComponent<GameplayManager>();
		ESObject = GameObject.Find("Enemy Spawner");
		ESScript = ESObject.GetComponent<EnemySpawner>();
		EnemyType = gameObject.tag;
	}
	
	// Update is called once per frame
	void Update () {

 		// Enemy Movement Horizontally
 		StopTimer -= Time.deltaTime;
 		YieldTimer -= Time.deltaTime;
 		if (StopTimer <= 0){
	 		Vector3 newPosition = transform.position;
	 		if (YieldTimer > 0){
				newPosition.x = newPosition.x - (YieldMoveSpeed * Time.deltaTime); // New X = Original X - MaxMovementSpeed per second
	 		}
	 		else {
				newPosition.x = newPosition.x - (MaxMovementSpeed * Time.deltaTime); // New X = Original X - MaxMovementSpeed per second
	 		}
			transform.position = newPosition;
 		}

		// Handle Reaching Edge
		Vector3 currentPosition = transform.position;
		if (currentPosition.x < LeftBorder){
			switch (EnemyType){
				case "Enemy":
					GMScript.EnemiesPassed += 1;
					Debug.Log ("Enemies Passed: " + GMScript.EnemiesPassed);
					ESScript.NumEnemiesSpawned -= 1;
					Die();
					break;
				case "Human":
					GMScript.HumansPassed += 1;
					Debug.Log ("Humans Passed: " + GMScript.HumansPassed);
					ESScript.NumHumansSpawned -= 1;
					Die();
					break;
			}
			Die();
		}

		// Handle Having No Health
		if (Health < 0){
			switch (EnemyType){
				case "Enemy":
					GMScript.EnemiesKilled += 1;
					Debug.Log ("Enemies Killed: " + GMScript.EnemiesKilled);
					ESScript.NumEnemiesSpawned -= 1;
					Die();
					break;
				case "Human":
					GMScript.HumansKilled += 1;
					Debug.Log ("Humans Killed: " + GMScript.HumansKilled);
					ESScript.NumHumansSpawned -= 1;
					Die();
					break;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D OtherObject){
		// Debug.Log ("Enemy Trigger!");
		switch (OtherObject.tag){
			case "Stopper":
				StopTimer = StopDuration;
				Health = Health - StopDamage;
				break;
			case "Bullet":
				Health = Health - BulletDamage;
				break;
			case "Yielder":
				YieldTimer = YieldDuration;
				Health = Health - YieldDamage;
				break;
		}
		//Debug.Log ("\t New Health = ", + Health);
	}

	void Die (){
		Destroy(gameObject);
	}
}