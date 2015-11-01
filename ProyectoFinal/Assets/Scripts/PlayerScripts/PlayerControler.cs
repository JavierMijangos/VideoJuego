using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed ;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	
	
	private bool grounded = false;
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
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
		{
			jump = true;
		}

		if(grounded){
			anim.SetBool("Jump", false);
		}
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		bool running = false;
		if (h != 0) {

			anim.SetBool ("Run", true);
			running = true;
		} else {
			anim.SetBool ("Run", false);
			h = 0;
			running = false;

		}

		if (h * rb2d.velocity.x < maxSpeed ) {
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
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
