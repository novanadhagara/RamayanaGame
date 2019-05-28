using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {
	private float timer = 0;
	private float acc = 0.001f;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		transform.localScale = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3(0, 0, -2) ;//* Time.deltaTime;
		timer += Time.deltaTime;
		
		if (timer >= 3) {
			//move to another scene
			Application.LoadLevel("MainMenuScreen");
		}
		
		if (transform.localScale.x <= 1) {
			transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f) * Time.deltaTime * acc;
			acc++;
		}
	}
}
