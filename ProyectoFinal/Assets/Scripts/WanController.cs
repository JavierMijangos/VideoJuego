using UnityEngine;
using System.Collections;

public class WanController : MonoBehaviour {
	public int healt = 3;
		
	public float walkSpeed = 1.5f;
	public float runSpeed = 4 ;
	
	public GameObject atackTrigger;
	public GameObject head;
	public Transform spawnerHead;

	private bool headSpawned = false;

	private Animator anim;
	private Transform player;

	private bool walking = false;
	private bool running = false;
	private bool getingHit = false;
	private bool getingReady = false;
	private bool atacking = false;
	private bool death = false;
	private bool decide = true;
	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		player = GameObject.FindWithTag ("Player").transform;
		StartCoroutine (Decide(4));

	}

	void start(){

	}
	
	// Update is called once per frame
	void Update () {

		if ( transform.position.x < player.position.x - 16) {

			transform.position = new Vector3 (player.position.x - 11,transform.position.y);
		}

		if(walking){
			transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
		}

		if(running){
			transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
		}

		if (getingHit || atacking || death) {

			//Stop();
		}

		if(getingReady){
			decide = true;
			StopAllCoroutines();
			StartCoroutine(Decide(0.01f));
		}

		if (death){
			Stop();
			anim.SetBool("GetHit", true);
			anim.SetBool("GetReady", false);
			anim.SetBool("Death",true);
			StartCoroutine(SpawnHead());
		}
	}
	

	IEnumerator Decide(float waitTime){
		getingReady = false;
		yield return new WaitForSeconds (waitTime);
		while(decide){
			int value = Random.Range(0,9);
			switch(value){
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
				StartCoroutine(Walk());
				break;
			case 7:
			case 8:
			case 9:
				StartCoroutine(Run());
				break;
			}
			yield return new WaitForSeconds(3);
		}
	}

	IEnumerator Walk(){
		anim.SetBool("Walk", true);
		walking = true;
		yield return new WaitForSeconds(3);
		anim.SetBool("Walk", false);
		walking = false;
	}

	IEnumerator Run(){
		anim.SetBool("Run", true);
		running = true;
		yield return new WaitForSeconds(3);
		anim.SetBool("Run", false);
		running = false;

	}

	IEnumerator Atack(){
		while(atacking){
			if(transform.position.y < -0.6f){
				anim.SetBool("Atack", true);
				transform.Translate(Vector3.up * walkSpeed * Time.deltaTime);
			}else{
				StartCoroutine(Down());
			}

			yield return new WaitForSeconds(0);
		}
	}

	IEnumerator Down(){
		StopCoroutine (Atack ());
		while (atacking) {
			if (transform.position.y > -17.8f) {
				transform.Translate (Vector3.down * walkSpeed * Time.deltaTime);
			} else {
				anim.SetBool ("Atack", false);
				atacking=false;
			}
			yield return new WaitForSeconds(0);
		}
	}

	IEnumerator GetHit(){
		Stop ();
		anim.SetBool("GetHit", true);
		getingHit = true;
		yield return new WaitForSeconds(0.20f);
		anim.SetBool("GetHit", false);
		getingHit = false;
		StartCoroutine (GetReady());
	}

	IEnumerator GetReady(){
		anim.SetBool("GetReady", true);
		yield return new WaitForSeconds(0.13f);
		anim.SetBool("GetReady", false);
		getingReady = true;
	}

	public void HitEnemy (int damage){
		if (healt > 0) {
			healt -= damage;
			StartCoroutine(GetHit());
		} 

		if(healt == 0){
			death = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Stop();
			anim.SetBool("Atack", true);
			other.gameObject.GetComponent<PlayerControler> ().HitPlayer(this.gameObject.transform.position.x, 100);
			StartCoroutine(StopAtack());
		}
	}

	void Stop(){
		walking = false;
		running = false;
		getingHit = false;
		getingReady = false;
		atacking = false;
		anim.SetBool("Walk", false);
		anim.SetBool("Run", false);
		anim.SetBool ("Atack", false);
		//StopCoroutine(Decide(0));
		decide = false;
		StopCoroutine(Walk());
		StopCoroutine(Run());
	}

	IEnumerator StopAtack(){
		yield return new WaitForSeconds (0.5f);
		anim.SetBool("Atack", false);
	}
	IEnumerator SpawnHead(){
		if (!headSpawned) {
			Instantiate (head, spawnerHead.position, Quaternion.identity);
			headSpawned = true;
		}
		yield return new WaitForSeconds (0);
	}
}
