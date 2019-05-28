using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsController : MonoBehaviour {

	//playaer instance
	public Slider STRSlider, AGISlider, INTSlider, DEFSlider, HPSliderMini, WESliderMini;
	public int STR, AGI, INT, DEF;

	public Slider HPSlider, WisdomSlider;
	public int MaxHP, MinHP, MaxWisdom, MinWisdom;

	void Start(){
		//MaxHP = MinHP = MaxWisdom = MinWisdom = 0;
		MaxHP = (int)Player._instance.MaxHealth;
		MinHP = 0;
		MaxWisdom = Player._instance.MaxWisdom;
		MinWisdom = 0;

		//set up the slider value
		HPSlider.maxValue = MaxHP;
		HPSlider.minValue = MinHP;
		WisdomSlider.maxValue = MaxWisdom;
		WisdomSlider.minValue = MinWisdom;


		//Setting up the stats
		STR = Player._instance.STR; //DAMAGE
		AGI = Player._instance.AGI; //SPEED & FIRE RATE
		DEF = Player._instance.DEF; //MAXHP
		INT = Player._instance.INT; //RANDOMIZED WISDOM POINT

		STRSlider.minValue = 0;
		AGISlider.minValue = 0;
		INTSlider.minValue = 0;
		DEFSlider.minValue = 0;
		HPSliderMini.minValue = HPSlider.minValue;
		WESliderMini.minValue = WisdomSlider.minValue;

		STRSlider.maxValue = 10;
		AGISlider.maxValue = 10;
		INTSlider.maxValue = 10;
		DEFSlider.maxValue = 20;
		HPSliderMini.maxValue = HPSlider.maxValue;
		WESliderMini.maxValue = WisdomSlider.maxValue;
	}
	//starting vale, min, max
	void Update(){
		UpdateHPSlider ();
		UpdateWisdomSlider ();
		UpdateStats ();
	}

	void UpdateHPSlider(){
		if (HPSlider.value >= MinHP && HPSlider.value <= (int)MaxHP) {
			HPSlider.value = (int)Player._instance.CurrentHealth;
			//Debug.Log("HP "+HPSlider.value);
		}
	}
	void UpdateWisdomSlider(){
		if (WisdomSlider.value >= MinWisdom && WisdomSlider.value <= MaxWisdom) {
			WisdomSlider.value = Player._instance.CurrentWisdom;
			//Debug.Log("Wisdom "+WisdomSlider.value);
		}
	}
	void UpdateStats(){
		//all stats here
		if (Player._instance.STR >= STRSlider.minValue && Player._instance.STR <= STRSlider.maxValue) {
			STRSlider.value = Player._instance.STR;
			STR = Player._instance.STR;
		}
		if (Player._instance.AGI >= AGISlider.minValue && Player._instance.AGI <= AGISlider.maxValue) {
			AGISlider.value = Player._instance.AGI;
			AGI = Player._instance.AGI;
		}
		if (Player._instance.INT >= INTSlider.minValue && Player._instance.INT <= INTSlider.maxValue) {
			INTSlider.value = Player._instance.INT;
			INT = Player._instance.INT;
		}
		if (Player._instance.DEF >= DEFSlider.minValue && Player._instance.DEF <= DEFSlider.maxValue) {
			DEFSlider.value = Player._instance.DEF;
			DEF = Player._instance.DEF;
		}
		HPSliderMini.value = HPSlider.value;
		WESliderMini.value = WisdomSlider.value;
	}

}
