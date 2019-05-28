using UnityEngine;
using System.Collections;

public class PathedArrowSpawner : MonoBehaviour {

	public Transform Destination;
	public PathedArrow Arrow;
	
	public float Speed;
	public float FireRate;
	public GameObject SpawnEffect;
	
	public float _nextShotInSecond;

	public void Start(){
		_nextShotInSecond = FireRate;
	}
	
	public void Update(){
		if ((_nextShotInSecond -= Time.deltaTime) > 0) {
			return;		
		}
		
		_nextShotInSecond = FireRate;
		var arrow = (PathedArrow)Instantiate (Arrow, transform.position, transform.rotation);
		arrow.Initialize (Destination, Speed);
		
		if (SpawnEffect != null) {
			Instantiate(SpawnEffect, transform.position, transform.rotation);		
		}
	}
	
	public void OnDrawGizmos(){
		if (Destination == null) {
			return;		
		}
		
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, Destination.position);
	}
}
