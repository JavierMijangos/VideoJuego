using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool death = false;
	public float moveForce = 365f;
	public float maxSpeed ;
	public float jumpForce = 1000f;
	public float hitForce= 500f;

	public GameObject bodyTrigger;
	public GameObject normalAtackTrigger;
	public GameObject airAttackTrigger;
	public GameObject crouchAtackTrigger;
	public GameObject specialObj;

	public Transform groundCheck;
	public Transform specialSpawner;
	
	
	private bool grounded = false;
	private bool crouching = false;
	private bool atacking = false;
	private bool getingHit = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	private float waitingTime = 1;
	private float timing=0;

	private float hitSide;

	private int healt;

	public RectTransform healthTranform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	public int maxHealt;

	public Image visualHealth;
	
	private int power;
	public RectTransform powerTranform;
	private float cachedPowerY;
	private float minXPowerValue;
	private float maxXPowerValue;
	public int maxPower;
	
	public Image visualPower;

	void Start(){
		cachedY = healthTranform.position.y;
		maxXValue = healthTranform.position.x;
		minXValue = healthTranform.position.x - healthTranform.rect.width;
		healt = 100;

		cachedPowerY = powerTranform.position.y;
		maxXPowerValue = powerTranform.position.x;
		minXPowerValue = powerTranform.position.x - powerTranform.rect.width;
		power = 0;
		HandlePower ();

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

		//Eliminar luego---
		if(Input.GetKeyDown(KeyCode.J)){
			GivePower();
		}
		//-----------------

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded && !death) {
			jump = true;
		}

		if (grounded) {
			anim.SetBool ("Jump", false);
		}

		if (Input.GetKey (KeyCode.DownArrow) && grounded && !death) {
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

		if (Input.GetKeyDown (KeyCode.Z) && !death) {
			anim.SetBool ("Atack", true);
			atacking = true;
			//StartCoroutine (Atack ());
		} else {
			anim.SetBool ("Atack", false);
			atacking = false;
		}

		if (Input.GetKeyDown (KeyCode.X) && grounded && power == 3 && !death) {
			Debug.Log("Special");
			power = 0;
			anim.SetBool ("Special", true);
			StartCoroutine(SpecialAtack());
		} else {
			anim.SetBool ("Special", false);
		}

		if(crouching && facingRight && Input.GetKey(KeyCode.LeftArrow) || crouching && !facingRight && Input.GetKey(KeyCode.RightArrow) && !death){
			Flip();
		}

	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		bool running = false;

		if (h != 0 && !crouching && !death) {
			anim.SetBool ("Run", true);
			running = true;
		} else {
			anim.SetBool ("Run", false);
			h = 0;
			running = false;
		}

		if (h * rb2d.velocity.x < maxSpeed && !crouching && !atacking && !death) {
			rb2d.velocity = new Vector2(maxSpeed * h ,rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(0f,rb2d.velocity.y);
		}

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

		if (getingHit && !death) {
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

	}

	IEnumerator SpecialAtack(){
		yield return new WaitForSeconds (1f);
		Quaternion rot;

		if (facingRight) {
			rot = Quaternion.Euler (new Vector3 (0, 0));
		} else {
			rot = Quaternion.Euler (new Vector3 (0,180));
		}

		HandlePower();
		Instantiate (specialObj, specialSpawner.position, rot);
	}
	

	public void HitPlayer (float otherObjSide, int damage){
		anim.SetBool ("GetHit", true);
		hitSide = otherObjSide; //other.gameObject.transform.position.x;
		getingHit = true;
		healt -= damage;
		HandleHealth ();

		if(healt <= 0){
			anim.SetBool("Death", true);
			death = true;
			StartCoroutine(die ());
		}
	}

	IEnumerator die(){
		yield return new WaitForSeconds (0.02f);
		anim.SetBool("Death", false);
	}

	private void GiveHealth (){
		if(healt < maxHealt-5){
			healt += 5;
			HandleHealth();
		}
	}

	public void GivePower(){
		if(power < 3){
			power += 1;
			HandlePower();
		}
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin)*(outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleHealth(){
		float currentXValue = MapValues (healt, 0, maxHealt, minXValue, maxXValue);
		healthTranform.position = new Vector3 (currentXValue, cachedY);

		if (healt > maxHealt / 2) {
			visualHealth.color = new Color32 ((byte)MapValues (healt, maxHealt / 2, maxHealt, 255, 0), 255, 0, 255);
		} else {
			visualHealth.color = new Color32(255,(byte)MapValues(healt, 0, maxHealt/2, 0, 255), 0,255);
		}

	}

	private void HandlePower(){
		float currentXValue = MapValues (power, 0, maxPower, minXPowerValue, maxXPowerValue);
		powerTranform.position = new Vector3 (currentXValue, cachedPowerY);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("HealthItem")){
			GiveHealth();
			Destroy(other.gameObject);
		}
		if(other.CompareTag("EnergyItem")){
			GivePower();
			Destroy(other.gameObject);
		}

	}

}

