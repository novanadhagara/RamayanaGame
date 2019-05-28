using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour, ITakeDamage {

	public static Player _instance;
	public Animator Animator;
	public GameObject OuchEffect;
	public Arrow Arrow;
	public AudioClip PlayerHitSound;
	public AudioClip PlayerShootShound;
	private CharController _controller;

	//public Button right;

	private float normalizedHorizontalSpeed;
	public float MaxSpeed = 8;
	public float SpeedAccelerationOnGround = 10f;
	public float SpeedAccelerationInAir = 5f;

	public float MaxSolves = 208;	//REMEMBER TO CHANGE THIS  28 LATER
	public float CurrentSolves = 0f;
	public int TotalArrowFire = 0;
	public int CurrentArrowHit = 0;
	public int TimeLeft = 0;
	public float MaxHealth = 10f;
	public float CurrentHealth { get; private set;} //current health
	public int MaxWisdom = 100;
	public int CurrentWisdom { get; private set;}
	public int STR, INT, AGI, DEF;

	//Player prefs -> LastScene //Make several keys in prefs, as Try of each level

	public float FireRate;
	private float _canFireIn;
	public Transform ArrowFireLocation;

	public bool reachCheckpoint = false;		// to check finish or not
	private bool _isFacingRight;
	public bool IsDead{ get; private set;}
	private bool buttonRight = false, buttonLeft = false, buttonJump = false, buttonShoot = false, x = false ;

	public void Awake(){
		_controller = GetComponent<CharController> ();
		_isFacingRight = transform.localScale.x > 0; //face right or left?
		CurrentHealth = MaxHealth;
		CurrentWisdom = 0;
		//Debug.Log ("awake");
		//Debug.Log (""+CurrentHealth);
		if (_instance == null) {
			_instance = this;
		}
		STR = 1;
		AGI = (int)MaxSpeed;
		INT = 1;
		DEF = (int)MaxHealth;
		//CurrentSolves = 0;
		if (PlayerPrefs.GetString("LastScene") == null) {
			PlayerPrefs.SetString("LastScene", Application.loadedLevelName);
		}

        if (Application.loadedLevelName == "Level1")
        {
            MaxSolves = 8;
        }
        else if (Application.loadedLevelName == "Level2")
        {
            MaxSolves = 18;
        }
        else if (Application.loadedLevelName == "Level3")
        {
            MaxSolves = 28;
        }
        else if (Application.loadedLevelName == "Level4")
        {
            MaxSolves = 38;
        }
        else if (Application.loadedLevelName == "Level5")
        {
            MaxSolves = 48;
        }
        else if (Application.loadedLevelName == "Level6")
        {
            MaxSolves = 58;
        }
        else if (Application.loadedLevelName == "Level7")
        {
            MaxSolves = 68;
        }
        else if (Application.loadedLevelName == "Level8")
        {
            MaxSolves = 78;
        }
        else if (Application.loadedLevelName == "Level9")
        {
            MaxSolves = 88;
        }
        else if (Application.loadedLevelName == "Level0")
        {
            MaxSolves = 98;
        }
        else if (Application.loadedLevelName == "Level11")
        {
            MaxSolves = 108;
        }
        else if (Application.loadedLevelName == "Level12")
        {
            MaxSolves = 118;
        }
        else if (Application.loadedLevelName == "Level13")
        {
            MaxSolves = 128;
        }
        else if (Application.loadedLevelName == "Level14")
        {
            MaxSolves = 138;
        }
        else if (Application.loadedLevelName == "Level15")
        {
            MaxSolves = 148;
        }
        else if (Application.loadedLevelName == "Level16")
        {
            MaxSolves = 158;
        }
        else if (Application.loadedLevelName == "Level17")
        {
            MaxSolves = 168;
        }
        else if (Application.loadedLevelName == "Level18")
        {
            MaxSolves = 178;
        }
        else if (Application.loadedLevelName == "Level8")
        {
            MaxSolves = 188;
        }
        else if (Application.loadedLevelName == "Level9")
        {
            MaxSolves = 198;
        }
        else if (Application.loadedLevelName == "Level20")
        {
            MaxSolves = 208;
        }


	}

	public void Start(){
		//INITIALIZE THE STATS (STR, AGI , INT, DEF) Here from Playerprefs!
		reachCheckpoint = false;
	}

	public void Update(){
		//Debug.Log ("Player- Wisdom"+CurrentWisdom+" ArrowHit :"+CurrentArrowHit+" Total arrow"+TotalArrowFire);
		_canFireIn -= Time.deltaTime;
		if (!IsDead || !reachCheckpoint) {
			HandleInput ();
		}
		var movementFactor = _controller.State.isGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir; 
		
		if (IsDead) {
			_controller.SetHorizontalForce (0);		
		} else {
			_controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, 
			                                            normalizedHorizontalSpeed * MaxSpeed,
			                                            Time.deltaTime * movementFactor));
			// if facing left, max speed = -8, 
		}

		//Open after the animation defined
		Animator.SetBool ("IsGrounded", _controller.State.isGrounded);
		Animator.SetBool ("IsDead", IsDead);
		Animator.SetFloat ("Speed", Mathf.Abs (_controller.Velocity.x) / MaxSpeed);

		if (transform.position.y <= -5) {
			Die(); //SHOULD CHECK IT FIRST
			if(IsDead){
				Destroy (gameObject);
			}
		}


	}

	//public Void Kill()
	public void Die(){
		_controller.HandleCollisions = false;  
		GetComponent<Collider2D>().enabled = false;
		
		IsDead = true;
		CurrentHealth = 0;
		
		Debug.Log ("player kill");
		_controller.SetForce (new Vector2 (0, 15));
	}

	public void TakeDamage(int damage, GameObject instigator){
		AudioSource.PlayClipAtPoint (PlayerHitSound, transform.position);
		Instantiate (OuchEffect, transform.position, transform.rotation);
		CurrentHealth -= damage;
		
		if (CurrentHealth <= 0) {
			LevelManager.Instance.KillPlayer();		
		}
	}

	public void TouchRightUp(){
		buttonRight = false;
	}
	public void TouchRightDown(){
		buttonRight = true;
	}
	public void TouchLeftUp(){
		buttonLeft = false;
	}
	public void TouchLeftDown(){
		buttonLeft = true;
	}
	public void TouchJumpUp(){
		buttonJump = false;
	}
	public void TouchJumpDown(){
		buttonJump = true;
	}
	public void TouchShootUp(){
		buttonShoot = false;
	}
	public void TouchShootDown(){
		buttonShoot = true;
	}


	private void HandleInput(){
		if (buttonRight || Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)) {
			normalizedHorizontalSpeed = 1;
			if(!_isFacingRight){
				Flip();
			}
			
		}else if (buttonLeft || Input.GetKey (KeyCode.LeftArrow)||Input.GetKey (KeyCode.A)) {
			normalizedHorizontalSpeed = -1;
			if(_isFacingRight){
				Flip();
			}
        }


        else {
			normalizedHorizontalSpeed = 0;
		}
		
		if (buttonJump || _controller.CanJump&& Input.GetKeyDown (KeyCode.Space)||_controller.CanJump && Input.GetKeyDown (KeyCode.W)) {
			if (_controller.State.isGrounded) {
				_controller.Jump();
			}
		}

        //if (Input.GetMouseButtonDown (0)) {
        //	FireArrow();		
        //}

        if (buttonShoot || Input.GetKeyDown(KeyCode.Q) )
        {

            FireArrow();
        }
        /*
		if (buttonShoot ) {

			FireArrow();
		}
        */


        if (Input.GetKey(KeyCode.DownArrow) && Animator.GetFloat("Speed") <= 0.1f || Input.GetKey(KeyCode.S) && Animator.GetFloat("Speed") <= 0.1f)
        {
            Animator.SetBool("IsSquat", true);
        }
        else
        {
            Animator.SetBool("IsSquat", false);
        }
    }
    
    public void FireArrow(){

		if (_canFireIn > 0) {
			return;	
		}

        GameObject Bow = GameObject.Find("Bow");
		AudioSource.PlayClipAtPoint (PlayerShootShound, transform.position);
        
        // Choosing animation based on weapon
        if (Bow != null)
        {
            _canFireIn = FireRate;
            Animator.SetTrigger("Fire");
            TotalArrowFire += 1;

            if (Bow.GetComponent<SpriteRenderer>().sprite.name != "sword")
            {
                // Range
                Animator.SetBool("Melee", false);
                Vector2 arrowVelocity = Vector2.zero;
                
                //Arrow Particles effect here soon
                var direction = _isFacingRight ? Vector2.right : -Vector2.right;
                var direction2 = _isFacingRight ? Vector2.left : -Vector2.left;
                var arrow = (Arrow)Instantiate(Arrow, ArrowFireLocation.position, ArrowFireLocation.rotation);

                if (_controller.Velocity.x > 0)
                {
                    arrowVelocity = new Vector2(0, 3);
                    arrow.Initialize(gameObject, direction, arrowVelocity);
                }
                else if (_controller.Velocity.x < 0)
                {
                    arrowVelocity = new Vector2(0, -3);
                    arrow.Initialize(gameObject, direction, arrowVelocity);
                }
                else
                {
                    arrow.Initialize(gameObject, direction, _controller.Velocity);
                }

                //arrow.transform.localScale = new Vector3 (_isFacingRight ? 1 : -1, 1, 1);
            }
            else
            { 
                // Melee
                Animator.SetBool("Melee", true);
                Debug.Log("Pedang");
            }

        }
    }
	
	private void Flip(){
		transform.localScale = new Vector3 (-transform.localScale.x,
		                                    transform.localScale.y,
		                                    transform.localScale.z);
		_isFacingRight = transform.localScale.x > 0;
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Checkpoint> () != null) {
			reachCheckpoint = true;
			//REACH THE CHECKPOINT
			//Die();
			//Win ();
		}
		if (other.GetComponent<SimpleEnemyAI> () == null) {
			return;
		}


		if (PlayerHitSound != null) {
			AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
		}

		//POINTS TO ADD TO GAME CONTROLLER/ GAME MANAGER
		//GameManager.Instance.AddPoints (PointsToAdd);
		Instantiate (OuchEffect, transform.position, transform.rotation);
		if (_isFacingRight) {
			_controller.SetForce (new Vector2 (-5, 10));
			_controller.transform.position = new Vector3(_controller.transform.position.x - 0.2f, _controller.transform.position.y, _controller.transform.position.z);
			//Some Bug one the collider, if ths code didnt exist, reducehealt would be called twice
		} else {
			_controller.SetForce (new Vector2 (5, 10));
			_controller.transform.position = new Vector3(_controller.transform.position.x + 0.2f, _controller.transform.position.y, _controller.transform.position.z);
		}


		//gameObject.SetActive (false);
		//Destroy (this.gameObject);
	}

	public void ReduceHealth(int Damage){
		CurrentHealth -= Damage;
		if (CurrentHealth <= 0) {
			//gameover
			Animator.SetBool("IsDead", true);
			Die();
			return;
		}

	}

	public void AddWisdomPoint(int Wisdom){
		if (CurrentWisdom < MaxWisdom) {
			this.CurrentWisdom += Wisdom;
		}
		CurrentSolves+=1;
//		Debug.Log ("W"+Player._instance.CurrentWisdom+"S:"+Player._instance.CurrentSolves+"MS:"+Player._instance.MaxSolves);
	}

	public void AddTRY(){
		if (!Application.loadedLevelName.Equals(PlayerPrefs.GetString ("LastScene"))) {
			ResetTRY();
			Debug.Log ("Reset Try");
		}

		PlayerPrefs.SetInt ("TRY3", PlayerPrefs.GetInt("TRY3")+1);
		PlayerPrefs.SetString ("LastScene", Application.loadedLevelName.ToString());
		PlayerPrefs.SetString("IsPaused", "false");
		Debug.Log ("Add Try");
		Debug.Log ("LastScene"+Application.loadedLevelName.ToString());
	}
	public void ResetTRY(){
		if (Application.loadedLevelName.Equals ("Level20")) {
			PlayerPrefs.SetInt("TRY3", 0);
			Debug.Log ("loaded level "+Application.loadedLevelName.ToString());
		}
	}
	public int GetTRY(){
		if (Application.loadedLevelName.Equals ("Level20")) {
			return PlayerPrefs.GetInt ("TRY3");
		} else {
			return 0;
		}

	}
}