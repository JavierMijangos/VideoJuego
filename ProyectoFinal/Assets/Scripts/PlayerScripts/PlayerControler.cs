using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed ;
	public float jumpForce = 1000f;
	public float hitForce= 500f;

	public GameObject bodyTrigger;
	public GameObject normalAtackTrigger;
	public GameObject airAttackTrigger;
	public GameObject crouchAtackTrigger;

	public Transform groundCheck;
	
	
	private bool grounded = false;
	private bool crouching = false;
	private bool atacking = false;
	private bool getingHit = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	private float waitingTime = 1;
	private float timing=0;

	private float hitSide;
	

	void Start(){
		StartCoroutine (Atack ());
	}

	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			jump = true;
		}

		if (grounded) {
			anim.SetBool ("Jump", false);
		}

		if (Input.GetKey (KeyCode.DownArrow) && grounded) {
			anim.SetBool ("Crouch", true);
			crouching = true;
			Vector3 thePosition = bodyTrigger.transform.localPosition;
			thePosition.y = -0.84f;
			bodyTrigger.transform.localPosition = thePosition;
		} else {
			anim.SetBool ("Crouch", false);
			crouching = false;
			Vector3 thePosition = bodyTrigger.transform.localPosition;
			thePosition.y = -0.43f;
			bodyTrigger.transform.localPosition = thePosition;
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			anim.SetBool ("Atack", true);
			atacking = true;
			//StartCoroutine (Atack ());
		} else {
			anim.SetBool ("Atack", false);
			atacking = false;
		}

		if (Input.GetKeyDown (KeyCode.X) && grounded) {
			Debug.Log("Special");
			anim.SetBool ("Special", true);
		} else {
			anim.SetBool ("Special", false);
		}

		if(crouching && facingRight && Input.GetKey(KeyCode.LeftArrow) || crouching && !facingRight && Input.GetKey(KeyCode.RightArrow)){
			Flip();
		}

		if(!atacking){
			//StopCoroutine(Atack());
		}


	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		bool running = false;
		if (h != 0 && !crouching) {

			anim.SetBool ("Run", true);
			running = true;
		} else {
			anim.SetBool ("Run", false);
			h = 0;
			running = false;

		}

		if (h * rb2d.velocity.x < maxSpeed && !crouching && !atacking) {
			//rb2d.AddForce (Vector2.right * h * moveForce);
			rb2d.velocity = new Vector2(maxSpeed * h ,rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
		}

		
		//if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
		//	rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		//}

		if(!running){
			rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
		}

		if (h > 0 && !facingRight) {
			Flip ();
		}else if (h < 0 && facingRight){
			Flip ();
		}


		
		if (jump)
		{
			anim.SetBool("Jump", true);
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

		if (getingHit) {
			timing += 0.1f;
			if(hitSide < transform.position.x){
				rb2d.AddForce(new Vector2(hitForce, 0f));
			}else{
				rb2d.AddForce(new Vector2(-hitForce, 0f));
			}

		}
		
		if(waitingTime< timing){
			anim.SetBool ("GetHit", false);
			getingHit = false;
			timing=0;
		}
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator Atack(){
		//yield return new WaitForSeconds (0.2f);
		//anim.SetBool ("Atack", false);
		//atacking = false;
		bool airAtackValid = true;
		while (true) {
			if(atacking && grounded && !crouching){
				yield return new WaitForSeconds (0.1f);
				normalAtackTrigger.SetActive(true);
				yield return new WaitForSeconds (0.2f);
				normalAtackTrigger.SetActive(false);
			}

			if(atacking && grounded && crouching){
				yield return new WaitForSeconds (0.1f);
				crouchAtackTrigger.SetActive(true);
				yield return new WaitForSeconds (0.2f);
				crouchAtackTrigger.SetActive(false);
			}

			if(atacking  && !grounded && airAtackValid){
				yield return new WaitForSeconds (0.1f);
				airAttackTrigger.SetActive(true);
				airAtackValid = false;
				yield return new WaitForSeconds (0.2f);
				airAttackTrigger.SetActive(false);
			}

			if(grounded){
				airAtackValid = true;
			}

			if(!atacking || getingHit){
				normalAtackTrigger.SetActive(false);
			}
			yield return new WaitForSeconds (0.00f);
		}
		//yield return new WaitForSeconds (0.0f);
	}
	

	public void HitPlayer (float otherObjSide){
		anim.SetBool ("GetHit", true);
		hitSide = otherObjSide; //other.gameObject.transform.position.x;
		getingHit = true;
	}
}

