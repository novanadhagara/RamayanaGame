using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	private GameObject bgMusic;
	public GameObject playButton, infoButton, exitButton;
	public Animator playButtonAnimator, exitButtonAnimator, infoButtonAnimator, blurBackgroundAnimator, 
		titleAnimator;

	void Awake(){
		Screen.orientation = ScreenOrientation.Landscape;

		bgMusic = GameObject.Find ("GaMetal21(Clone)");
		if(bgMusic != null)
		{
			if (!bgMusic.GetComponent<AudioSource> ().isPlaying) {
				bgMusic.GetComponent<AudioSource> ().Play();
			}
		}
	}

	void Start(){
		TransitionIn ();
	}

	public void TransitionIn(){
		playButtonAnimator.SetBool ("BounceIn", true);
		exitButtonAnimator.SetBool ("BounceIn", true);
		infoButtonAnimator.SetBool ("BounceIn", true);
		blurBackgroundAnimator.SetBool ("FadeOut",true);
		titleAnimator.SetBool ("FadeIn", true);
	}

	public void TransitionOut(){
		playButtonAnimator.SetBool ("BounceIn", false);
		exitButtonAnimator.SetBool ("BounceIn", false);
		infoButtonAnimator.SetBool ("BounceIn", false);
		blurBackgroundAnimator.SetBool ("FadeOut",false);
		titleAnimator.SetBool ("FadeIn", false);

		playButtonAnimator.SetBool ("BounceOut", true);
		exitButtonAnimator.SetBool ("BounceOut", true);
		infoButtonAnimator.SetBool ("BounceOut", true);
		blurBackgroundAnimator.SetBool ("FadeIn",true);
		titleAnimator.SetBool ("FadeOut", true);
	}

	public void GoToOptionScreen(){
		TransitionOut ();
		Invoke ("ToOptionScreen",1f);
	}
	void ToOptionScreen(){
		Application.LoadLevel("OptionScreen");
	}
	
	public void GoToInfoScreen(){
		TransitionOut ();
		Invoke ("ToInfoScreen", 1f);
	}
	void ToInfoScreen(){
		Application.LoadLevel("InfoScreen");
	}

	public void GoToLevelScreen(){
		TransitionOut ();
		Invoke ("ToLevelScreen", 1f);
	}
	void ToLevelScreen(){
		Application.LoadLevel("LevelScreen");
	}
	
	public void ExitApplication(){
		TransitionOut ();
		Invoke ("ToExitApplication", 1f);
	}
	void ToExitApplication(){
		Application.Quit ();
	}

	/*
	 * cara selain invoke
	public void ExitApplication(){
		StartCoroutine (ToExitApplication());
	}
	IEnumerator ToExitApplication(){
		yield return new  WaitForSeconds(1);
		Application.Quit ();
	}
	 */
}