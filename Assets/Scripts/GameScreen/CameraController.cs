using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject bgMusic;
	public BoxCollider2D Bounds;

	public Transform Player;

	private Vector3 _min, _max;
	public Vector2 Margin;
	public Vector2 Smoothing;

	public bool isFollowing;

	public void Start(){
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		isFollowing = true;
		bgMusic = GameObject.Find ("GaMetal21(Clone)");
		//bgMusic.GetComponent<AudioSource> ().Stop ();
	}

	public void Update(){
		var x = transform.position.x;
		var y = transform.position.y;

		if (this.Player != null) {
			if (isFollowing) {
				if(Mathf.Abs(x - Player.position.x) > Margin.x){
					x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
				}
				
				if(Mathf.Abs(y - Player.position.y) > Margin.y){
					y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
				}
			}
			
			var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
			x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
			y = Mathf.Clamp (y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);
			
			transform.position = new Vector3 (x,y,transform.position.z);
		}

	}
}
