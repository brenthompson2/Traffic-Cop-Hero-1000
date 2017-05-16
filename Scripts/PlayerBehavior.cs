using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brendan Thompson
// 05/15/17
// PlayerBehavior.cs Script

// Handles Control of the player
		// Check Button Mapping in Unity with edit > project settings > input 
	// W & S = move Vertically
	// A & D = cycle weapons
	// space = fire weapon

public class PlayerBehavior : MonoBehaviour {

	// Public Variables 
		public GameObject BulletPrefab; // The Bullet
		public GameObject StopPrefab; // The Shot
		public GameObject YieldPrefab; // The Yield
		public Sprite PlayerGun;
		public Sprite PlayerStop;
		public Sprite PlayerYield;

	// Private Variables
		private Vector3 BulletOffset = new Vector3(2f, 1.18f, 0f); // Bullet spawn location compared to player
		private Vector3 OtherOffset = new Vector3(2f, 1.18f, 0f); // Bullet spawn location compared to player
		private float BulletTimer = 0; // Time it take the gun to cool down
		private float StopTimer = 0; // Time it take the stopper to cool down
		private float YieldTimer = 0; // Time it take the yielder to cool down
		private int CorrectLayer;	// set to the layer that the Player starts at
		private int CurrentWeapon; // The Value of the Weapon 0=Stop 1=Gun 2=Yield
		private float invulnTimer = 0; // Time to be invulnerabile after colliding with enemy
		private SpriteRenderer SpriteR;

	// Public Constants
		public const float MaxMovmentSpeed = 15f;
		public const float BulletDelay = 0.5f; // Delay between Bullets
		public const float StopDelay = 0.5f; // Delay between Stoppers
		public const float YieldDelay = 0.5f; // Delay between Yielders

	// Private Constants
		private const float bottomBorder = -10f;
		private const float topBorder = 8.25f;

	// Use this for initialization
	void Start () {
		CorrectLayer = gameObject.layer; // Save the proper player layer
		CurrentWeapon = 1; // Start with the Gun
		SpriteR = GetComponent<SpriteRenderer>();
		SpriteR.sprite = PlayerGun;
	}
	
	// Update is called once per frame
	void Update () {

 		// Player Movement Vertically 
 		Vector3 newPosition = transform.position;
		newPosition.y = newPosition.y + (Input.GetAxis("Vertical") * MaxMovmentSpeed * Time.deltaTime) ;
			// new y = original y + (Vertical Input * movementSpeed per second)
			//Input.GetAxis("Vertical"); // returns a float between -1.0 to 1.0 related to button mapping
		if ((newPosition.y > bottomBorder) && (newPosition.y < topBorder)){
			transform.position = newPosition;
		}


		// Handle Invulnerability
		// invulnTimer -= Time.deltaTime;
		// if (invulnTimer <= 0){
		// 	gameObject.layer = CorrectLayer;
		// }


		// Handle Firing Projectile
		BulletTimer -= Time.deltaTime;
		StopTimer -= Time.deltaTime;
		YieldTimer -= Time.deltaTime;
		if (Input.GetButton("Fire1")) {
			switch (CurrentWeapon){
				case 0:
					if (BulletTimer <= 0){
						FireWeapon(0);
						BulletTimer = StopDelay;
					}
					break;
				case 1:
					if (StopTimer <= 0){
						FireWeapon(1);
						StopTimer = StopDelay;
					}
					break;
				case 2:
					if (YieldTimer <= 0){
						FireWeapon(2);
						YieldTimer = StopDelay;
					}
					break;
			}
		}
		

		// Handle Swapping Weapons
		if (Input.GetKeyDown(KeyCode.D)){
			ChangeWeapon(1);
		}
		if (Input.GetKeyDown(KeyCode.A)){
			ChangeWeapon(0);
		}
	}

	// If Player Contacted by Enemy
	void OnTriggerEnter2D (){
		// Debug.Log ("Player Trigger!");


		// Make Invulnerable
		//invulnTimer = 0.50f;
		// gameObject.layer = 10;
		//Debug.Log ("\t New Health = ", health);
	}

	// Change Weapons - 1=cycle higher, 0=cycle lower
	void ChangeWeapon(int CycleDirection){
		switch (CurrentWeapon){
			case 0: // Currently On Stopper
				if (CycleDirection == 1){
					CurrentWeapon = 1;
					SpriteR.sprite = PlayerGun;
				}
				else {
					CurrentWeapon = 2;
					SpriteR.sprite = PlayerYield;
				}
				//Debug.Log("New Weapon: " + CurrentWeapon);
				break;
			case 1: // Currently On Gun
				if (CycleDirection == 1){
					CurrentWeapon = 2;
					SpriteR.sprite = PlayerYield;
				}
				else {
					CurrentWeapon = 0;
					SpriteR.sprite = PlayerStop;
				}
				//Debug.Log("New Weapon: " + CurrentWeapon);
				break;
			case 2: // Currently On Yielder
				if (CycleDirection == 1){
					CurrentWeapon = 0;
					SpriteR.sprite = PlayerStop;
				}
				else {
					CurrentWeapon = 1;
					SpriteR.sprite = PlayerGun;
				}
				//Debug.Log("New Weapon: " + CurrentWeapon);
				break;
		}
	}

	// Fires the projectile associated with the Weapon
	void FireWeapon(int Weapon){
		Quaternion newRotation = transform.rotation;
		Vector3 newPosition = transform.position;
		switch (Weapon){
			case 0:
				newPosition += OtherOffset;
				Instantiate(StopPrefab, newPosition, newRotation);
				break;
			case 1:
				newPosition += BulletOffset;
				newRotation.z += 270;
				Instantiate(BulletPrefab, newPosition, newRotation);
				BulletTimer = BulletDelay;
				break;
			case 2:
				newPosition += OtherOffset;
				Instantiate(YieldPrefab, newPosition, newRotation);
				YieldTimer = YieldDelay;
				break;
		}
	}

}
