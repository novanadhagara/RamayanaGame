using UnityEngine;
using System.Collections;

public class SimpleArrow : Arrow{//, ITakeDamage {

	//THIS SHOULD CHECK THE PLATFORM MASK : PLATFORMS, ENEMY;
	public Sprite[] Arrows;
	public GameObject DestroyerEffect;

	public float TimeToLive;
	public int Damage;
	public int PointsToGiveToPlayer;

	void Start(){
        if (Arrows.Length == 3) {
            gameObject.GetComponent<SpriteRenderer>().sprite = Arrows[PlayerPrefs.GetInt("Weapons") ];
        }
	}

	public void Update(){
		if (PlayerPrefs.GetString ("IsPaused") != "true") {
			if ((TimeToLive -= Time.deltaTime) <= 0) {
				DestroyArrow();
				return;
			}
			
			transform.Translate ((Direction + new Vector2(InitialVelocity.x, 0)) * Speed * Time.deltaTime, Space.World);
			transform.Translate (Direction * ((Mathf.Abs(InitialVelocity.x) + Speed)
			                                  * Time.deltaTime), Space.World 
			                     );
		}
	}
	//TAKE DAMAGE DIDNT USED
	/*public void TakeDamage(int Damage, GameObject instigator){
		if (PointsToGiveToPlayer != 0) {
			var arrow = instigator.GetComponent<Arrow>();
			if(arrow != null && arrow.Owner.GetComponent<Player>() != null){
				GameManager.Instance.AddPoints(PointsToGiveToPlayer);
				//floating text
			}
		}
		Debug.Log ("TakeDamage Arrow");
		//DestroyArrow ();
	}*/

	/*protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage){
		takeDamage.TakeDamage (Damage, gameObject);
		DestroyArrow();
	}*/

        ///
	private void DestroyArrow(){
		if (DestroyerEffect != null) {
			Instantiate(DestroyerEffect, transform.position, transform.rotation);		
		}
		Destroy (gameObject);
	}

	protected override void OnCollideOther(Collider2D other){
		if (other.GetComponent<SimpleEnemyAI> () != null) {
			if(other.gameObject.name!="Kurawa"){
				Destroy(other.gameObject);
				Player._instance.CurrentArrowHit +=1;
				//ADD POINT TO PLAYER
			}
		}
		DestroyArrow();
		//Other object here
	}

}
