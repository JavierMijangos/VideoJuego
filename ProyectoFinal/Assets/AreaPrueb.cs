using UnityEngine;
using System.Collections;

public class AreaPrueb : MonoBehaviour {
	public Transform parent;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){

	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			//Debug.Log("Jugador entro en area");
			parent.Translate (Vector3.left * Time.deltaTime);
		}
	}
}
