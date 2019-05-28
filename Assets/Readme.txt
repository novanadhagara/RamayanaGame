
//note
//CHARSCREENCONTROLLER TIDAK DIPAKAI
//yang belum dibuka di gamescreen items, terrain, character
//check player.cs untuk max value dari max solve

Yang Kurang

1. Background Level 3
2. Story VVVVVVVVVVVV


//TEMPLATE KURVA PHI
----------------------------------------------------------------------
		if (X<= A || X >= E) {
			u_HP_CUKUP = 0;
		} else if (X >= A && X <= B) {
			u_HP_CUKUP = 2 * (Mathf.Pow (((X - A) / (C - A)), 2));
		} else if (X >= B && X <= C) {
			u_HP_CUKUP = 1 - 2 * (Mathf.Pow (((C - X) / (C - A)), 2));
		} else if (X == C) {
			u_HP_CUKUP = 1;
		} else if (X >= C && X <= D) {
			u_HP_CUKUP = 1 - 2 * (Mathf.Pow (((X - C) / (E - C)), 2));
		} else if (X >= D && X <= E) {
			u_HP_CUKUP = 2 * (Mathf.Pow (((E - X) / (E - C)), 2));
		}
----------------------------------------------------------------------
Bookmark : Try belum, difficulty belum


Enemy
------------
box collider, rigid body,  

Scripts : character controller, simpleenemyAI, give damagetoplayer, arrow path spawner
Prefab : patharrow, explossion

* note : tembakan di simple enemy AI diganti dengan PathArrow (agar nanti bisa dimodifikasi arah tembakkannya)

PARAMETER AI
INPUT :
HP
TIME
SOLVE
ACCURACY
DIFFICULTY
TRY

OUTPUT :
difficulty
reward
ai/npc
hint


BACKUP
using UnityEngine;
using System.Collections;

public class ARD : MonoBehaviour {

	//public static ARD Instance;
	//INPUT
	public float HP { get; private set;}
	public float TIME { get; private set;}
	public float TRY { get; private set;}
	public float SOLVE { get; private set;}
	public float ACCURACY { get; private set;}
	public string INPUT_DIFFICULTY { get; private set;}


	//INFERENSI
	public float u_HP_SEDIKIT{ get; private set;}
	public float u_HP_CUKUP{ get; private set;}
	public float u_HP_BANYAK{ get; private set;}
	public float u_HP_PENUH{ get; private set;}

	public float u_TIME_LAMBAT{ get; private set;}
	public float u_TIME_AGAKLAMBAT{ get; private set;}
	public float u_TIME_CEPAT{ get; private set;}
	public float u_TIME_SEMPURNA{ get; private set;}
	
	public float u_SOLVE_RENDAH{ get; private set;}
	public float u_SOLVE_CUKUP{ get; private set;}
	public float u_SOLVE_TINGGI{ get; private set;}
	public float u_SOLVE_PENUH{ get; private set;}

	public float u_ACCURACY_SANGATRENDAH{ get; private set;}
	public float u_ACCURACY_RENDAH{ get; private set;}
	public float u_ACCURACY_CUKUP{ get; private set;}
	public float u_ACCURACY_TINGGI{ get; private set;}

	public string u_LASTDIFFICULTY { get; private set;}
	/**public string u_LASTDIFFICULTY_BEGINNER{ get; private set;}
	public string u_LASTDIFFICULTY_MEDIUM{ get; private set;}
	public string u_LASTDIFFICULTY_HARD{ get; private set;}
	public string u_LASTDIFFICULTY_VERYHARD{ get; private set;}
*/
	public float u_TRY_TIDAKPERNAH{ get; private set;}
	public float u_TRY_JARANG{ get; private set;}
	public float u_TRY_SERING{ get; private set;}
	public float u_TRY_SANGATSERING{ get; private set;}

	//OUTPUT
	private string OUTPUT_DIFFICULTY{ get; set;}

	//GET, SET, RESET, ADD VALUE, REDUCE VALUE, GET ALL ATTRIBUTE, SET ALL ATTRIBUTE, UPDATE VALUE, 
	//on trigger enter (checkpoint)
	//METHOD

