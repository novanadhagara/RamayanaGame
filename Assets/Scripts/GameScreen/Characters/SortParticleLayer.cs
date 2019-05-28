using UnityEngine;
using System.Collections;

public class SortParticleLayer : MonoBehaviour {

	public string LayerName = "Particles";

	public void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = LayerName;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
