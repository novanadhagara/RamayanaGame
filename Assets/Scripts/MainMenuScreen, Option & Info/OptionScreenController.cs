using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionScreenController : MonoBehaviour {

	public Slider slider;
	public GameObject backButton, optionBoard;
	public Animator backButtonAnimator, optionBoardAnimator;

	void Awake(){
		if (PlayerPrefs.GetFloat("SOUNDVOLUME") > 0.01f) {
			slider.value = PlayerPrefs.GetFloat ("SOUNDVOLUME");
		}
		PlayerPrefs.SetFloat ("SOUNDVOLUME", slider.value);
		AudioListener.volume = PlayerPrefs.GetFloat ("SOUNDVOLUME");
	}

	public void Start(){
		TransitionIn ();
	}

	public void TransitionIn(){
		backButtonAnimator.SetBool ("BounceIn", true);
		optionBoardAnimator.SetBool ("BounceIn", true);
	}
	public void TransitionOut(){
		backButtonAnimator.SetBool ("BounceIn", false);
		optionBoardAnimator.SetBool ("BounceIn", false);

		backButtonAnimator.SetBool ("BounceOut", true);
		optionBoardAnimator.SetBool ("BounceOut", true);
	}

	void Update(){
		PlayerPrefs.SetFloat ("SOUNDVOLUME", slider.value);
		AudioListener.volume = PlayerPrefs.GetFloat ("SOUNDVOLUME");
		slider.value = PlayerPrefs.GetFloat ("SOUNDVOLUME");
	}

	public void GoToMainMenuScreen(){
		TransitionOut ();
		Invoke ("ToMainMenuScreen", 1f);
	}

	void ToMainMenuScreen(){
		Application.LoadLevel ("MainMenuScreen");
	}
}
