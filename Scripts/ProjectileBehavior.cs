using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brendan Thompson
// 05/15/17
// ProjectileBehavior.cs Script

public class ProjectileBehavior : MonoBehaviour {

	// Private Variables
		private string ProjectileType; //  Type of Projectile
		private int Health = 10;

	// Public Constants
		public const float StopperSpeed = 10f;
		public const float YielderSpeed = 12f;
		public const float BulletSpeed = 5f;

	// Private Constants


	// Initialize Projectile
	void Start () {
		ProjectileType = gameObject.tag;
		// Debug.Log ("Projectile " + ProjectileType);
	}
	
	// Update is called once per frame
	void Update () {
		
 		// Bullet Movement Vertically 
 		Vector3 newPosition = transform.position;
 		switch (ProjectileType){
 			case "Stopper":
				newPosition.x = newPosition.x + (StopperSpeed * Time.deltaTime);
 				break;
 			case "Yielder":
				newPosition.x = newPosition.x + (YielderSpeed * Time.deltaTime);
 				break;
 			case "Bullet":
				newPosition.x = newPosition.x + (BulletSpeed * Time.deltaTime);
 				break;
 		}
		transform.position = newPosition;

		// Handle Having No Health
		if (Health == 0){
			Die();
		}
	}

	// If Projectile Contacts Anything
	void OnTriggerEnter2D (Collider2D OtherObject){
		// Debug.Log ("Projectile Hit Something!");
		switch (OtherObject.tag){
			case "Stopper":
				break;
			case "Bullet":
				break;
			case "Yielder":
				break;
			case "Enemy": // Projectile Collided with Enemy
		 		switch (ProjectileType){
		 			case "Stopper":
						Health -= 1;
						break;
		 			case "Yielder":
						Health -= 1;
						break;
		 			case "Bullet":
						Health = 0;
						break;
		 		}
				break;
			case "Human": // Projectile Collided with Human
		 		switch (ProjectileType){
		 			case "Stopper":
						Health -= 1;
						break;
		 			case "Yielder":
						Health -= 1;
						break;
		 			case "Bullet":
						Health = 0;
						break;
		 		}
				break;
		}
	}

	void Die (){
		Destroy(gameObject);
	}
}
