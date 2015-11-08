using UnityEngine;
using System.Collections;

public class KurokoArea : MonoBehaviour {

	private KurokoController controller;

	private bool running = false;

	private Transform player;

	private float waitingTime= 1f;
	private float validTime= 0;
	// Use this for initialization
	void Start () {
		controller = GetComponent<KurokoController> ();
		player = GameObject.FindWithTag ("Player").transform;
		//StartCoroutine (Run ());
	}
	
	// Update is called once per frame
	void Update () {

		if(controller.death){
			controller.validMove = false;
		}

		if(validTime > waitingTime && !running && !controller.death){
			validTime = 0;
			controller.validMove = true;
		}

		if(!controller.validMove){
			validTime += Time.deltaTime;
		}

		if (player.position.x < transform.position.x && controller.facingRight) {
			controller.Flip ();
		} 

		if (player.position.x > transform.position.x && !controller.facingRight) {
			controller.Flip ();
		}
		//
		if (transform.position.x < player.position.x + 11 && transform.position.x > player.position.x + 1.79 && controller.validMove && !controller.facingRight ||
		    transform.position.x > player.position.x - 11 && transform.position.x < player.position.x - 1.79 && controller.validMove && controller.facingRight) {
				decideToMove();
			//controller.Move ();
		} /*else {
			controller.Stop();
			//controller.validMove = false;
		}*/

		if(running){
			controller.Move ();
		}

		if (transform.position.x < player.position.x + 1.79  && !controller.facingRight || transform.position.x > player.position.x - 1.79 && controller.facingRight) {
			controller.Stop();
			//StopCoroutine(Run());
			running = false;
		}

		if(transform.position.x <= player.position.x + 1.79 && controller.validMove && !controller.facingRight || transform.position.x >= player.position.x - 1.79  && controller.validMove && controller.facingRight){
			StartCoroutine(Atack());
			controller.validMove = false;
		}

		/*if (transform.position.x > player.position.x - 11 && transform.position.x < player.position.x - 1.79 && controller.validMove && controller.facingRight) {
			decideToMove();
		} else {
			controller.Stop();
			controller.validMove = false;
		}*/
		if (transform.position.x > player.position.x + 15 || transform.position.x < player.position.x - 15) {
			Destroy(this.gameObject);
		}


	}

	void decideToMove(){
		int value = Random.Range (0, 9);
		if (value > 2) {
			controller.validMove = false;
			running = true;

		} else {
			controller.validMove = false;
			running = false;
		}
	}

	IEnumerator Atack(){
		yield return new WaitForSeconds (0.1f);
		controller.Atack ();
	}



	IEnumerator Run(){

		while (running) {
			controller.Move ();
			yield return new WaitForSeconds(0.0f);
		}
	}
}
