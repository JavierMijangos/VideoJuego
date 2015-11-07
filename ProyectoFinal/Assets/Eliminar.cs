using UnityEngine;
using System.Collections;

public class Eliminar : MonoBehaviour {

	public PlayerControler playerControler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if(other.CompareTag("PlayerAtack")){
			Debug.Log("Me dieron nigga!");
		}
		if(other.CompareTag("BodyPlayer")){
			Debug.Log("Hitting the player");
			playerControler.HitPlayer(this.gameObject.transform.position.x);
		}
	}
}
