using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public GameObject BackgroundMusic;
	private AudioSource source;

	void Awake(){
		GameObject instance = Instantiate (BackgroundMusic, Vector3.zero, Quaternion.identity) as GameObject;
		source = instance.GetComponent<AudioSource> ();
		source.Play ();
		DontDestroyOnLoad (instance);
	}
}
