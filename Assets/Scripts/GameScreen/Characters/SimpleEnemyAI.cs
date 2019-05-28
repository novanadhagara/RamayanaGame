using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : MonoBehaviour {

	public Animator Animator;
	public GameObject WisdomPointsPattern;
	public GameOverController GOC;
	//public Player player;
	public GameObject DestroyedEffect;
	private CharController _controller;
	private string Difficulty;

	private Vector2 _direction;
	//private Vector2 _startPosition;

	public int HP;
	public int Points = 1;
	public int MaxRange = 5;
	public float Speed;
	public float FireRate = 1;
	private float _canFireIn;

	private bool OnRange = false;
	//private bool isFacingLeft = true;

	public void Start(){
		_controller = GetComponent<CharController> ();
		_direction = new Vector2 (-1, 0);
        //Debug.unityLogger.logEnabled = false;
        //_startPosition = transform.position;
    }

	public void Update(){
		UpdateDifficulty ();
		CheckRange2 ();
		if (OnRange && !GOC.gameOver && (PlayerPrefs.GetString("IsPaused") != "true")) {
			_controller.SetHorizontalForce (_direction.x * Speed);
			Animator.SetBool ("OnRange", OnRange);
		} else {
			_controller.SetHorizontalForce (0f);
			Animator.SetBool("OnRange", false);
		}

		if ((_direction.x < 0 && _controller.State.isCollidingLeft) ||
		    (_direction.x > 0 && _controller.State.isCollidingRight)) {
			_direction = -_direction;
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		if (transform.position.y <= -5) {
			Destroy (gameObject);
		}
		
		if ((_canFireIn -= Time.deltaTime) > 0) {
			return;
		}
		
		var rayCast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
		if (!rayCast) {
			return;		
		}

		//var arrow = (Arrow)Instantiate (Arrow, transform.position, transform.rotation);
		//arrow.Initialize (gameObject, _direction, _controller.Velocity);
		_canFireIn = FireRate;

		//Animator.SetBool ("IsGrounded", _controller.State.isGrounded);
		//Animator.SetFloat ("Speed", Mathf.Abs (_direction.x) / Speed);
		//Debug.Log ("anim"+Animator.GetFloat("Speed"));


	}
	//TAKE DAMAGE DIDNT USED
	/**public void TakeDamage(int damage, GameObject instigator){
		/**if (PointsToGivePlayer != 0) {
			var arrow = instigator.GetComponent<Arrow>();
			if(arrow != null && arrow.Owner.GetComponent<Player>() != null){
				GameManager.Instance.AddPoints(PointsToGivePlayer);
			}
		}**/
		
	/**	Instantiate (DestroyedEffect, transform.position, transform.rotation);
		//gameObject.SetActive (false);
		Destroy (gameObject);

		Debug.Log ("TakeDamage AI");
	}**/

	/*public void CheckRange(){
		if (this.transform.position.x - Player._instance.transform.position.x <= MaxRange ||
		    this) {
			OnRange = true;
			//Debug.Log(""+(this.transform.position.x - Player._instance.transform.position.x));
		} else {
			OnRange = false;
		} 
	}*/

	public void CheckRange2(){
		if (Player._instance != null) {
			if (Mathf.Abs(this.transform.position.x - Player._instance.transform.position.x) <= MaxRange) {
				OnRange = true;
				if(this.transform.position.x > Player._instance.transform.position.x){
					_direction = new Vector2 (-1, 0);
					if(transform.localScale.x <= 0){
						transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
					}
				}else {
					_direction = new Vector2 (1, 0);
					if(transform.localScale.x >= 0){
						transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
					}
					//_direction = -_direction;
				}
				//change dirrection
				//Debug.Log(""+(this.transform.position.x - Player._instance.transform.position.x));
			} else {
				OnRange = false;
			}
		}
		 
	}



    public void OnTriggerEnter2D(Collider2D other){
        print(other.gameObject.name);
        if (other.gameObject.tag == "SW")
        {
            HP--;
            print("collidex");
            //Destroy(gameObject);
            if (HP <= 0)
            {
                Instantiate(WisdomPointsPattern, transform.position, transform.rotation);
                Instantiate(DestroyedEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
      

        if (other.GetComponent<SimpleArrow> () != null) {
			HP--;

			if(gameObject.name=="Kurawa" && HP<=0){
				Destroy(this.gameObject);
			}

			Instantiate (WisdomPointsPattern, transform.position, transform.rotation);
			Instantiate (DestroyedEffect, transform.position, transform.rotation);
		}

		if (other.GetComponent<Player> () == null) {
			return;
		}

		Player._instance.ReduceHealth (1);
		Debug.Log (""+Player._instance.CurrentHealth);
	}

	void UpdateDifficulty(){
		Difficulty = PlayerPrefs.GetString("DIFFICULTY");

		if (Difficulty.Equals ("MEDIUM")) {
			//points
			//FireRate
			MaxRange = 6;
			Speed = 1;
		} else if (Difficulty.Equals ("HARD")) {
			//points
			//FireRate
			MaxRange = 7;
			Speed = 2;
		} else if (Difficulty.Equals ("VERY HARD")) {
			//points
			//FireRate
			MaxRange = 9;
			Speed = 3;
		} else { //JIKA BEGINNER DAN UNDEFINED
			//points
			//firerate
			MaxRange = 5;
			Speed = 0.5f;
		}
	}
}