	void Start(){
	
	}

	private void SetAttribute(float HP, float TIME, float TRY, float SOLVE, float ACCURACY){//, string DIFFICULTY){
		this.HP = HP;
		this.TIME = TIME;
		this.TRY = TRY;
		this.SOLVE = SOLVE;
		this.ACCURACY = ACCURACY;
		//this.INPUT_DIFFICULTY = DIFFICULTY;
	}

	public void ARDSystem(){

	}

	public void Get_u(){
		SetAttribute (PlayerPrefs.GetFloat("HP"), (float)PlayerPrefs.GetInt("TIME"), 
		              (float)PlayerPrefs.GetInt("TRY"), PlayerPrefs.GetFloat("SOLVE"), 
		              PlayerPrefs.GetFloat ("ACCURACY")
		              ); //PlayerPrefs.GetString(""));
		Set_u_HP ();
		Set_u_TIME ();
		Set_u_TRY ();
		Set_u_SOLVE ();
		Set_u_ACCURACY ();
		//Set_u_DIFFICULTY ();

		//DEBUGGING
		Debug.Log ("HP sedikit:"+u_HP_SEDIKIT+" HP cukup"+u_HP_CUKUP+" HP banyak"+u_HP_BANYAK+" HP Penuh"+u_HP_PENUH); 	//HP
		Debug.Log ("TRY tidakpernah:"+u_TRY_TIDAKPERNAH+" TRY jarang"+u_TRY_JARANG+" TRY sering"+u_TRY_SERING+" TRY sangat sering"+u_TRY_SANGATSERING);	//TRY
		Debug.Log ("TIME sempurna:"+u_TIME_SEMPURNA+" TIME cepat"+u_TIME_CEPAT+" TIME agaklambat"+u_TIME_AGAKLAMBAT+" TIME lambat"+u_TIME_LAMBAT); 	//TIME
		Debug.Log ("SOLVE rendah:"+u_SOLVE_RENDAH+" SOLVE cukup"+u_SOLVE_CUKUP+" SOLVE tinggi"+u_SOLVE_TINGGI+" SOLVE penuh"+u_SOLVE_PENUH); 	//SOLVE
		Debug.Log ("ACCURACY sangatrendah:"+u_ACCURACY_SANGATRENDAH+" ACCURACY rendah"+u_ACCURACY_RENDAH+" ACCURACY cukup"+u_ACCURACY_CUKUP+" ACC tinggi"+u_ACCURACY_TINGGI); 	//accuracy
	}

	//FUZZIFIKASI
	public void Set_u_HP(){
		//U_HP_SEDIKIT
		if (HP <= 30) {
			u_HP_SEDIKIT = 1;
		} else if (HP >= 30 && HP <= 40) {
			u_HP_SEDIKIT = 1 - 2 * (Mathf.Pow (((HP - 30) / (60 - 30)), 2));
		} else if (HP >= 40 && HP <= 60) {
			u_HP_SEDIKIT = 2 * (Mathf.Pow (((60 - HP) / (60 - 30)), 2));
		} else if (HP >= 60) {
			u_HP_SEDIKIT = 0;
		}

		//U_HP_CUKUP
		if (HP <= 50) {
			u_HP_CUKUP = 1 - 2 * (Mathf.Pow (((50 - HP) / (50 - 30)), 2));
		} else if (HP > 50) {
			u_HP_CUKUP = 2 * (Mathf.Pow(((70 - HP) / (70 - 50)),2));
		}

		//U_HP_BANYAK
		if (HP <= 75) {
			u_HP_BANYAK = 1 - 2 * (Mathf.Pow (((75 - HP) / (75 - 55)), 2));
		} else if (HP > 75) {
			u_HP_BANYAK = 2 * (Mathf.Pow(((95 - HP) / (95 - 75)), 2));
		}

		//U_HP_PENUH
		if (HP < 100) {
			u_HP_PENUH = 0;
		}else if (HP >= 100) {
			u_HP_PENUH = 1;
		}
	}

