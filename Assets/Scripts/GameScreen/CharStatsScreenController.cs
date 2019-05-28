using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharStatsScreenController : MonoBehaviour {

	public Animator characterBoardAnimator, backButtonAnimator;
	public GameObject CharStats, PlayerStatus, WisdomShop;
	public GameObject Blurry, backbutton, charboard;
	public Text StatusText, StatusTextShadow, DifHUD, DifHUDShadow;
	public Button[] Weapons;
	public Sprite[] bowSprites;
	public SpriteRenderer bowSprite;
	
	public void Start(){
		CharStats.SetActive (true);
		PlayerStatus.SetActive (false);
		WisdomShop.SetActive (false);
		
		if (PlayerPrefs.GetString ("DIFFICULTY") == "UNDEFINED" || PlayerPrefs.GetString ("DIFFICULTY") == "") {
			PlayerPrefs.SetString("DIFFICULTY", "BEGINNER");
		}

		characterBoardAnimator.SetBool ("BounceIn", false);
		backButtonAnimator.SetBool ("BounceIn", false);
		characterBoardAnimator.SetBool ("BounceOut", false);
		backButtonAnimator.SetBool ("BounceOut", false);
		ShakeOff();

		UpdateChoosenWeapon();
	}
	
	public void TransitionIn(){
		Blurry.SetActive (true);
		characterBoardAnimator.SetBool ("BounceIn", true);
		backButtonAnimator.SetBool ("BounceIn", true);

		characterBoardAnimator.SetBool ("BounceOut", false);
		backButtonAnimator.SetBool ("BounceOut", false);
	}

	public void TransitionOut(){
		characterBoardAnimator.SetBool ("BounceOut", true);
		backButtonAnimator.SetBool ("BounceOut", true);

		characterBoardAnimator.SetBool ("BounceIn", false);
		backButtonAnimator.SetBool ("BounceIn", false);

		Invoke("BackToGame",1f);
	}
	void BackToGame(){
		Blurry.SetActive (false);
	}

	void Update(){
		//SHOULD BE REMOVE, hanya bisa tampil saat sudah gameover
		StorePrefs ();
		ShowPrefsText ();
		UpdateDifHUD ();
	}

	void UpdateDifHUD(){
		DifHUD.text = ""+PlayerPrefs.GetString("DIFFICULTY");
		DifHUDShadow.text = DifHUD.text;
	}

	public void ShowCharacterBoard(){
		TransitionIn();
	}

	public void ShowCharStats(){
		Shake();
		CharStats.SetActive (true);
		PlayerStatus.SetActive (false);
		WisdomShop.SetActive (false);
		Invoke("ShakeOff",0.5f);
	}
	
	public void ShowPlayerStatus(){
		WisdomShop.SetActive (false);
		CharStats.SetActive (false);
		PlayerStatus.SetActive (true);
		StorePrefs ();
		ShowPrefsText ();
		Shake();
		Invoke("ShakeOff",0.5f);
	}
	
	public void ShowWisdomShop(){
		Shake();
		CharStats.SetActive (false);
		PlayerStatus.SetActive (false);
		WisdomShop.SetActive (true);
		Invoke("ShakeOff",0.5f);
	}

	public void Back(){
		TransitionOut ();
	}

	void Shake(){
		characterBoardAnimator.SetBool ("Shake", true);
	}
	void ShakeOff(){
		characterBoardAnimator.SetBool ("Shake", false);
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
		PlayerPrefs.SetFloat("CURRENT_TRY", Player._instance.GetTRY());
		//PlayerPrefs.SetString ("DIFFICULTY", PlayerPrefs);
		//SHOW CALCULATED FUZZY HERE
		//call the fuzzy to store difficulty here/ should be in checkpoint.cs
	}
	public void ShowPrefsText(){
		StatusText.text = "" + PlayerPrefs.GetFloat ("CURRENT_TRY") + "x \n" +
			"" + PlayerPrefs.GetFloat ("ACCURACY") + "% \n" +
			"" + PlayerPrefs.GetFloat ("TIME") + "second \n" + 
			"" + PlayerPrefs.GetFloat ("SOLVE") + "% \n" +
			"" + PlayerPrefs.GetString("DIFFICULTY") + "\n" +
			"" + PlayerPrefs.GetFloat ("HP") + " %\n";
		StatusTextShadow.text = StatusText.text;
	}

	public void ChooseWeapon(int weaponIndex){
		PlayerPrefs.SetInt("Weapons",weaponIndex);
		UpdateChoosenWeapon();
	}

	void UpdateChoosenWeapon(){
		for(int i=0;i<Weapons.Length;i++){
			if(PlayerPrefs.GetInt("Weapons")==i){
				Weapons[i].interactable = false;
				bowSprite.sprite = bowSprites[i];
			}else {
				Weapons[i].interactable = true;
			}
		}
        UpdateSwordSprite();

    }

    void UpdateSwordSprite() {
        if (bowSprite.sprite.name == "sword"){
            bowSprite.flipY = true;
        }
        else {
            bowSprite.flipY = false;
        }
    }
}
