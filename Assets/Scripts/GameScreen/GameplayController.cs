using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	public Text TimerText;

	public float MaxTimeSecond;
	public float CurrentTimeLeft;

	public bool isPaused;

	void Start(){
		isPaused = false;
		UpdateText ();
		CurrentTimeLeft = MaxTimeSecond; 
	}

	void UpdateText(){
		TimerText.text = ""+(int)CurrentTimeLeft / 60+":"+(int)CurrentTimeLeft % 60 +"";
		Player._instance.TimeLeft = (int)CurrentTimeLeft;
	}

	void FixedUpdate(){
		if (!isPaused && (PlayerPrefs.GetString("IsGameOver") != "true") ) {
			if(CurrentTimeLeft > 0){
				CurrentTimeLeft -= Time.deltaTime;
			}else if(CurrentTimeLeft <= 0){
				CurrentTimeLeft = 0;
			}
			UpdateText();
		}
	}

	public void Pause(){
		isPaused = true;
		PlayerPrefs.SetString("IsPaused", "true");
	}

	public void Resume(){
		isPaused = false;
		PlayerPrefs.SetString("IsPaused", "false");
	}
}
