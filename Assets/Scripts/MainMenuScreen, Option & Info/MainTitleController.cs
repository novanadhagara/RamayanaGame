using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainTitleController : MonoBehaviour {
	
	private float yAcc=0;
	private bool Up=true,Down=false,accPlus=true;

	void Start () {

	}

	void Update () {
		if (Up) {
			if(accPlus){
				yAcc +=0.02f*Time.deltaTime*10;
			}else	{
				yAcc -= 0.02f*Time.deltaTime*10;
			}
			
			if(yAcc>=0.25f){
				yAcc=0.25f;
				accPlus=false;
			}else if(yAcc<=0){
				yAcc=0;
				Up=false;
				Down=true;
				accPlus=true;
			}
			transform.position += new Vector3 (0,yAcc,0);
			
		} else if(Down){ //if(down)
			if(accPlus){
				yAcc +=0.02f*Time.deltaTime*10;
			}else{
				yAcc -= 0.02f*Time.deltaTime*10;
			}	
			
			if(yAcc>=0.25f){
				yAcc=0.25f;
				accPlus=false;
			}else if(yAcc<=0){
				yAcc=0;
				Up=true;
				Down=false;
				accPlus=true;
			}
			transform.position -= new Vector3 (0,yAcc,0);
		}
	}
}