	public void Set_u_TIME(){
		//u_TIME_LAMBAT
		if (TIME <= 5) {
			u_TIME_LAMBAT = 1;
		} else if (TIME >= 5 && TIME <= 10) {
			u_TIME_LAMBAT = 1 - 2 * (Mathf.Pow (((TIME - 5) / (15 - 5)), 2));
		} else if (TIME >= 10 && TIME <= 15) {
			u_TIME_LAMBAT = 2 * (Mathf.Pow (((15 - TIME) / (15 - 5)), 2));
		} else if (TIME >= 15) {
			u_TIME_LAMBAT = 0;
		}

		//U_TIME_AGAKLAMBAT
		if (TIME <= 15) {
			u_TIME_AGAKLAMBAT = 1 - 2 * (Mathf.Pow (((15 - TIME) / (15 - 5)), 2));
		} else if (TIME > 15) {
			u_TIME_AGAKLAMBAT = 2 * (Mathf.Pow(((25 - TIME) / (25 - 15)), 2));
		}

		//U_TIME_CEPAT
		if (TIME <= 25) {
			u_TIME_CEPAT = 1 - 2 * (Mathf.Pow (((25 - TIME) / (25 - 15)), 2));
		} else if (TIME > 25) {
			u_TIME_CEPAT = 2 * (Mathf.Pow(((35 - TIME) / (35 - 25)), 2));
		}

		//u_TIME_SEMPURNA
		if (TIME <= 23) {
			u_TIME_SEMPURNA = 0;
		} else if (TIME >= 23 && TIME <= 28) {
			u_TIME_SEMPURNA = 2 * (Mathf.Pow (((TIME - 23) / (30 - 23)), 2));
		} else if (TIME >= 28 && TIME <= 30) {
			u_TIME_SEMPURNA = 1 - 2 * (Mathf.Pow (((30 - TIME) / (30 - 23)), 2));
		} else if (TIME >= 30) {
			u_TIME_SEMPURNA = 1;
		}
	}

	public void Set_u_SOLVE(){
		//u_SOLVE_RENDAH
		if (SOLVE <= 30) {
			u_SOLVE_RENDAH = 1;
		} else if (TIME >= 30 && TIME <= 45) {
			u_SOLVE_RENDAH = 1 - 2 * (Mathf.Pow (((SOLVE - 30) / (60 - 30)), 2));
		} else if (TIME >= 45 && TIME <= 60) {
			u_SOLVE_RENDAH = 2 * (Mathf.Pow (((60 - SOLVE) / (60 - 45)), 2));
		} else if (TIME >= 60) {
			u_SOLVE_RENDAH = 0;
		}

		//u_SOLVE_CUKUP
		if (SOLVE <= 60) {
			u_SOLVE_CUKUP = 1 - 2 * (Mathf.Pow (((60 - SOLVE) / (60 - 30)), 2));
		} else if (SOLVE > 60) {
			u_SOLVE_CUKUP = 2 * (Mathf.Pow(((90 - SOLVE) / (90 - 60)), 2));
		}

		//u_SOLVE_TINGGI
		if (SOLVE <= 85) {
			u_SOLVE_TINGGI = 1 - 2 * (Mathf.Pow (((85 - SOLVE) / (85 - 75)), 2));
		} else if (SOLVE > 85) {
			u_SOLVE_TINGGI = 2 * (Mathf.Pow(((100 - SOLVE) / (100 - 85)), 2));
		}

		//u_SOLVE_PENUH
		if (SOLVE < 100) {
			u_SOLVE_PENUH = 0;
		} else if (SOLVE >= 100) {
			u_SOLVE_PENUH = 1;
		}
	}

