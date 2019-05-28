using UnityEngine;
using System.Collections;

public class WisdomPoint : MonoBehaviour {

	public GameObject Effect;
	public AudioClip HitWisdomSound;

	public string WisdomWords="";
	public int Point = 1;
	private float yAcc=0;
	private bool Up=true,Down=false,accPlus=true, onRange=false;

	public void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> () == null) {
			return;
		}

		if (HitWisdomSound != null) {
			AudioSource.PlayClipAtPoint(HitWisdomSound, transform.position);
		}
		Player._instance.AddWisdomPoint (Point);
		WisdomText.instance.SetText(WisdomWords);
		Instantiate (Effect, transform.position, transform.rotation);

		Destroy (this.gameObject);
	}

	void Update(){
		if (PlayerPrefs.GetString ("IsPaused") != "true") {
			CheckRange ();
			if(onRange){
				
				if (Up) {
					if(accPlus){
						yAcc +=0.02f*Time.deltaTime*10;
					}else	{
						yAcc -= 0.02f*Time.deltaTime*10;
					}
					
					if(yAcc>=0.05f){
						yAcc=0.05f;
						accPlus=false;
					}else if(yAcc<=0){
						yAcc=0;
						Up=false;
						Down=true;
						accPlus=true;
					}
					transform.position += new Vector3 (0,yAcc,0);
					
				} else if(Down){ //if(down)
					if(accPlus){
						yAcc +=0.02f*Time.deltaTime*10;
					}else{
						yAcc -= 0.02f*Time.deltaTime*10;
					}	
					
					if(yAcc>=0.05f){
						yAcc=0.05f;
						accPlus=false;
					}else if(yAcc<=0){
						yAcc=0;
						Up=true;
						Down=false;
						accPlus=true;
					}
					transform.position -= new Vector3 (0,yAcc,0);
				}
			}
		}
	}

	void CheckRange(){
		if (Player._instance != null) {
			if (Mathf.Abs (this.transform.position.x - Player._instance.transform.position.x) < 5) {
				onRange = true;
			} else {
				onRange = false;
			}
		}
	}
}
