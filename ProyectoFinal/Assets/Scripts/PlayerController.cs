using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	
	// Use this for initialization
	void Start()
	{

		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		
		if (Input.GetKey (KeyCode.H)) {
			animator.SetInteger ("Action", 2);
		} else if (vertical > 0) {
			animator.SetInteger ("Action", 1);
		} else if (horizontal > 0) {

			animator.SetInteger ("Action", 3);
			transform.Translate (Vector3.right * 2 * Time.deltaTime);
		} else if (horizontal <= 0) {
			animator.SetInteger ("Action", 0);
		}
	}
}
