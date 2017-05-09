#pragma strict

// Brendan Thompson
// 05/05/17
// Player.js

// Global Variables
	var maxMovmentSpeed : float = 0.2;
	var Gun : Sprite;
	var GunSelected : Sprite;
	var Yield : Sprite;
	var YieldSelected : Sprite;
	//private var weapon : Sprite;

// Constants
	// const string gun;
	// const string stopSign;
	// const string yieldSign;

function Start () {
	// weapon = gun;
	// Stop.GetComponent(SpriteRenderer)().sprite = StopSelected;
}

function Update () {
	var rightBorder = 7;
	var leftBorder = -7.5;

	// Movement
	if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A))){
		// animation

		// If Moving Right
		if ((Input.GetKey(KeyCode.D)) && (transform.position.x < rightBorder)) {
	         transform.position.x += maxMovmentSpeed;
	     }// If Moving Left
	
		if ((Input.GetKey(KeyCode.A)) && (transform.position.x > leftBorder)) {
	         transform.position.x -= maxMovmentSpeed;
	     }
	}

	

	

     // Weapon Selection
     // if (Input.GetKey(KeyCode.Q)){
     	//Stop.GetComponent(SpriteRenderer)().sprite = Stop;
     	//Gun.GetComponent(SpriteRenderer)().sprite = GunSelected;
     // 	if (weapon == gun){
     // 		weapon = yieldSign;
     // 		}
     // 	if (weapon == yieldSign){
     // 		weapon = stopSign;
     // 	}
     // 	if (weapon == stopSign){
     // 		weapon = gun;
     // 	}
     // }
     // if (Input.GetKey(KeyCode.E)){
     // 	if (weapon == gun){
     // 		weapon = stopSign;
     // 	}
     // 	if (weapon == yieldSign){
     // 		weapon = gun;
     // 	}
     // 	if (weapon == stopSign){
     // 		weapon = yieldSign;
     // 	}
     // }
}
