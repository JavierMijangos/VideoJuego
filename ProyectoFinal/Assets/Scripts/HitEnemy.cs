using UnityEngine;
using System.Collections;

public class HitEnemy : MonoBehaviour {

	private KurokoController kuController;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("BodyKuroko")){
			kuController= other.gameObject.GetComponentInParent<KurokoController> ();
			kuController.HitEnemy(1);
		}
		if(other.CompareTag("BodyHanzo")){
			other.gameObject.GetComponent<EnemyController> ();
		}
		if(other.CompareTag("BodyWanfu")){

		}
	}
}