	public void Set_u_ACCURACY(){
		//u_ACCURACY_SANGATRENDAH
		if (ACCURACY <= 0) {
			u_ACCURACY_SANGATRENDAH = 1;
		} else if (ACCURACY >= 0 && ACCURACY <= 10) {
			u_ACCURACY_SANGATRENDAH = 1 - 2 * (Mathf.Pow (((ACCURACY - 0) / (20 - 0)), 2));
		} else if (ACCURACY >= 10 && ACCURACY <= 20) {
			u_ACCURACY_SANGATRENDAH = 2 * (Mathf.Pow (((20 - ACCURACY) / (20 - 10)), 2));
		} else if (ACCURACY >= 20) {
			u_ACCURACY_SANGATRENDAH = 0;
		}

		//u_ACCURACY_RENDAH
		if (ACCURACY <= 20) {
			u_ACCURACY_RENDAH = 1 - 2 * (Mathf.Pow (((20 - ACCURACY) / (20 - 0)), 2));
		} else if (ACCURACY > 20) {
			u_ACCURACY_RENDAH = 2 * (Mathf.Pow(((45 - ACCURACY) / (45 - 20)), 2));
		}

		//u_ACCURACY_CUKUP
		if (ACCURACY <= 45) {
			u_ACCURACY_CUKUP = 1 - 2 * (Mathf.Pow (((45 - ACCURACY) / (45 - 20)), 2));
		} else if (ACCURACY > 45) {
			u_ACCURACY_CUKUP = 2 * (Mathf.Pow(((75 - ACCURACY) / (75 - 45)), 2));
		}

		//u_ACCURACY_TINGGI
		if (ACCURACY <= 40) {
			u_ACCURACY_TINGGI = 0;
		} else if (ACCURACY >= 40 && ACCURACY <= 60) {
			u_ACCURACY_TINGGI = 2 * (Mathf.Pow (((ACCURACY - 40) / (70 - 40)), 2));
		} else if (ACCURACY >= 60 && ACCURACY <= 70) {
			u_ACCURACY_TINGGI = 1 - 2 * (Mathf.Pow (((70 - ACCURACY) / (70-40)), 2));
		} else if (ACCURACY >= 70) {
			u_ACCURACY_TINGGI = 1;
		}
	}

	public void Set_u_DIFFICULTY(){
		/*if (INPUT_DIFFICULTY.Equals ("BEGINNER")) {
			u_LASTDIFFICULTY = "BEGINNER";
		} else if (INPUT_DIFFICULTY.Equals ("MEDIUM")) {
			u_LASTDIFFICULTY = "MEDIUM";
		} else if (INPUT_DIFFICULTY.Equals ("HARD")) {
			u_LASTDIFFICULTY = "HARD";
		} else if (INPUT_DIFFICULTY.Equals ("VERY HARD")) {
			u_LASTDIFFICULTY = "VERY HARD";
		} else {
			u_LASTDIFFICULTY = "BEGINNER";
		}*/
		u_LASTDIFFICULTY = INPUT_DIFFICULTY;
	}

	public void Set_u_TRY(){
		//u_TRY_TIDAKPERNAH
		if (TRY > 0) {
			u_TRY_TIDAKPERNAH = 0;
		} else if (TRY <= 0) {
			u_TRY_TIDAKPERNAH = 1;
		}

		//u_TRY_JARANG
		if (TRY <= 1.5f) {
			u_TRY_JARANG = 1 - 2 * (Mathf.Pow (((1.5f - TRY) / (1.5f - 0)), 2));
		} else if (TRY > 1.5f) {
			u_TRY_JARANG = 2 * (Mathf.Pow(((3 - TRY) / (3 - 1.5f)), 2));
		}

		//u_TRY_SERING
		if (TRY <= 3) {
			u_TRY_JARANG = 1 - 2 * (Mathf.Pow (((3 - TRY) / (3 - 1)), 2));
		} else if (TRY > 3) {
			u_TRY_JARANG = 2 * (Mathf.Pow(((5 - TRY) / (5 - 3)), 2));
		}

		//u_TRY_SANGATSERING
		if (TRY <= 5) {
			u_TRY_SANGATSERING = 0;
		} else if (TRY >= 4 && TRY <= 4.5f) {
			u_TRY_SANGATSERING = 2 * (Mathf.Pow (((TRY - 4) / (5 - 4)), 2));
		} else if (TRY >= 4.5f && TRY <= 5) {
			u_TRY_SANGATSERING = 1 - 2 * (Mathf.Pow (((5 - TRY) / (5 - 4)), 2));
		} else if (TRY > 5) {
			u_TRY_SANGATSERING = 1;
		}
	}
}


