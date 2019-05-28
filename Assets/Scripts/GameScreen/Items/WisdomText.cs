using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WisdomText : MonoBehaviour {
	public static WisdomText instance = null;

	public Animator animator;
	public Text text;

	public Animator animator1;
	public Text text1;

	void Awake(){
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		text.text="";
		text1.text="";
		//text = gameObject.GetComponent<Text>();

		//animator = gameObject.GetComponent<Animator>();
		animator.SetBool("FadeIn",false);
		animator1.SetBool("FadeIn",false);
		RefreshID();
		RefreshID1();

	}

	public void SetText(string wisdomText){
		if(!wisdomText.Equals("")){
			if(text.text==""){
				text.text = wisdomText;
				FadeIn();
				Debug.Log("MASUK SATU");
				Invoke ("FadeOut",2f);
			}else if(text1.text==""){
				text1.text = wisdomText;
				FadeIn1();
				Debug.Log("MASUK DUA");
				Invoke ("FadeOut1",2f);
			}
		}
	}

	void FadeIn(){
		animator.SetBool("FadeIn",true);
	}
	
	void FadeOut(){
		animator.SetBool("FadeIn",false);
		Invoke("RefreshID",1.5f);
	}

	void RefreshID(){
		text.text = "";
	}
	//-----------------
	void FadeIn1(){
		animator1.SetBool("FadeIn",true);
	}
	
	void FadeOut1(){
		animator1.SetBool("FadeIn",false);
		Invoke("RefreshID1",1.5f);
	}
	
	void RefreshID1(){
		text1.text = "";
	}

}
