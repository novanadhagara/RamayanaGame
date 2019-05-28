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

	public string Max_u_HP;
	public string Max_u_TIME;
	public string Max_u_TRY;
	public string Max_u_SOLVE;
	public string Max_u_ACCURACY;
	public string Max_u_DIFFICULTY;

	public float MaxHP, MaxTIME, MaxTRY, MaxSOLVE, MaxACCURACY, MaxDIFFICULTY;
	//OUTPUT
	private string OUTPUT_DIFFICULTY{get; set;}

	//GET, SET, RESET, ADD VALUE, REDUCE VALUE, GET ALL ATTRIBUTE, SET ALL ATTRIBUTE, UPDATE VALUE, 
	//on trigger enter (checkpoint)
	//METHOD

	void Start(){
		//Debug.Log ("Start : "+Time.deltaTime);
	}

	void Update(){
		//Debug.Log ("Start time : "+Time.deltaTime);
		Get_u ();
	}

	private void SetAttribute(float HP, float TIME, float TRY, float SOLVE, float ACCURACY, string DIFFICULTY){
		this.HP = HP;
		this.TIME = TIME;
		this.TRY = TRY;
		this.SOLVE = SOLVE;
		this.ACCURACY = ACCURACY;
		this.INPUT_DIFFICULTY = DIFFICULTY;
	}

	public void ARDSystem(){

	}

	public void Get_u(){
		SetAttribute (PlayerPrefs.GetFloat("HP"), PlayerPrefs.GetFloat("TIME"), 
		              PlayerPrefs.GetFloat("CURRENT_TRY"), PlayerPrefs.GetFloat("SOLVE"), 
		              PlayerPrefs.GetFloat ("ACCURACY"), PlayerPrefs.GetString("DIFFICULTY"));
		SetAllParameters ();

        //DEBUGGING
        
     //Debug.Log ("HP sedikit:"+u_HP_SEDIKIT+" HP cukup"+u_HP_CUKUP+" HP banyak"+u_HP_BANYAK+" HP Penuh"+u_HP_PENUH); 	//HP
    // Debug.Log ("TRY tidakpernah:"+u_TRY_TIDAKPERNAH+" TRY jarang"+u_TRY_JARANG+" TRY sering"+u_TRY_SERING+" TRY sangat sering"+u_TRY_SANGATSERING);	//TRY
     //Debug.Log ("TIME sempurna:"+u_TIME_SEMPURNA+" TIME cepat"+u_TIME_CEPAT+" TIME agaklambat"+u_TIME_AGAKLAMBAT+" TIME lambat"+u_TIME_LAMBAT); 	//TIME
     //Debug.Log ("SOLVE rendah:"+u_SOLVE_RENDAH+" SOLVE cukup"+u_SOLVE_CUKUP+" SOLVE tinggi"+u_SOLVE_TINGGI+" SOLVE penuh"+u_SOLVE_PENUH); 	//SOLVE
     //Debug.Log ("ACCURACY sangatrendah:"+u_ACCURACY_SANGATRENDAH+" ACCURACY rendah"+u_ACCURACY_RENDAH+" ACCURACY cukup"+u_ACCURACY_CUKUP+" ACC tinggi"+u_ACCURACY_TINGGI); 	//accuracy
     //Debug.Log ("LastDifficulty "+u_LASTDIFFICULTY+"playerprefs dif "+PlayerPrefs.GetString("DIFFICULTY")+"input dif"+INPUT_DIFFICULTY);
     
        Calculate60 ();
     //Calculate61 ();
     //STORE PREFS TO BE NEW PREFS (ONLY THE DIFFICULTIES)
 }

 public void SetAllParameters(){
     Set_u_HP ();
     Set_u_TIME ();
     Set_u_TRY ();
     Set_u_SOLVE ();
     Set_u_ACCURACY ();
     Set_u_DIFFICULTY ();
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
     if (HP <= 30 || HP >= 70) {
         u_HP_CUKUP = 0;
     } else if (HP >= 30 && HP <= 40) {
         u_HP_CUKUP = 2 * (Mathf.Pow (((HP - 30) / (50 - 30)), 2));
     } else if (HP >= 40 && HP <= 50) {
         u_HP_CUKUP = 1 - 2 * (Mathf.Pow (((50 - HP) / (50 - 30)), 2));
     } else if (HP == 50) {
         u_HP_CUKUP = 1;
     } else if (HP >= 50 && HP <= 60) {
         u_HP_CUKUP = 1 - 2 * (Mathf.Pow (((HP - 50) / (70 - 50)), 2));
     } else if (HP >= 60 && HP <= 70) {
         u_HP_CUKUP = 2 * (Mathf.Pow (((70 - HP) / (70 - 50)), 2));
     }

     //U_HP_BANYAK
     if (HP<= 50 || HP >= 100) {
         u_HP_BANYAK = 0;
     } else if (HP >= 50 && HP <= 65) {
         u_HP_BANYAK = 2 * (Mathf.Pow (((HP - 50) / (75 - 50)), 2));
     } else if (HP >= 65 && HP <= 75) {
         u_HP_BANYAK = 1 - 2 * (Mathf.Pow (((75 - HP) / (75 - 50)), 2));
     } else if (HP == 75) {
         u_HP_BANYAK = 1;
     } else if (HP >= 75 && HP <= 85) {
         u_HP_BANYAK = 1 - 2 * (Mathf.Pow (((HP - 75) / (100 - 75)), 2));
     } else if (HP >= 85 && HP <= 100) {
         u_HP_BANYAK = 2 * (Mathf.Pow (((100 - HP) / (100 - 75)), 2));
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
     if (TIME<= 5 || TIME >= 25) {
         u_TIME_AGAKLAMBAT = 0;
     } else if (TIME >= 5 && TIME <= 10) {
         u_TIME_AGAKLAMBAT = 2 * (Mathf.Pow (((TIME - 5) / (15 - 5)), 2));
     } else if (TIME >= 10 && TIME <= 15) {
         u_TIME_AGAKLAMBAT = 1 - 2 * (Mathf.Pow (((15 - TIME) / (15 - 5)), 2));
     } else if (TIME == 15) {
         u_TIME_AGAKLAMBAT = 1;
     } else if (TIME >= 15 && TIME <= 20) {
         u_TIME_AGAKLAMBAT = 1 - 2 * (Mathf.Pow (((TIME - 15) / (25 - 15)), 2));
     } else if (TIME >= 20 && TIME <= 25) {
         u_TIME_AGAKLAMBAT = 2 * (Mathf.Pow (((25 - TIME) / (25 - 15)), 2));
     }

     //U_TIME_CEPAT
     if (TIME<= 15 || TIME >= 32) {
         u_TIME_CEPAT = 0;
     } else if (TIME >= 15 && TIME <= 20) {
         u_TIME_CEPAT = 2 * (Mathf.Pow (((TIME - 15) / (22 - 15)), 2));
     } else if (TIME >= 20 && TIME <= 22) {
         u_TIME_CEPAT = 1 - 2 * (Mathf.Pow (((22 - TIME) / (22 - 15)), 2));
     } else if (TIME == 22) {
         u_TIME_CEPAT = 1;
     } else if (TIME >= 22 && TIME <= 27) {
         u_TIME_CEPAT = 1 - 2 * (Mathf.Pow (((TIME - 22) / (32 - 22)), 2));
     } else if (TIME >= 27 && TIME <= 32) {
         u_TIME_CEPAT = 2 * (Mathf.Pow (((32 - TIME) / (32 - 25)), 2));
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
     if (SOLVE<= 30 || SOLVE >= 90) {
         u_SOLVE_CUKUP = 0;
     } else if (SOLVE >= 30 && SOLVE <= 50) {
         u_SOLVE_CUKUP = 2 * (Mathf.Pow (((SOLVE - 30) / (60 - 30)), 2));
     } else if (SOLVE >= 50 && SOLVE <= 60) {
         u_SOLVE_CUKUP = 1 - 2 * (Mathf.Pow (((60 - SOLVE) / (60 - 30)), 2));
     } else if (SOLVE == 60) {
         u_SOLVE_CUKUP = 1;
     } else if (SOLVE >= 60 && SOLVE <= 70) {
         u_SOLVE_CUKUP = 1 - 2 * (Mathf.Pow (((SOLVE - 60) / (90 - 60)), 2));
     } else if (SOLVE >= 70 && SOLVE <= 90) {
         u_SOLVE_CUKUP = 2 * (Mathf.Pow (((90 - SOLVE) / (90 - 60)), 2));
     }

     //u_SOLVE_TINGGI
     if (SOLVE<= 70 || SOLVE >= 100) {
         u_SOLVE_TINGGI = 0;
     } else if (SOLVE >= 70 && SOLVE <= 75) {
         u_SOLVE_TINGGI = 2 * (Mathf.Pow (((SOLVE - 70) / (85 - 70)), 2));
     } else if (SOLVE >= 75 && SOLVE <= 85) {
         u_SOLVE_TINGGI = 1 - 2 * (Mathf.Pow (((85 - SOLVE) / (85 - 70)), 2));
     } else if (SOLVE == 85) {
         u_SOLVE_TINGGI = 1;
     } else if (SOLVE >= 85 && SOLVE <= 90) {
         u_SOLVE_TINGGI = 1 - 2 * (Mathf.Pow (((SOLVE - 85) / (100 - 85)), 2));
     } else if (SOLVE >= 90 && SOLVE <= 100) {
         u_SOLVE_TINGGI = 2 * (Mathf.Pow (((100 - SOLVE) / (100 - 85)), 2));
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
     if (ACCURACY<= 0 || ACCURACY >= 40) {
         u_ACCURACY_RENDAH = 0;
     } else if (ACCURACY >= 0 && ACCURACY <= 15) {
         u_ACCURACY_RENDAH = 2 * (Mathf.Pow (((ACCURACY - 0) / (20 - 0)), 2));
     } else if (ACCURACY >= 15 && ACCURACY <= 20) {
         u_ACCURACY_RENDAH = 1 - 2 * (Mathf.Pow (((20 - ACCURACY) / (20 - 0)), 2));
     } else if (ACCURACY == 20) {
         u_ACCURACY_RENDAH = 1;
     } else if (ACCURACY >= 20 && ACCURACY <= 30) {
         u_ACCURACY_RENDAH = 1 - 2 * (Mathf.Pow (((ACCURACY - 20) / (40 - 20)), 2));
     } else if (ACCURACY >= 30 && ACCURACY <= 40) {
         u_ACCURACY_RENDAH = 2 * (Mathf.Pow (((40 - ACCURACY) / (40 - 20)), 2));
     }

     //u_ACCURACY_CUKUP
     if (ACCURACY<= 20 || ACCURACY >= 70) {
         u_ACCURACY_CUKUP = 0;
     } else if (ACCURACY >= 20 && ACCURACY <= 35) {
         u_ACCURACY_CUKUP = 2 * (Mathf.Pow (((ACCURACY - 20) / (45 - 20)), 2));
     } else if (ACCURACY >= 35 && ACCURACY <= 45) {
         u_ACCURACY_CUKUP = 1 - 2 * (Mathf.Pow (((45 - ACCURACY) / (45 - 20)), 2));
     } else if (ACCURACY == 45) {
         u_ACCURACY_CUKUP = 1;
     } else if (ACCURACY >= 45 && ACCURACY <= 55) {
         u_ACCURACY_CUKUP = 1 - 2 * (Mathf.Pow (((ACCURACY - 45) / (70 - 45)), 2));
     } else if (ACCURACY >= 55 && ACCURACY <= 70) {
         u_ACCURACY_CUKUP = 2 * (Mathf.Pow (((70 - ACCURACY) / (70 - 45)), 2));
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
     if (TRY<= 0 || TRY >= 3) {
         u_TRY_JARANG = 0;
     } else if (TRY >= 0 && TRY <= 0.5f) {
         u_TRY_JARANG = 2 * (Mathf.Pow (((TRY - 0) / (1.5f - 0)), 2));
     } else if (TRY >= 0.5f && TRY <= 1.5f) {
         u_TRY_JARANG = 1 - 2 * (Mathf.Pow (((1.5f - TRY) / (1.5f - 0)), 2));
     } else if (TRY == 1.5f) {
         u_TRY_JARANG = 1;
     } else if (TRY >= 1.5f && TRY <= 2) {
         u_TRY_JARANG = 1 - 2 * (Mathf.Pow (((TRY - 1.5f) / (3 - 1.5f)), 2));
     } else if (TRY >= 2 && TRY <= 3) {
         u_TRY_JARANG = 2 * (Mathf.Pow (((3 - TRY) / (3 - 1.5f)), 2));
     }

     //u_TRY_SERING
     if (TRY<= 1 || TRY >= 5) {
         u_TRY_SERING = 0;
     } else if (TRY >= 1 && TRY <= 2.5f) {
         u_TRY_SERING = 2 * (Mathf.Pow (((TRY - 1) / (3 - 1)), 2));
     } else if (TRY >= 2.5f && TRY <= 3) {
         u_TRY_SERING = 1 - 2 * (Mathf.Pow (((3 - TRY) / (3 - 1)), 2));
     } else if (TRY == 3) {
         u_TRY_SERING = 1;
     } else if (TRY >= 3 && TRY <= 4) {
         u_TRY_SERING = 1 - 2 * (Mathf.Pow (((TRY - 3) / (5 - 3)), 2));
     } else if (TRY >= 4 && TRY <= 5) {
         u_TRY_SERING = 2 * (Mathf.Pow (((5 - TRY) / (5 - 3)), 2));
     }

     //u_TRY_SANGATSERING
     if (TRY < 5) {	//this bugged
         u_TRY_SANGATSERING = 0;
     } else if (TRY >= 4 && TRY <= 4.5f) {
         u_TRY_SANGATSERING = 2 * (Mathf.Pow (((TRY - 4) / (5 - 4)), 2));
     } else if (TRY >= 4.5f && TRY <= 5) {
         u_TRY_SANGATSERING = 1 - 2 * (Mathf.Pow (((5 - TRY) / (5 - 4)), 2));
     } else if (TRY >= 5) { //this bugged
         u_TRY_SANGATSERING = 1;
     }
 }

 public void Calculate60(){
     //cari nilai linguistik dari semua parameter input
     //pilih nilai max untuk tiap parameter (LOW, HIGH, dsb) --> easy, gunakan tabel untuk mengecek secara crisp

     //MAX u HP
     if (u_HP_SEDIKIT >= u_HP_CUKUP && u_HP_SEDIKIT >= u_HP_BANYAK && u_HP_SEDIKIT >= u_HP_PENUH) {
         Max_u_HP = "HP SEDIKIT";
     } else if (u_HP_CUKUP >= u_HP_SEDIKIT && u_HP_CUKUP >= u_HP_BANYAK && u_HP_CUKUP >= u_HP_PENUH) {
         Max_u_HP = "HP CUKUP";
     } else if (u_HP_BANYAK >= u_HP_SEDIKIT && u_HP_BANYAK >= u_HP_CUKUP && u_HP_BANYAK >= u_HP_PENUH) {
         Max_u_HP = "HP BANYAK";
     } else if (u_HP_PENUH >= u_HP_SEDIKIT && u_HP_PENUH >= u_HP_CUKUP && u_HP_PENUH >= u_HP_BANYAK) {
         Max_u_HP = "HP PENUH";
     }

     //Max_u_TIME;
     if (u_TIME_LAMBAT >= u_TIME_AGAKLAMBAT && u_TIME_LAMBAT >= u_TIME_CEPAT && u_TIME_LAMBAT >= u_TIME_SEMPURNA) {
         Max_u_TIME = "TIME LAMBAT";
     } else if (u_TIME_AGAKLAMBAT >= u_TIME_LAMBAT && u_TIME_AGAKLAMBAT >= u_TIME_CEPAT && u_TIME_AGAKLAMBAT >= u_TIME_SEMPURNA) {
         Max_u_TIME = "TIME AGAKLAMBAT";
     } else if (u_TIME_CEPAT >= u_TIME_LAMBAT && u_TIME_CEPAT >= u_TIME_AGAKLAMBAT && u_TIME_CEPAT >= u_TIME_SEMPURNA) {
         Max_u_TIME = "TIME CEPAT";
     } else if (u_TIME_SEMPURNA >= u_TIME_LAMBAT && u_TIME_SEMPURNA >= u_TIME_AGAKLAMBAT && u_TIME_SEMPURNA >= u_TIME_CEPAT) {
         Max_u_TIME = "TIME SEMPURNA";
     }

     //Max_u_TRY;
     if (u_TRY_SANGATSERING >= u_TRY_SERING && u_TRY_SANGATSERING >= u_TRY_JARANG && u_TRY_SANGATSERING >= u_TRY_TIDAKPERNAH) {
         Max_u_TRY = "TRY SANGATSERING";
     } else if (u_TRY_SERING >= u_TRY_SANGATSERING && u_TRY_SERING >= u_TRY_JARANG && u_TRY_SERING >= u_TRY_TIDAKPERNAH) {
         Max_u_TRY = "TRY SERING";
     } else if (u_TRY_JARANG >= u_TRY_SANGATSERING && u_TRY_JARANG >= u_TRY_SERING && u_TRY_JARANG >= u_TRY_TIDAKPERNAH) {
         Max_u_TRY = "TRY JARANG";
     } else if (u_TRY_TIDAKPERNAH >= u_TRY_SANGATSERING && u_TRY_TIDAKPERNAH >= u_TRY_SERING && u_TRY_TIDAKPERNAH >= u_TRY_JARANG) {
         Max_u_TRY = "TRY TIDAKPERNAH";
     }

      //Max_u_SOLVE;
     if (u_SOLVE_RENDAH >= u_SOLVE_CUKUP && u_SOLVE_RENDAH >= u_SOLVE_TINGGI && u_SOLVE_RENDAH >= u_SOLVE_PENUH) {
         Max_u_SOLVE = "SOLVE RENDAH";
     } else if (u_SOLVE_CUKUP >= u_SOLVE_RENDAH && u_SOLVE_CUKUP >= u_SOLVE_TINGGI && u_SOLVE_CUKUP >= u_SOLVE_PENUH) {
         Max_u_SOLVE = "SOLVE CUKUP";
     } else if (u_SOLVE_TINGGI >= u_SOLVE_RENDAH && u_SOLVE_TINGGI >= u_SOLVE_CUKUP && u_SOLVE_TINGGI >= u_SOLVE_PENUH) {
         Max_u_SOLVE = "SOLVE TINGGI";
     } else if (u_SOLVE_PENUH >= u_SOLVE_RENDAH && u_SOLVE_PENUH >= u_SOLVE_CUKUP && u_SOLVE_PENUH >= u_SOLVE_TINGGI) {
         Max_u_SOLVE = "SOLVE PENUH";
     }

     //Max_u_ACCURACY;
     if (u_ACCURACY_SANGATRENDAH >= u_ACCURACY_RENDAH && u_ACCURACY_SANGATRENDAH >= u_ACCURACY_CUKUP && u_ACCURACY_SANGATRENDAH >= u_ACCURACY_TINGGI) {
         Max_u_ACCURACY = "ACCURACY SANGATRENDAH";
     } else if (u_ACCURACY_RENDAH >= u_ACCURACY_SANGATRENDAH && u_ACCURACY_RENDAH >= u_ACCURACY_CUKUP && u_ACCURACY_RENDAH >= u_ACCURACY_TINGGI) {
         Max_u_ACCURACY = "ACCURACY RENDAH";
     } else if (u_ACCURACY_CUKUP >= u_ACCURACY_SANGATRENDAH && u_ACCURACY_CUKUP >= u_ACCURACY_RENDAH && u_ACCURACY_CUKUP >= u_ACCURACY_TINGGI) {
         Max_u_ACCURACY = "ACCURACY CUKUP";
     } else if (u_ACCURACY_TINGGI >= u_ACCURACY_SANGATRENDAH && u_ACCURACY_TINGGI >= u_ACCURACY_RENDAH && u_ACCURACY_TINGGI >= u_ACCURACY_CUKUP) {
         Max_u_ACCURACY = "ACCURACY TINGGI";
     }

     //Max_u_DIFFICULTY;
     Max_u_DIFFICULTY = u_LASTDIFFICULTY;

     //Max of each parameter defined, now use 60 tabel!
     //BEGINNER
     if (Max_u_TRY == "TRY SANGATSERINGT" && Max_u_DIFFICULTY == "BEGINNER"  //rule 1
         && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" && Max_u_SOLVE == "SOLVE RENDAH") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_DIFFICULTY == "BEGINNER" && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" //rule 2
               && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_TIME == "TIME LAMBAT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_ACCURACY == "ACCURACY SANGATRENDAH" && Max_u_SOLVE == "SOLVE RENDAH" //rule 3
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_SOLVE == "SOLVE RENDAH" //rule 4
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_DIFFICULTY == "BEGINNER"  //rule 5
               && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_DIFFICULTY == "BEGINNER" && Max_u_SOLVE == "SOLVE RENDAH" //rule 6
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_DIFFICULTY == "BEGINNER" //rule 7
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_DIFFICULTY == "BEGINNER"  //rule 8
               && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" && Max_u_TIME == "TIME LAMBAT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_DIFFICULTY == "BEGINNER" && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" //RULE 9
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_DIFFICULTY == "BEGINNER" && Max_u_ACCURACY == "ACCURACY SANGATRENDAH"  //rule 10
               && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_HP == "HP SEDIKIT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_ACCURACY == "ACCURACY SANGATRENDAH"  //11
               && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_TIME == "TIME LAMBAT") {
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING"  && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" 
               && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_HP == "HP SEDIKIT") { //12
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_DIFFICULTY == "BEGINNER" 
                && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_TIME == "TIME LAMBAT") { //13
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_DIFFICULTY == "BEGINNER" 
              && Max_u_SOLVE == "SOLVE RENDAH" && Max_u_HP == "HP SEDIKIT") { //14
         OUTPUT_DIFFICULTY = "BEGINNER";
     }else if (Max_u_TRY == "TRY SANGATSERING" && Max_u_ACCURACY == "ACCURACY SANGATRENDAH" 
               && Max_u_TIME == "TIME LAMBAT" && Max_u_HP == "HP SEDIKIT") { //15
         OUTPUT_DIFFICULTY = "BEGINNER";
     }
     //MEDIUM
     if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM" 
         && Max_u_ACCURACY == "ACCURACY RENDAH" && Max_u_SOLVE == "SOLVE CUKUP") { //1
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_DIFFICULTY == "MEDIUM" && Max_u_ACCURACY == "ACCURACY RENDAH" 
               && Max_u_SOLVE == "SOLVE CUKUP" && Max_u_TIME == "TIME AGAKLAMBAT") { //2
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_ACCURACY == "ACCURACY RENDAH" && Max_u_SOLVE == "SOLVE CUKUP" //3
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_SOLVE == "SOLVE CUKUP" //4
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM" 
               && Max_u_ACCURACY == "ACCURACY RENDAH" && Max_u_HP == "HP CUKUP") { //5
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_DIFFICULTY == "MEDIUM" && Max_u_SOLVE == "SOLVE CUKUP" //6
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM"  //7
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM"  //8
               && Max_u_ACCURACY == "ACCURACY RENDAH" && Max_u_TIME == "TIME AGAKLAMBAT") { 
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_DIFFICULTY == "MEDIUM" && Max_u_ACCURACY == "ACCURACY RENDAH" //9
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_DIFFICULTY == "MEDIUM" && Max_u_ACCURACY == "ACCURACY RENDAH" 
               && Max_u_SOLVE == "SOLVE CUKUP" && Max_u_HP == "HP CUKUP") { //10
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_ACCURACY == "ACCURACY RENDAH"  //11
               && Max_u_SOLVE == "SOLVE CUKUP" && Max_u_TIME == "TIME AGAKLAMBAT") {
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_ACCURACY == "ACCURACY RENDAH" 
               && Max_u_SOLVE == "SOLVE CUKUP" && Max_u_HP == "HP CUKUP") { //12
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM" 
              && Max_u_SOLVE == "SOLVE CUKUP"  && Max_u_TIME == "TIME AGAKLAMBAT") { //13
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING" && Max_u_DIFFICULTY == "MEDIUM" 	
              && Max_u_SOLVE == "SOLVE CUKUP" && Max_u_HP == "HP CUKUP") { //14
         OUTPUT_DIFFICULTY = "MEDIUM";
     }else if (Max_u_TRY == "TRY SERING"  && Max_u_ACCURACY == "ACCURACY RENDAH" 
               && Max_u_TIME == "TIME AGAKLAMBAT" && Max_u_HP == "HP CUKUP") { //15
         OUTPUT_DIFFICULTY = "MEDIUM";
     }

     //HARD
     if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" 
         && Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_SOLVE == "SOLVE TINGGI") { //1
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_DIFFICULTY == "HARD" && Max_u_TIME == "TIME CEPAT"
               && Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_SOLVE == "SOLVE TINGGI") { //2
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_SOLVE == "SOLVE TINGGI" //3 
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") {
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_SOLVE == "SOLVE TINGGI" //4
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") {
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" 
               && Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_HP == "HP BANYAK") { //5
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_DIFFICULTY == "HARD" && Max_u_SOLVE == "SOLVE TINGGI" //6
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") {
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" //7
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") {
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" 
               && Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_TIME == "TIME CEPAT") { //8
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_DIFFICULTY == "HARD" && Max_u_ACCURACY == "ACCURACY CUKUP" //9
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") {
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_DIFFICULTY == "HARD"&& Max_u_ACCURACY == "ACCURACY CUKUP" 
               && Max_u_SOLVE == "SOLVE TINGGI" && Max_u_HP == "HP BANYAK") { //10
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_TIME == "TIME CEPAT"
               && Max_u_ACCURACY == "ACCURACY CUKUP" && Max_u_SOLVE == "SOLVE TINGGI") { //11
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_ACCURACY == "ACCURACY CUKUP" 
          && Max_u_SOLVE == "SOLVE TINGGI"  && Max_u_HP == "HP BANYAK") { //12
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" 
               && Max_u_SOLVE == "SOLVE TINGGI" && Max_u_TIME == "TIME CEPAT" ) { //13
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_DIFFICULTY == "HARD" 
               && Max_u_SOLVE == "SOLVE TINGGI" && Max_u_HP == "HP BANYAK") { //14
         OUTPUT_DIFFICULTY = "HARD";
     }else if (Max_u_TRY == "TRY JARANG" && Max_u_ACCURACY == "ACCURACY CUKUP"
               && Max_u_TIME == "TIME CEPAT" && Max_u_HP == "HP BANYAK") { //15
         OUTPUT_DIFFICULTY = "HARD";
     }
     //VERY HARD
     if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" 
         && Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_SOLVE == "SOLVE SEMPURNA") { //1
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_DIFFICULTY == "VERY HARD" && Max_u_TIME == "TIME SEMPURNAT"
         && Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_SOLVE == "SOLVE SEMPURNA") { //2
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_SOLVE == "SOLVE SEMPURNA" //3 
         && Max_u_TIME == "TIME SEMPURNAT" && Max_u_HP == "HP PENUH") {
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_SOLVE == "SOLVE SEMPURNA" //4
         && Max_u_TIME == "TIME SEMPURNAT" && Max_u_HP == "HP PENUH") {
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" 
         && Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_HP == "HP PENUH") { //5
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_DIFFICULTY == "VERY HARD" && Max_u_SOLVE == "SOLVE SEMPURNA" //6
         && Max_u_TIME == "TIME SEMPURNAT" && Max_u_HP == "HP PENUH") {
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" //7
         && Max_u_TIME == "TIME SEMPURNAT" && Max_u_HP == "HP PENUH") {
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" 
         && Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_TIME == "TIME SEMPURNAT") { //8
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_DIFFICULTY == "VERY HARD" && Max_u_ACCURACY == "ACCURACY TINGGI" //9
         && Max_u_TIME == "TIME SEMPURNAT" && Max_u_HP == "HP PENUH") {
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_DIFFICULTY == "VERY HARD" && Max_u_ACCURACY == "ACCURACY TINGGI" 
         && Max_u_SOLVE == "SOLVE SEMPURNA" && Max_u_HP == "HP PENUH") { //10
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_TIME == "TIME SEMPURNAT"
         && Max_u_ACCURACY == "ACCURACY TINGGI" && Max_u_SOLVE == "SOLVE SEMPURNA") { //11
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_ACCURACY == "ACCURACY TINGGI" 
         && Max_u_SOLVE == "SOLVE SEMPURNA" && Max_u_HP == "HP PENUH") { //12
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" 
         && Max_u_SOLVE == "SOLVE SEMPURNA" && Max_u_TIME == "TIME SEMPURNAT") { //13
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_DIFFICULTY == "VERY HARD" 
         && Max_u_SOLVE == "SOLVE SEMPURNA" && Max_u_HP == "HP PENUH") { //14
         OUTPUT_DIFFICULTY = "VERY HARD";
     } else if (Max_u_TRY == "TRY TIDAKPERNAH" && Max_u_ACCURACY == "ACCURACY TINGGI"
         && Max_u_TIME == "TIME SEMPURNA" && Max_u_HP == "HP PENUH") { //15
         OUTPUT_DIFFICULTY = "VERY HARD";
     }//ELSE OF ALL
     else {
         //Debug.Log ("output dif 60: "+OUTPUT_DIFFICULTY);
         //OUTPUT_DIFFICULTY = "UNDEFINED";
         //Debug.Log("UNDEFINED");
         Calculate61();
     }

     PlayerPrefs.SetString ("DIFFICULTY", OUTPUT_DIFFICULTY);
     //Debug.Log ("End time : "+Time.deltaTime);
 }
 public void Calculate61(){
     //cari nilai linguistik dari semua parameter input
     //hitung dan cari derajat keannggotaan yang max, ditentukan dari sigma maka akan didapat u_max dari setiap parameter
     float Total_u_BEGINNER, Total_u_MEDIUM, Total_u_HARD, Total_u_VERYHARD;
     float DifBeginner = 0, DifMedium = 0, DifHard = 0, DifVeryHard = 0;

     SetAllParameters ();

     if (Max_u_DIFFICULTY == "BEGINNER") {
         DifBeginner = 1;
     } else if (Max_u_DIFFICULTY == "MEDIUM") {
         DifMedium = 1;
     } else if (Max_u_DIFFICULTY == "HARD") {
         DifHard = 1;
     } else if (Max_u_DIFFICULTY == "VERY HARD") {
         DifVeryHard = 1;
     }

     Total_u_BEGINNER = (u_HP_SEDIKIT + u_TIME_LAMBAT + u_TRY_SANGATSERING + u_SOLVE_RENDAH + u_ACCURACY_SANGATRENDAH + DifBeginner) / 6;
     Total_u_MEDIUM = (u_HP_CUKUP + u_TIME_AGAKLAMBAT + u_TRY_SERING + u_SOLVE_CUKUP + u_ACCURACY_RENDAH + DifMedium) / 6;
     Total_u_HARD = (u_HP_BANYAK + u_TIME_CEPAT + u_TRY_JARANG + u_SOLVE_TINGGI + u_ACCURACY_CUKUP + DifHard) / 6;
     Total_u_VERYHARD = (u_HP_PENUH + u_TIME_SEMPURNA + u_TRY_TIDAKPERNAH + u_SOLVE_PENUH + u_ACCURACY_TINGGI + DifVeryHard) / 6;

    // Debug.Log ("hp sedikit"+u_HP_SEDIKIT +" time lambat" +u_TIME_LAMBAT +" try sangat sering"+ u_TRY_SANGATSERING +" solve rendah" + u_SOLVE_RENDAH + "acc sangat rendah"+u_ACCURACY_SANGATRENDAH + " difbeginner"+DifBeginner);
    // Debug.Log ("hp cukup"+u_HP_CUKUP +" time agak lambat"+ u_TIME_AGAKLAMBAT +" try sering"+ u_TRY_SERING + " Solve cukup"+u_SOLVE_CUKUP +" acc rendah"+ u_ACCURACY_RENDAH +" dif med"+ DifMedium);
  //  Debug.Log ("hp banyak"+u_HP_BANYAK +" time cepat"+ u_TIME_CEPAT +" try jarang"+ u_TRY_JARANG +" solve tinggi"+ u_SOLVE_TINGGI +" acc cukup"+ u_ACCURACY_CUKUP + " difhard"+DifHard);
   //  Debug.Log ("hp penuh"+u_HP_PENUH + "time sempurna"+u_TIME_SEMPURNA +"try never"+ u_TRY_TIDAKPERNAH + "solve sempurna"+u_SOLVE_PENUH + "acc tinggi"+u_ACCURACY_TINGGI + "dif very hard"+DifVeryHard);

   //Debug.Log ("Total beginner : "+Total_u_BEGINNER+" Total medium :"+Total_u_MEDIUM+ 
    //   " totalhard :"+Total_u_HARD+"total very hard"+Total_u_VERYHARD);
         
        //MAX
        if (Total_u_BEGINNER >= Total_u_MEDIUM && Total_u_BEGINNER >= Total_u_HARD && Total_u_BEGINNER >= Total_u_VERYHARD) {
			OUTPUT_DIFFICULTY = "BEGINNER";
		} else if (Total_u_MEDIUM >= Total_u_BEGINNER && Total_u_MEDIUM >= Total_u_HARD && Total_u_MEDIUM >= Total_u_VERYHARD) {
			OUTPUT_DIFFICULTY = "MEDIUM";
		} else if (Total_u_HARD >= Total_u_BEGINNER && Total_u_HARD >= Total_u_MEDIUM && Total_u_HARD >= Total_u_VERYHARD) {
			OUTPUT_DIFFICULTY = "HARD";
		} else if (Total_u_VERYHARD >= Total_u_BEGINNER && Total_u_VERYHARD >= Total_u_MEDIUM && Total_u_VERYHARD >= Total_u_HARD) {
			OUTPUT_DIFFICULTY = "VERY HARD";
		}

		///Debug.Log ("output dif 61: "+OUTPUT_DIFFICULTY);
		PlayerPrefs.SetString ("DIFFICULTY", OUTPUT_DIFFICULTY);
	}
	
	public void Calculate61Perceptron(){
		//pilih nilai max untuk tiap parameter, gunakan perceptron
	}
	//public void ReCalculateParameters(){}
	//tidak perlu dibuat coding, hanya perlu pembuktian saja
}
