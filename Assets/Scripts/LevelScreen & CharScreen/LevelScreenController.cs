using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelScreenController : MonoBehaviour {

	public GameObject backButton, nextButton, levelBoard, levelBoard2, loadingText, storyText;
	public GameObject StoryBoard;
	public GameObject[] StoryImages;

	public Animator backButtonAnimator, nextButtonAnimator, levelBoardAnimator,levelBoard2Animator, storyBoardAnimator;
	public Text storyTextFront, storyTextShadow;

	private int Level;
	private int storyCounter;
	private bool textShown;

    public GameObject[] levels;
    public int indexLevels;

    void ReviewLevelBoard() {
        if (indexLevels > levels.Length - 1)
        {
            indexLevels = levels.Length - 1;
        }
        int i = 0;
        while (i < levels.Length - 1)
        {

            levels[i].SetActive(false);
            i++;
        }
        levels[indexLevels].SetActive(true);
    }

    public void NextLevelBoard() {
        indexLevels++;
        if (indexLevels > levels.Length-1) {
            indexLevels = levels.Length - 1;
        }
        int i = 0;
        while (i < levels.Length-1) {
           
            levels[i].SetActive(false);
            i++;
        }
        PlayerPrefs.SetInt("indexLv", indexLevels);
        levels[indexLevels].SetActive(true);
    }

    public void PrevLevelBoard()
    {
        indexLevels--;
        if (indexLevels < 0)
        {
            TransitionOut();
            Invoke("ToMainMenuScreen", 1f);
            indexLevels = 0;
            return;
        }

        int i = 0;
        while (i < levels.Length)
        {
           levels[i].SetActive(false);
            i++;
        }
        levels[indexLevels].SetActive(true);
        PlayerPrefs.SetInt("indexLv", indexLevels);

    }

    void Awake (){
		textShown = false;

		loadingText.SetActive (false);
		StoryBoard.SetActive (false);
		nextButton.SetActive (false);
		storyText.SetActive (false);
    
		storyCounter = 0;
		/*int i;
		for (i = 0; i < StoryImages.Length; i++) {
			if(i!=storyCounter){
				StoryImages[i].SetActive(false);
			}else {
				StoryImages[i].SetActive(true);
			}
		}*/
		ShowStoryImage ();
        if (PlayerPrefs.HasKey("indexLv")) {
            indexLevels = PlayerPrefs.GetInt("indexLv");
        }
        ReviewLevelBoard();
	}
	
	public void Start(){
		TransitionIn ();
	}
	
	public void TransitionIn(){
		backButtonAnimator.SetBool ("BounceIn", true);
		levelBoardAnimator.SetBool ("BounceIn", true);
	}
	public void TransitionOut(){
		backButtonAnimator.SetBool ("BounceIn", false);
		levelBoardAnimator.SetBool ("BounceIn", false);
        levelBoard2Animator.SetBool("BounceIn", false);
		
		backButtonAnimator.SetBool ("BounceOut", true);
		levelBoardAnimator.SetBool ("BounceOut", true);
        levelBoard2Animator.SetBool("BounceOut", true);
	}
	
	public void GoToMainMenuScreen(){
		TransitionOut ();
		Invoke ("ToMainMenuScreen", 1f);
	}
	void ToMainMenuScreen(){
		Application.LoadLevel ("MainMenuScreen");
	}
	
	public void ShowStoryBoard(){
		TransitionOut ();
		Invoke ("ShowStory", 1f);
	}
	void ShowStory(){
		StoryBoard.SetActive (true);
		storyBoardAnimator.SetBool ("BounceOut",false);
		storyBoardAnimator.SetBool ("BounceIn",true);
		//bounce in story false

		nextButton.SetActive (true);
		nextButtonAnimator.SetBool ("BounceOut",false);
		nextButtonAnimator.SetBool ("BounceIn",true);

	}
	public void ShowStoryBoardOff(){
		storyBoardAnimator.SetBool ("BounceIn",false);
		storyBoardAnimator.SetBool ("BounceOut",true);

		nextButtonAnimator.SetBool ("BounceIn",false);
		nextButtonAnimator.SetBool ("BounceOut",true);
	}

	public void addCounter(){
		storyBoardAnimator.SetBool ("BounceIn", false);
		Shake ();

		if (!textShown) {
			textShown = true;
		} else {
			textShown = false;
			storyCounter++;

			if (storyCounter >= 8) {
				ShowStoryBoardOff();
				loadingText.SetActive(true);
				GoToLevel();
			}
		}
		ShowStoryImage ();
		Invoke ("ShakeOff", 0.5f);
	}

    public void GoToLevel()
    {
       
       // ShowStoryBoard();
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel", 1f);
    }
    void Tolevel()
    {
        Application.LoadLevel("Level");
    }


	public void GoToLevel1(){
		TransitionOut();
		loadingText.SetActive(true);
		Invoke ("ToLevel1",1f);
	}
	void ToLevel1(){
		Application.LoadLevel ("Level1");
	}

	public void GoToLevel2(){
		TransitionOut();
		loadingText.SetActive(true);
		Invoke("ToLevel2",1f);
	}
	void ToLevel2(){
		Application.LoadLevel ("Level2");
	}

	public void GoToLevel3(){
		TransitionOut();
		loadingText.SetActive(true);
		Invoke("ToLevel3",1f);
	}
	void ToLevel3(){
		Application.LoadLevel ("Level3");
	}

    public void GoToLevel4()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel4", 1f);
    }
    void ToLevel4()
    {
        Application.LoadLevel("Level4");
    }

    public void GoToLevel5()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel5", 1f);
    }
    void ToLevel5()
    {
        Application.LoadLevel("Level5");
    }

    public void GoToLevel6()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel6", 1f);
    }
    void ToLevel6()
    {
        Application.LoadLevel("Level6");
    }

    public void GoToLevel7()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel7", 1f);
    }
    void ToLevel7()
    {
        Application.LoadLevel("Level7");
    }

    public void GoToLevel8()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel8", 1f);
    }
    void ToLevel8()
    {
        Application.LoadLevel("Level8");
    }

    public void GoToLevel9()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel9", 1f);
    }
    void ToLevel9()
    {
        Application.LoadLevel("Level9");
    }

    public void GoToLevel10()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel10", 1f);
    }
    void ToLevel10()
    {
        Application.LoadLevel("Level10");
    }

    public void GoToLevel11()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel11", 1f);
    }
    void ToLevel11()
    {
        Application.LoadLevel("Level11");
    }

    public void GoToLevel12()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel12", 1f);
    }
    void ToLevel12()
    {
        Application.LoadLevel("Level12");
    }

    public void GoToLevel13()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel13", 1f);
    }
    void ToLevel13()
    {
        Application.LoadLevel("Level13");
    }

    public void GoToLevel14()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel14", 1f);
    }
    void ToLevel14()
    {
        Application.LoadLevel("Level14");
    }

    public void GoToLevel15()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel15", 1f);
    }
    void ToLevel15()
    {
        Application.LoadLevel("Level15");
    }

    public void GoToLevel16()
    {
        TransitionOut();
        loadingText.SetActive(true);
        Invoke("ToLevel16", 1f);
    }
    void ToLevel16()
    {
        Application.LoadLevel("Level16");
    }


    public void ShowStoryImage(){
		//check what story should be showen
		//check , add more arrays of story images
		storyText.SetActive (textShown);
		
		int i ;
		for (i = 0; i < StoryImages.Length; i++) {
			if(i!=storyCounter){
				StoryImages[i].SetActive(false);
			}else {
				StoryImages[i].SetActive(true);
			}
		}
		Debug.Log (""+storyCounter);
		
		if( storyCounter == 0){
			storyTextFront.text = "Jaman dahulu kala, Dewa Rama dan Dewi Shinta hidup bahagia.";
			storyTextShadow.text = "Jaman dahulu kala, Dewa Rama dan Dewi Shinta hidup bahagia.";
		}else if(storyCounter == 1){
			storyTextFront.text = "Mereka hidup dengan penuh kasih sayang dan cinta.";
			storyTextShadow.text = "Mereka hidup dengan penuh kasih sayang dan cinta.";
		}else if(storyCounter ==2){
			storyTextFront.text = "Suatu hari, seorang raksasa bernama Rahwana datang. " +
				"Rahwana iri akan kisah kasih sayang mereka.";
			storyTextShadow.text = "Suatu hari, seorang raksasa bernama Rahwana datang. " +
				"Rahwana iri akan kisah kasih sayang mereka.";
		}else if (storyCounter ==3){
			storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
			storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
		}else if (storyCounter == 4) {
            storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
            storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
        }
        else if (storyCounter == 5){
            storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
            storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
        }
        else if (storyCounter == 6){
            storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
            storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
        }
        else if (storyCounter == 7){
            storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
            storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
        }
        else if (storyCounter == 8){
            storyTextFront.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
            storyTextShadow.text = "Akhirnya Rahwana menculik Shinta. Rama harus mencari Shinta yang diculik.";
        }


    }

	void Shake(){
		storyBoardAnimator.SetBool ("Shake", true);
	}

	void ShakeOff(){
		storyBoardAnimator.SetBool ("Shake", false);
	}
}
