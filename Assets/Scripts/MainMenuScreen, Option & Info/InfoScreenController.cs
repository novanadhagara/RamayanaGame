using UnityEngine;
using System.Collections;

public class InfoScreenController : MonoBehaviour {

	public GameObject backbutton, infoboard;
	public GameObject creditsImage, howtoImage;
	public Animator backButtonAnimator, infoBoardAnimator;

	void Awake (){
		howtoImage.SetActive (true);
		creditsImage.SetActive(false);
	}

	public void Start(){
		TransitionIn ();
	}
	
	public void TransitionIn(){
		backButtonAnimator.SetBool ("BounceIn", true);
		infoBoardAnimator.SetBool ("BounceIn", true);
	}
	public void TransitionOut(){
		backButtonAnimator.SetBool ("BounceIn", false);
		infoBoardAnimator.SetBool ("BounceIn", false);
		
		backButtonAnimator.SetBool ("BounceOut", true);
		infoBoardAnimator.SetBool ("BounceOut", true);
	}

	public void GoToMainMenuScreen(){
		TransitionOut ();
		Invoke ("ToMainMenuScreen", 1f);
	}

	void ToMainMenuScreen(){
		Application.LoadLevel ("MainMenuScreen");
	}

	public void ShowHowTo(){
		infoBoardAnimator.SetBool ("Shake", true);
		howtoImage.SetActive (true);
		creditsImage.SetActive(false);
		Invoke ("ShakeOff", 0.5f);
	}

	public void ShowCredits(){
		infoBoardAnimator.SetBool ("Shake", true);
		howtoImage.SetActive (false);
		creditsImage.SetActive(true);
		Invoke ("ShakeOff", 0.5f);
	}

	void Shake(){
		infoBoardAnimator.SetBool ("Shake", true);
	}
	void ShakeOff(){
		infoBoardAnimator.SetBool ("Shake", false);
	}
}
