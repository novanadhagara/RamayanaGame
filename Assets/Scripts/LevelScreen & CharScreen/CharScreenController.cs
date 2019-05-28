using UnityEngine;
using System.Collections;

public class CharScreenController : MonoBehaviour {

	/*public GameObject CharStats, CurrentStats, WisdomShop;
	public GameObject backbutton, charboard;
	public Transform  backin, backout, charin, charout;
	public bool inTransition, outTransition, ready;

	public void Start(){
		inTransition = true;
		outTransition = false;
		ready = false;
	}

	public void TransitionIn(){
		inTransition = true;
		outTransition = false;
	}
	public void TransitionOut(){
		outTransition = true;
		inTransition = false;
	}

	void Update(){
		UpdateTransition();
		GoToScreen ();
	}

	public void GoToScreen(){
		if (ready) {
			Application.LoadLevel ("LevelScreen");
		}
	}

	public void UpdateTransition(){
		//entry
		if (inTransition) {
			//char
			charboard.transform.position = Vector3.Lerp(charboard.transform.position,
			                                            new Vector3(charboard.transform.position.x, charboard.transform.position.y - 5, charboard.transform.position.z),
			                                             5);
			if(charboard.transform.position.y <= charin.position.y){
				//levelboard.transform.position.y = 0;
				inTransition=false;
			}
			
			if(backbutton.transform.position.y <= backin.position.y){
				//back
				backbutton.transform.position = Vector3.Lerp(backbutton.transform.position,
				                                             new Vector3(backbutton.transform.position.x, backbutton.transform.position.y + 5, backbutton.transform.position.z),
				                                             5);
			}
			
		} else if (outTransition) {
			//char
			charboard.transform.position = Vector3.Lerp(charboard.transform.position,
			                                            new Vector3(charboard.transform.position.x, charboard.transform.position.y + 5, charboard.transform.position.z),
			                                             5);
			if(charboard.transform.position.y >= charout.position.y){
				outTransition=false;
				ready = true;
			}
			
			//back
			backbutton.transform.position = Vector3.Lerp(backbutton.transform.position,
			                                             new Vector3(backbutton.transform.position.x, backbutton.transform.position.y - 5, backbutton.transform.position.z),
			                                             5);
		}
		Debug.Log (""+ready);
	}
	
	public void GoToLevelScreen(){
		TransitionOut ();
	}

	public void ShowCharStats(){
		CurrentStats.SetActive (false);
		WisdomShop.SetActive (false);
		CharStats.SetActive (true);
	}

	public void ShowCurrentStats(){
		WisdomShop.SetActive (false);
		CharStats.SetActive (false);
		CurrentStats.SetActive (true);
	}

	public void ShowWisdomShop(){
		CharStats.SetActive (false);
		CurrentStats.SetActive (false);
		WisdomShop.SetActive (true);
	}*/
}
