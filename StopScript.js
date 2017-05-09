#pragma strict

// Brendan Thompson
// 05/05/17
// StopScript.js

// Variables
	var selected = 0;
	var Stop : Sprite;
	var StopSelected : Sprite;

function Start () {
	
}

function Update () {
	
}

function Selected () {
	if (selected){
		Stop.GetComponent(SpriteRenderer)().sprite = Stop;
		selected = 0;
	}
	else {
		Stop.GetComponent(SpriteRenderer)().sprite = StopSelected;
		selected = 1;
	}
}
