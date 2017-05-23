using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
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
		public int LivesRemaining = 10;	// Lives Remaining
		public float TimeRemaining; // Time till victory
		public Slider TimeSlider; //
		public Image SliderFill;
		public GameObject PauseUI;
		public GameObject LostUI;
		public GameObject WonUI;

	// Private Variables
		private Color Green1 = new Color(0f, 1f, 0f, 0.5f);
		private Color Green2 = new Color(0f, .9f, 0f, 0.5f);
		private Color Green3 = new Color(0f, .8f, 0f, 0.5f);
		private Color Green4 = new Color(0f, .5f, 0f, 0.5f);
		private Color Green5 = new Color(0f, .4f, 0f, 0.5f);
		private Color Green6 = new Color(0f, .1f, 0f, 0.5f);
		private Color Yellow1 = new Color(.5f, .5f, 0f, 0.5f);
		private Color Red1 = new Color(1f, 0f, 0f, 0.5f);
		private bool isPaused;
		private bool gameOver;

	// Public Constants
		public const int TotalTime = 120;
		public const int TotalLives = 10;

	// Private Constants

	// Use this for initialization
	void Start () {		
		TimeRemaining = (float) TotalTime;
		LivesRemaining = TotalLives;
		// TimeSlider.MaxValue = TotalLives;
		TimeSlider.value = TotalLives;
		SliderFill.color = Green1;
		isPaused = false;
		PauseUI.SetActive(false);
		LostUI.SetActive(false);
		WonUI.SetActive(false);
		gameOver = false;
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;
		LivesRemaining = TotalLives - EnemiesPassed - HumansKilled;

		// Timer Finished, Beat Level
		if (TimeRemaining <= 0){
			// Debug.Log("Congrats you beat the level!");
			gameOver = true;
			WonGame();
		}

		// No Lives Remaining, Retry Level
		if (LivesRemaining <= 0){
			// Debug.Log("You Lose!");
			gameOver = true;
			LostGame();
		}

		// Handle Slider Color
		TimeSlider.value = (int) TimeRemaining;
		switch (LivesRemaining){
			case 8:
				SliderFill.color = Green2;	
				break;
			case 7:
				SliderFill.color = Green3;	
				break;
			case 6:
				SliderFill.color = Green4;
				break;
			case 5:
				SliderFill.color = Green5;
				break;
			case 4:
				SliderFill.color = Green6;
				break;
			case 3:
				SliderFill.color = Yellow1;
				break;
			case 2:
				SliderFill.color = Color.black;
				break;
			case 1:
				SliderFill.color = Red1;
				break;
		}
		
		// if hit "Pause" button
		if (Input.GetButtonDown("Pause") && (!gameOver)){
			// Debug.Log("Paused " + isPaused);
			isPaused = !isPaused;
			if (isPaused){
				PauseGame();
			}
			if (!isPaused){
				UnPauseGame();
			}
		}
	}

	public void PauseGame(){
		isPaused = true;
		PauseUI.SetActive(true);
		Time.timeScale = 0;
	}

	public void UnPauseGame(){
		isPaused = false;
		PauseUI.SetActive(false);
		Time.timeScale = 1;
	}

	public void LostGame(){
		Time.timeScale = 0;
		LostUI.SetActive(true);
	}

	public void WonGame(){
		Time.timeScale = 0;
		WonUI.SetActive(true);
	}

}
