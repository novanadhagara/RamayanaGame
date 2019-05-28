using UnityEngine;
using System.Collections;

public class PathedArrow : MonoBehaviour {

	private Transform _destination;
	private float _speed;
	
	public GameObject DestroyEffect;
	public GameObject player;
	
	public void Initialize(Transform destination, float speed){
		_destination = destination;
		_speed = speed;
	}
	
	public void Update(){
		if(_destination==null){
			Destroy(this.gameObject);
		}else {
			transform.position = Vector3.MoveTowards (transform.position, _destination.position, Time.deltaTime * _speed);
			
			var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;
			if (distanceSquared > .01f * 0.1f) {
				return;		
			}
			
			if (DestroyEffect != null) {
				Instantiate(DestroyEffect, transform.position, transform.rotation);		
			}
			Destroy (gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Player" ){//|| collider.gameObject.layer == 8){
			if (DestroyEffect != null) {
				Instantiate(DestroyEffect, transform.position, transform.rotation);		
			}	
		}
		Destroy (gameObject);
	}

}
