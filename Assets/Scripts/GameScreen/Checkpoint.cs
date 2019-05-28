using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	private bool Checked;

	void Start(){
		Checked = false;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //GAME OVER/ WIN SESSION HERE
        // SHOW THE DEBUG.LOG OF STATUS / TRANSITIONIN CHAR STATS SCREEN

        //if (!Checked) {
        //	Checked=true;
        //	Player._instance.reachCehckpoint = true;
        //}
        //if (collider.gameObject.name == "Player Character") {
        //	Animator.SetTrigger("Animate");
        //}

        //StorePrefs ();
    
        //Debug.Log ("Player	 prefs- Time:"+PlayerPrefs.GetInt("TIME")+" ,Wisdom"+PlayerPrefs.GetInt("WISDOM")+" ,HP"+PlayerPrefs.GetFloat("HP")
           //     +" ,Solved"+PlayerPrefs.GetFloat("SOLVE")+" ,Accuracy"+PlayerPrefs.GetFloat("ACCURACY")+" ,CurrentTry"+Player._instance.GetTRY()
            ///    +" ,DIF "+ PlayerPrefs.GetString("DIFFICULTY"));

      
    }
public void StorePrefs(){
if (Player._instance.TotalArrowFire > 0) {
 PlayerPrefs.SetFloat ("ACCURACY", Mathf.RoundToInt (100 * Player._instance.CurrentArrowHit / Player._instance.TotalArrowFire));
} else {
 PlayerPrefs.SetFloat("ACCURACY", 0);
}
PlayerPrefs.SetFloat ("TIME", Player._instance.TimeLeft);
PlayerPrefs.SetFloat ("SOLVE",  Mathf.RoundToInt(100 * Player._instance.CurrentSolves / Player._instance.MaxSolves));
PlayerPrefs.SetFloat ("WISDOM", Player._instance.CurrentWisdom);
PlayerPrefs.SetFloat ("HP", Mathf.RoundToInt(100 * Player._instance.CurrentHealth / Player._instance.MaxHealth));
PlayerPrefs.SetFloat ("CURRENT_TRY", Player._instance.GetTRY());
}
}
