using UnityEngine;
using System.Collections;

public class StoryEndController : MonoBehaviour {

	public GameObject StoryBoard, NextButton;

	public Animator StoryBoardAnim, NextButtonAnim;

	void Awake(){
		StoryBoard.SetActive(false);
		NextButton.SetActive(false);
	}

	public void StoryEndIn(){
		StoryBoard.SetActive(true);
		NextButton.SetActive(true);

		StoryBoardAnim.SetBool("BounceIn",true);
		NextButtonAnim.SetBool("BounceIn",true);
	}

	public void StoryEndOut(){
		StoryBoardAnim.SetBool("BounceIn",false);
		NextButtonAnim.SetBool("BounceIn",false);

		StoryBoardAnim.SetBool("BounceOut",true);
		NextButtonAnim.SetBool("BounceOut",true);
	}
}
