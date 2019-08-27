using UnityEngine;
using System.Collections;

public class StoryEndController : MonoBehaviour {

    public GameObject StoryBoard, NextButton;

    public Animator StoryBoardAnim, NextButtonAnim;

    public bool isDeadPlayer;

    public void GetDeadPlayerStatus(bool status){
        isDeadPlayer = status;
    }

	void Awake(){
        //CheckPoint.SetActive(false);
		StoryBoard.SetActive(false);
		NextButton.SetActive(false);
	}

	public void StoryEndIn(){
		
        if (!isDeadPlayer)
        {
            NextButton.SetActive(true);
               if (StoryBoard) {
                  
                   StoryBoard.SetActive(true);                   
                   StoryBoardAnim.SetBool("BounceIn", true);
               }
            
              NextButtonAnim.SetBool("BounceIn", true);
           }
        else {
            GameObject gameovercontroller = GameObject.Find("GameOverController");
           gameovercontroller.GetComponent<GameOverController>().GoToLevelMenu();
        }
        
	}

	public void StoryEndOut(){
		StoryBoardAnim.SetBool("BounceIn",false);
		NextButtonAnim.SetBool("BounceIn",false);

		StoryBoardAnim.SetBool("BounceOut",true);
		NextButtonAnim.SetBool("BounceOut",true);
    }
}