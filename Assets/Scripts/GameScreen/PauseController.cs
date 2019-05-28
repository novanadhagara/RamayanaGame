using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

	public Animator pauseBoardAnimator;
	public GameObject pauseboard, blurry;

	public bool pause;

	public void Start(){
		pause = false;
		blurry.SetActive (false);
		PlayerPrefs.SetString("IsPaused", "false");
		pauseBoardAnimator.SetBool ("BounceIn", false);
		pauseBoardAnimator.SetBool ("BounceOut", false);
	}

	public void TransitionIn(){
		pauseBoardAnimator.SetBool ("BounceIn", true);
		pauseBoardAnimator.SetBool ("BounceOut", false);
	}

	public void TransitionOut(){
		pauseBoardAnimator.SetBool ("BounceIn", false);
		pauseBoardAnimator.SetBool ("BounceOut", true);
	}

	public void Pause(){
		blurry.SetActive (true);
		pause = true;
		PlayerPrefs.SetString("IsPaused", "true");
		TransitionIn ();
	}

	public void Resume(){
		blurry.SetActive (false);
		pause = false;
		PlayerPrefs.SetString("IsPaused", "false");
		TransitionOut ();
	}

	public void GoToMainMenuScreen(){
		PlayerPrefs.SetString("IsPaused", "false");
		TransitionOut ();
		Invoke ("ToMainMenuScreen", 1f);
	}
	void ToMainMenuScreen(){
		Application.LoadLevel ("MainMenuScreen");
	}

	public void RestartLevel(){
		PlayerPrefs.SetFloat("CURRENT_TRY", Player._instance.GetTRY());
		PlayerPrefs.SetString("IsPaused", "false");
		TransitionOut ();
		Invoke ("ToRestartLevel", 1f);
		
	}
	void ToRestartLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}
}
