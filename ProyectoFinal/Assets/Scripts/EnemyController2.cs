using UnityEngine;
using System.Collections;

public class EnemyController2 : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	//[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed ;
	float tiempo;
	
	//public Transform groundCheck;
	
	
	//private bool grounded = false;
	//	private bool crouching = false;
	private bool atacking = false;
	private Animator anim;

	
	
	// Use this for initialization
	void Awake () 
	{
		Flip ();
		anim = GetComponent<Animator>();

		tiempo = Time.time + 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
				
		if (Input.GetKey (KeyCode.Z)) {
			anim.SetBool ("gethit", true);
			atacking = true;
			//StartCoroutine (Atack ());
		} else {
			anim.SetBool ("gethit", false);
			atacking = false;
		}
		
		
		if(!atacking){
			//StopCoroutine(Atack());
		}
	}

	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public void accion (){

		anim.SetBool("atackKuro",true);											
					
	}
	void OnTriggerStay2D(Collider2D other){
		if (Time.time > tiempo) {
			tiempo = Time.time + 3;
			anim.SetBool("atackKuro",true);											
		}

	}
	void OnTriggerExit2D(Collider2D other){
			
	}
}
