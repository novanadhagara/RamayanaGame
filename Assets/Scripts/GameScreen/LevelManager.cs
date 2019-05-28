using UnityEngine;
using System;
using System.Collections;

public class LevelManager : MonoBehaviour {


	public static LevelManager Instance { get; private set;}
	public Player Player { get; private set;}
	public CameraController Camera { get; private set;}

	public TimeSpan RunningTime {get {return DateTime.UtcNow - _started;}}
	private DateTime _started;

	public void Awake(){
		Instance = this;
		
	}
	public void Start () {
		Player = FindObjectOfType<Player> ();
		Camera = FindObjectOfType<CameraController> ();
		
		_started = DateTime.UtcNow;
	}

	public void Update () {
		_started = DateTime.UtcNow;
	}

	public void KillPlayer(){
		StartCoroutine (KillPlayerCo ());
	}
	
	private IEnumerator KillPlayerCo(){
		Player.Die ();
		Camera.isFollowing = false;
			yield return new WaitForSeconds (2f);
		Camera.isFollowing = true;

		//todo points
		_started = DateTime.UtcNow;
		//GameManager.Instance.ResetPoints (_savedPoints);
	}

}
