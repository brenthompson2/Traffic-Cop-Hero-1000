using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brendan Thompson
// 05/15/17
// GameplayManager.cs Script

// Manages elements of gameplay like Lives, Kills, and Time Remaining

public class GameplayManager : MonoBehaviour {

	// Public Variables
		public int EnemiesKilled = 0;
		public int HumansKilled = 0;
		public int EnemiesPassed = 0;
		public int HumansPassed = 0;
		public int Lives = 10;
		public float TimeRemaining;

	// Public Constants
		public const float TotalTime = 120;

	// Use this for initialization
	void Start () {		
		TimeRemaining = TotalTime;
	}
	
	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;

		// Timer Finished, Beat Level
		if (TimeRemaining <= 0){
			Debug.Log("Congrats you win!");
		}

		// No Lives Left, Retry Level
		if (EnemiesPassed >= Lives){
			Debug.Log("Game Over");
		}
		
	}
}
