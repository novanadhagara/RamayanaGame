using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public AudioClip[] BGM;
	private AudioSource audioSource;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        if(!audioSource.playOnAwake){
            PlayRandom(BGM);
        }
	}

	 void LateUpdate() {
        if(!audioSource.isPlaying){
            PlayRandom(BGM);
        }
    }
	
	 void PlayRandom(AudioClip[] clips){
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }
}
