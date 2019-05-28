using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public Animator starsBoardAnimator;

	public GameObject blurry;
	public GameObject star1, star2, star3;

	public bool gameOver;

	void Awake(){
		PlayerPrefs.SetString("IsGameOver","false");
	}
	void Start () {
		gameOver = false;
		blurry.SetActive (false);
		star1.SetActive (false);
		star2.SetActive (false);
		star3.SetActive(false);

		starsBoardAnimator.SetBool("BounceIn",false);
		starsBoardAnimator.SetBool("BounceOut",false);
	}
	public void TransitionIn(){
		starsBoardAnimator.SetBool("BounceIn",true);
		starsBoardAnimator.SetBool("BounceOut",false);
	}

	public void TransitionOut(){
		starsBoardAnimator.SetBool("BounceOut",true);
		starsBoardAnimator.SetBool("BounceIn",false);

	}

	void Update(){
		if(!gameOver){
			if (Player._instance.CurrentHealth <= 0 || Player._instance.reachCheckpoint) {
				blurry.SetActive (true);
				GameOver();	
				TransitionIn();
			}
		}
	}

	string CalculateStars(){
		//SHOULD REPLACED BY ARD SYSTEM, BASED ON NEW DIFFICULTY
		string Difficulty = PlayerPrefs.GetString("DIFFICULTY");
		/*if (Player._instance.CurrentWisdom == 0)
			Difficulty = "BEGINNER";
		else if (Player._instance.CurrentWisdom == 1)
			Difficulty = "MEDIUM";
		else if (Player._instance.CurrentWisdom == 2)
			Difficulty = "HARD";
		else if (Player._instance.CurrentWisdom > 2)
			Difficulty = "VERY HARD";
		else Difficulty ="BEGINNER";
*/
		return Difficulty;
	}

	void ShowStars(string Difficulty){
		//SHOULD BASED ON NEW DIFFICULTY
		if (Difficulty == ("MEDIUM")){
			star1.SetActive(true);
		}
		if (Difficulty == ("HARD")) {
			star1.SetActive(true);
			star2.SetActive(true);
		}
		if (Difficulty == ("VERY HARD")) {
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
		}

		//WRITE TO THE PLAYERPREFS
	}

	public void GameOver(){
		PlayerPrefs.SetInt ("CURRENT_TRY", Player._instance.GetTRY());
		gameOver = true;
		//TransitionIn ();
		Debug.Log ("REACH CHECK POINT ------------------------------------------" + Player._instance.reachCheckpoint);

		//IF REACH THE CHECKPOINT
		if (Player._instance.reachCheckpoint == true) {
			string Difficulty = CalculateStars ();
			ShowStars (Difficulty);
		}

		PlayerPrefs.SetString("IsGameOver","true");
	}

	public void GoToMainMenuScreen(){
		PlayerPrefs.SetString("IsGameOver","false");
		PlayerPrefs.SetString("IsPaused", "false");
		TransitionOut ();
		Invoke ("ToMainMenuScreen", 1f);
	}
	void ToMainMenuScreen(){
		Application.LoadLevel ("MainMenuScreen");
	}
	
	public void RestartLevel(){
		PlayerPrefs.SetString("IsGameOver","false");
		PlayerPrefs.SetFloat("CURRENT_TRY", Player._instance.GetTRY());
		PlayerPrefs.SetString("IsPaused", "false");
		TransitionOut ();
		Invoke ("ToRestartLevel", 1f);
		
	}
	void ToRestartLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void GoToLevelMenu(){
		PlayerPrefs.SetString("IsGameOver","false");
		TransitionOut ();
		Invoke ("ToLevelMenu", 1f);
	}
	void ToLevelMenu(){
		Application.LoadLevel("LevelScreen");
	}
}
