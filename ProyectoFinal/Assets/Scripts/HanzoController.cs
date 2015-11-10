using UnityEngine;
using System.Collections;

public class HanzoController : MonoBehaviour {
	public int healt = 3;
	private int itemCount=1;
	public Transform itemSpawner;
	public GameObject HItem;
	public GameObject EItem;

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool validMove = true;
	[HideInInspector] public bool death = false;

	public float moveForce = 365f;
	public float maxSpeed ;
	public GameObject atackTrigger;
	
	public Transform groundCheck;
	private bool atacking = false;
	[HideInInspector] public bool running = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(healt <= 0){
			death = true;
			if(itemCount == 1){
				SpawnItem();
			}
		}
	
	}
	void FixedUpdate()
	{
		if(death){
			anim.SetBool("killed", true);
			StartCoroutine(Disapire());
		}
		
	}
	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void Move(){
		anim.SetBool ("run", true);
		int h;
		
		if(facingRight){
			h=1;
		}else{
			h=-1;
		}
		
		if (h * rb2d.velocity.x < maxSpeed && !atacking) {
			rb2d.AddForce (Vector2.right * h * moveForce);
			rb2d.velocity = new Vector2(maxSpeed * h ,rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
		}
		
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}
	}
	public void Stop(){
		anim.SetBool ("run", false);
		rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
	}

	public void Atack(int count){
		//anim.SetBool ("stab", true);
		//atackTrigger.SetActive (true);
		//StartCoroutine (EndAtack());
		switch(count){
			
		case 1:							
			anim.SetBool("log",false);
			anim.SetBool("stab",false);
			anim.SetBool("longAtack",true);
			atackTrigger.SetActive (true);
			StartCoroutine (EndAtack(count));
			break;
		case 2:
			anim.SetBool("longAtack",false);
			anim.SetBool("stab",false);
			anim.SetBool("log",true);
			StartCoroutine (EndAtack(count));
			break;
		case 3:
			
			anim.SetBool("log",false);
			anim.SetBool("longAtack",false);
			anim.SetBool("stab",true);
			atackTrigger.SetActive (true);
			StartCoroutine (EndAtack(count));
			break;
		case 4:
			anim.SetBool("log",false);
			anim.SetBool("longAtack",false);
			anim.SetBool("stab",false);
			break;
		default:
			break;
			
		}
		
	}
	IEnumerator EndAtack(int count){
		if(count==1){
			yield return new WaitForSeconds (0.70f);
			anim.SetBool ("longAtack", false);
			atackTrigger.SetActive (false);
		}else{
			if(count==2){
				yield return new WaitForSeconds (0.70f);
				anim.SetBool ("log", false);
				atackTrigger.SetActive (false);
			}else{
				if(count==3){
					yield return new WaitForSeconds (0.70f);
					anim.SetBool ("stab", false);
					atackTrigger.SetActive (false);
				}
			}
		}

	}
	public void HitEnemy (int damage){
		healt -= damage;
	}
	
	IEnumerator Disapire(){
		yield return new WaitForSeconds (1);
		
		Destroy (this.gameObject);
	}
	void SpawnItem(){
		itemCount = 0;
		int value = Random.Range(0,9);
		switch(value){
		case 0:
		case 1:
		case 2:
			Debug.Log("Life");
			Instantiate(HItem, itemSpawner.position, Quaternion.identity);
			break;
		case 3:
		case 4:
		case 5:
			Debug.Log("No spawneo nada");
			break;
		case 6:
		case 7:
		case 8:
		case 9:
			Debug.Log("power");
			Instantiate(EItem, itemSpawner.position, Quaternion.identity);
			break;
		}
	}
}
