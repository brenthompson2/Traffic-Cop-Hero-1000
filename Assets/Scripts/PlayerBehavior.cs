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
		public GameObject StopPrefab; // The Stop
		public GameObject YieldPrefab; // The Yield
		public AudioClip gunShot;
		public AudioClip otherShot;

	// Private Variables
		private Vector3 BulletOffset = new Vector3(2f, 0f, 0f); // Bullet spawn location compared to player
		private Vector3 OtherOffset = new Vector3(2f, 0.2f, 0f); // Bullet spawn location compared to player
		private float BulletTimer = 0; // Time it take the gun to cool down
		private float StopTimer = 0; // Time it take the stopper to cool down
		private float YieldTimer = 0; // Time it take the yielder to cool down
		private int CurrentWeapon; // The Value of the Weapon 0=Stop 1=Gun 2=Yield
		// private int CorrectLayer;	// set to the layer that the Player starts at
		// private float invulnTimer = 0; // Time to be invulnerabile after colliding with enemy
		private Animator PlayerAnimator;
		private AudioSource Audio;
		private GameObject StopIcon; // The Stopsign icon for cooldowns
		private GameObject GunIcon; // The Gun icon for cooldowns
		private GameObject YieldIcon; // The Yield icon for cooldowns
		private Animator StopAnimator; // The Stopsign Renderer for cooldowns
		private Animator GunAnimator; // The Gun Renderer for cooldowns
		private Animator YieldAnimator; // The Yield Renderer for cooldowns
		private bool stopCooldown;
		private bool gunCooldown;
		private bool yieldCooldown;


	// Public Constants
		public const float MaxMovmentSpeed = 15f;
		public const float BulletDelay = 0.5f; // Delay between Bullets
		public const float StopDelay = 5f; // Delay between Stoppers
		public const float YieldDelay = 5f; // Delay between Yielders

	// Private Constants
		private const float topBorder = 3.3f;
		private const float bottomBorder = -8f;

	// Use this for initialization
	void Start () {
		// CorrectLayer = gameObject.layer; // Save the proper player layer
		
		// Start with gun and access weapon icons
		CurrentWeapon = 1;
		StopIcon = GameObject.Find("StopIcon");
		StopAnimator = StopIcon.GetComponent<Animator>();
		GunIcon = GameObject.Find("GunIcon");
		GunAnimator = GunIcon.GetComponent<Animator>();
		YieldIcon = GameObject.Find("YieldIcon");
		YieldAnimator = YieldIcon.GetComponent<Animator>();
		
		// Get Animator
		PlayerAnimator = GetComponent<Animator>();

		// Get AudioSource
		Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

 		// Player Movement Vertically 
 		if (Input.GetAxis("Vertical") != 0){
 			PlayerAnimator.SetBool("IsMoving", true);
	 		Vector3 newPosition = transform.position;
			newPosition.y = newPosition.y + (Input.GetAxis("Vertical") * MaxMovmentSpeed * Time.deltaTime);
			//Input.GetAxis("Vertical"); // returns a float between -1.0 to 1.0 related to button mapping
			if ((newPosition.y > bottomBorder) && (newPosition.y < topBorder)){
				transform.position = newPosition;
			}
 		}
 		else {
			PlayerAnimator.SetBool("IsMoving", false);
		}

		// Handle Invulnerability
		// invulnTimer -= Time.deltaTime;
		// if (invulnTimer <= 0){
		// 	gameObject.layer = CorrectLayer;
		// }

		// Handle Cooldowns
		StopTimer -= Time.deltaTime;
		if (StopTimer <= 0){
			stopCooldown = false;
			StopAnimator.SetBool("Cooldown", false);
		}
		BulletTimer -= Time.deltaTime;
		if (BulletTimer <= 0){
			gunCooldown = false;
			GunAnimator.SetBool("Cooldown", false);
		}
		YieldTimer -= Time.deltaTime;
		if (YieldTimer <= 0){
			yieldCooldown = false;
			YieldAnimator.SetBool("Cooldown", false);
		}

		// Handle Firing Projectile
		if (Input.GetButton("Fire1")) {
			switch (CurrentWeapon){
				case 0:
					if (!stopCooldown){
						FireWeapon(0);
						Audio.PlayOneShot(otherShot, 1f);
						stopCooldown = true;
						StopAnimator.SetBool("Cooldown", true);
						StopTimer = StopDelay;
					}
					break;
				case 1:
					if (!gunCooldown){
						FireWeapon(1);
						Audio.PlayOneShot(gunShot, 0.5f);
						gunCooldown = true;
						GunAnimator.SetBool("Cooldown", true);
						BulletTimer = BulletDelay;
					}
					break;
				case 2:
					if (!yieldCooldown){
						FireWeapon(2);
						Audio.PlayOneShot(otherShot, 1f);
						yieldCooldown = true;
						YieldAnimator.SetBool("Cooldown", true);
						YieldTimer = YieldDelay;
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
				}
				else {
					CurrentWeapon = 2;
				}
 				PlayerAnimator.SetInteger("Weapon", CurrentWeapon);
				break;
			case 1: // Currently On Gun
				if (CycleDirection == 1){
					CurrentWeapon = 2;
				}
				else {
					CurrentWeapon = 0;
				}
 				PlayerAnimator.SetInteger("Weapon", CurrentWeapon);
				break;
			case 2: // Currently On Yielder
				if (CycleDirection == 1){
					CurrentWeapon = 0;
				}
				else {
					CurrentWeapon = 1;
				}
 				PlayerAnimator.SetInteger("Weapon", CurrentWeapon);
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
