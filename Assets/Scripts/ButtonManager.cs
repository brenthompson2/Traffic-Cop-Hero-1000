using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour {

	// Public Variables

	// Private Variables
		private bool viewingInstructions;
		private GameObject MainUI;
		private GameObject InstructionsUI;
		private GameObject PregameUI;

	// Use this for initialization
	void Start () {

		// If Main Menu
		if (GameObject.Find("Main Canvas") != null){
			MainUI = GameObject.Find("Main Canvas");
			InstructionsUI = GameObject.Find("Instructions Canvas");
			PregameUI = GameObject.Find("Pregame Canvas");
			MainUI.SetActive(true);
			InstructionsUI.SetActive(false);
			viewingInstructions = false;
			PregameUI.SetActive(false);
		}
	}
	
	// // Update is called once per frame
	// void Update () {
	// }

	public void LoadPregame(){
		MainUI.SetActive(false);
		PregameUI.SetActive(true);
	}

	public void PlayLevel(string levelName){
		SceneManager.LoadScene(levelName);
	}

	public void ViewInstructions(){
		viewingInstructions = !viewingInstructions;
		if (viewingInstructions == true) {
			MainUI.SetActive(false);
			InstructionsUI.SetActive(true);
		}
		if (viewingInstructions == false) {
			MainUI.SetActive(true);
			InstructionsUI.SetActive(false);
		}
	}

	public void ExitGameBtn(){
		Application.Quit();
	}
}
