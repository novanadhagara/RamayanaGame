using UnityEngine;
using System.Collections;

public class GameManager2D {

	private static GameManager2D _instance;
	public static GameManager2D Instance{get {return _instance ?? (_instance = new GameManager2D()); }}

	public int Points { get; private set;}

	private GameManager2D(){}
	
	public void Reset(){
		Points = 0;
	}
	
	public void AddPoints(int points){
		Points += points;
	}
	
	public void ResetPoints(int points){
		Points = points;
	}
}
