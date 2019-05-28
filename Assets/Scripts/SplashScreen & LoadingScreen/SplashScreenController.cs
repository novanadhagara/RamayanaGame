using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour {


	private Image splash;
	private float alpha = 0;
	private float timer = 0;
	private bool alphaReached=false;

	void Awake(){
		Screen.autorotateToPortrait = false;
		Screen.orientation = ScreenOrientation.Landscape;
	}
	// Use this for initialization
	void Start () {

		splash = GetComponent<Image>();
		splash.color = new Color (splash.color.r, splash.color.g, splash.color.a, 0.0f);

	}

	// Update is called once per frame
	void Update () {
		if (alpha <= 1 && !alphaReached) {
			alpha+= 0.1f * Time.deltaTime*5; 	//splash.color = Color.Lerp(splash.color, new Color(splash.color.r,splash.color.g,splash.color.b,alpha), Time.deltaTime);
		}else if(timer >= 2 && alphaReached){
			alpha-= 0.1f * Time.deltaTime*5;
			if(alpha <= 0){
				//timer = 0;
				//Debug.Log(timer);
				timer += Time.deltaTime;
				if(timer >= 4){ //timer lanjut dari 2 ke 4
					//move to Loading screen
					Application.LoadLevel("LoadingScreen");
				}
			}									//splash.color = Color.Lerp(splash.color, new Color(splash.color.r,splash.color.g,splash.color.b,alpha), Time.deltaTime);
		}else if(alpha >= 1){
			timer += Time.deltaTime;
			alphaReached = true;
			alpha =1;
		}
		splash.color = Color.Lerp(splash.color, new Color(splash.color.r,splash.color.g,splash.color.b,alpha), Time.deltaTime);
	}

}
