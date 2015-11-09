using UnityEngine;
using System.Collections;

public class HitPlayer : MonoBehaviour {

	private PlayerControler playerControler;
	
	// Use this for initialization
	void Start () {
		playerControler = GameObject.FindWithTag ("Player").GetComponent<PlayerControler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("BodyPlayer")){
			playerControler.HitPlayer(this.gameObject.transform.position.x, 1);
		}
	}
}
