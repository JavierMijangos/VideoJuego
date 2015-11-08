using UnityEngine;
using System.Collections;

public class KurokoController : MonoBehaviour {

	public int healt = 3;
	private int itemCount=1;
	public Transform itemSpawner;

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool validMove = true;
	[HideInInspector] public bool death = false;

	public float moveForce = 365f;
	public float maxSpeed ;
	//public float hitForce= 1f;

	public GameObject atackTrigger;
	
	public Transform groundCheck;
	
	
//	private bool grounded = false;
	//	private bool crouching = false;
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
	void Update () 
	{
		//grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

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
			anim.SetBool("GetHit", true);
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
		anim.SetBool ("Run", true);
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
		anim.SetBool ("Run", false);
		rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
	}

	public void Atack(){
		anim.SetBool ("Atack", true);
		atackTrigger.SetActive (true);
		StartCoroutine (EndAtack());

	}

	IEnumerator EndAtack(){
		yield return new WaitForSeconds (0.05f);
		anim.SetBool ("Atack", false);
		atackTrigger.SetActive (false);
	}

	public void HitEnemy (int damage){
		healt -= damage;
	}

	IEnumerator Disapire(){
		yield return new WaitForSeconds (2);

		Destroy (this.gameObject);
	}

	void SpawnItem(){
		itemCount = 0;
		int value = Random.Range(0,9);
		switch(value){
		case 0:
		case 1:
		case 2:
		case 3:
			Debug.Log("No spawneo nada");
			break;
		case 4:
		case 5:
		case 6:
		case 7:
			Debug.Log("Life");
			//Instantiate(life, itemSpawner.position, Quaternion.identity);
			break;
		case 8:
		case 9:
			Debug.Log("power");
			//Instantiate(power, itemSpawner.position, Quaternion.identity);
			break;
		}
	}
}
