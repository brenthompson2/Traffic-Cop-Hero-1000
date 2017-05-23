Brendan Thompson
ReadMe.txt
05/23/17

Traffic Cop Hero 1000 is an arcade style shooter designed by Brendan Thompson, Kelsey McCoy, and Griffin Cobb for a Transylvania University May Term Game Design class. 

The game is available for download at https://brenthompson2.itch.io/traffic-cop-hero-1000

The Gameplay video is on youtube at https://youtu.be/a1Yp0_OP7N0

The game was developed in Unity 5.5.3xf1 Linux on a system running Ubuntu 16.04 LTS

Builds:
	- Linux (V2, V3, V4, V5) - I use the x86_64
	- Mac (V2, V3, V5) - Trouble with resolutions
	- Windows (V2, V5) - Untested
	

Animation Controllers:
	- DaveController = Player Animations (idle - movement)
	- StopController = HUD StopIcon Animations (cooldown)
	- GunController = HUD GunIcon Animations (cooldown)
	- YieldController = HUD YieldIcon Animations (cooldown)
	- HumanIdle1 = Human Animations (movement - explosion)
	- ZombieIdle1 = Zombie Animations (movement - explosion)

Prefabs:
	- Btns = Various Buttons User throughout the game and BtnManager
		- EngGame, MainMenu, NextLevel, Restart, Resume
		- BtnManager = Stores script that buttons accress
	- Canvases = Menu Screens for Various Parts of the game
		- HUD, MainMenu, Instructions, Lost, Won, Pause, Pregame
	- Game Objects = Objects Used Throughout the game
		- Bullet = Gun Bullet
		- Enemy Spawner = Spawner Script
		- Enemy01 = Zombie
		- Gameplay Manager = Gameplay Script
		- Human01 = Human
		- Player = Dave
		- Stopper = Stop Bullet
		- Yielder = Yield Bullet

Scenes:
	- Main Menu
	- Scene01 - Scene04 = Game Levels

Scripts:
	- ButtonManager = Handles using buttons like:
		- Next Level (PlayLevel())
		- Replay Level (PlayLevel())
		- Main Menu (PlayLevel())
		- Start Game (LoadPregame())
		- Exit Game (ExitGameBtn())
	- EnemyBehavior = Handles most scripts relating to an individual enemy or human, including:
		- Public Var:
			- Explosion sound effect
		- Animation Parameter manipulation for transitions (Death)
			- (Need to link dynamically with animation length)
		- Horizontal Movement
		- Handle passing the player
		- Handle death (Health <= 0)
		- Collision with Stopper, Yielder, or Bullet
		- Destroying itself
		- Accesses GameManager Variables:
			- EnemiesKilled
			- EnemiesPassed
			- HumansKilled
			- HumansPassed
		- Accesses EnemySpawner Variables:
			- NumEnemiesSpawned
			- NumHumansSpawned
	- EnemySpawner = Handles the spawning of enemies and humans, which includes:
		- Public Variables:
			- GameObject EnemyPrefab
				- (Is it better to "find"?)
			- GameObject HumanPrefab
				- (Is it better to "find"?)
			- NumEnemiesSpawned (accessed by enemyBehavior.cs)
			- NumHumansSpawned (accessed by enemyBehavior.cs)
			- MaxEnemies (Manage Difficulty)
			- MaxHumans (Manage Difficulty)
			- EnemyMaxDelay (ManageDifficulty)
			- EnemyMinDelay (ManageDifficulty)
			- HumanMaxDelay (ManageDifficulty)
			- HumanMinDelay (ManageDifficulty)
		- Random Y coordinate within a range
		- Random Cooldown within a range
		- Stores NumEnemiesSpawned and NumHumansSpawned
	- GameplayManager = Handles the flow of the game, including:
		- Public Variables:
			- EnemiesKilled (accessed by enemyBehavior.cs)
			- HumansKilled (accessed by enemyBehavior.cs)
			- EnemiesPassed (accessed by enemyBehavior.cs)
			- HumansPassed (accessed by enemyBehavior.cs)
			- LivesRemaining = TotalLives - EnemiesPassed - HumansKilled
			- TimeRemaining
			- Slider TimeSlider = HUD indicator of time and livesRemaing
			- GameObject _UI = canvas associated with menu
				- (Is it better to "find"?)
		- Handle winning the game (TimeRemaining <= 0)
			- WinCanvas
		- Handle losing game (LivesRemaining)
			- LoseCanvas
		- Handle Slider Color (TimeSlider)
		- Handle Pausing and Unpausing Game (ResumeButton)
		- Handle Winning or Losing Screen
	- PlayerBehavior = Handles Control of the player, including;
		- Public Variables:
			- GameObject BulletPrefab
				- (Is it better to "find"?)
			- GameObject StopPrefab
				- (Is it better to "find"?)
			- GameObject YieldPrefab
				- (Is it better to "find"?)
			- AudioClip gunShot
				- (Is it better to "find"?)
			- AudioClip otherShot = Swoosh sound for special weapons
				- (Is it better to "find"?)
		- Movement Vertically
		- Movement Animations (IsMoving)
		- Handles Firing Projectile
			- Instantiates Projectile
			- Plays Sound
			- Starts Cooldown Timers
				- (Need to link dynamically with Animation length)
			- Starts Cooldown Animations
				- (Need to link dynamically with Cooldown Timers)
		- Handle Swapping Weapons
	- ProjectileBehavior
		- Movement Horizontal
		- Collision (Health)
			- Bullets Die Immediately while Others lose health slowly
		- Handles Destruction

- Sounds:
	- EXPLOSION = car death
	- GUNSHOT
	- SWOOSH = yielder and stopper firing noise
	- ThemeSong = Song Played During Gameplay (by Griffin Cobb)
	- ThemeSong2 = Menu Song (by Griffin Cobb)

- Sprites:
	- BackgroundMain = Gameplay background
	- BackgroundMenu = Menu background
	- BackgroundOld = Original Test Background
	- CopSprites01 = Original Test Sprites
		- Player, Projectiles, HUD Icons
		- Still the Bullet that is in use
	- DaveSprites = Idle and Movement
	- HudWeapons = Icons and Cooldown Icons
	- HumanSprites = Motion & Explosion
	- Instructions_Overlay = Extra Pregame Screen before start first playthrough 
		-(because people are too cool for the instructions menu)
	- MenuTitle = Flatter Title used in menus and in the hud 
		-(need to remove names from the bottom)
	- TitcleCardV2 = Larger Logo for splash screens and icons
