using UnityEngine;
using System.Collections;

public class SpecialHit : MonoBehaviour {

	private KurokoController kuController;
	private HanzoController HanController;
	private WanController WanController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("BodyKuroko")){
			kuController= other.gameObject.GetComponentInParent<KurokoController> ();
			kuController.HitEnemy(2);
		}
		if(other.CompareTag("BodyHanzo")){
			HanController = other.gameObject.GetComponentInParent<HanzoController> ();
			HanController.HitEnemy(3);
		}
		if(other.CompareTag("BodyWanfu")){
			WanController = other.gameObject.GetComponent<WanController> ();
			WanController.HitEnemy(1);
			Destroy(this.gameObject);
		}
	}
}
