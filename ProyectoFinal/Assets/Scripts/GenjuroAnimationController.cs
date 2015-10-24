using UnityEngine;
using System.Collections;

public class GenjuroAnimationController : MonoBehaviour {

	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void StandAnimation(){
		mover ();
		animator.SetBool ("stand", true);
	}
	
	public void DeathAnimation(){
		animator.SetBool ("win", true);
	}
	
	public void WinAnimation(){
		animator.SetBool ("lose", true);
	}

	void mover(){
		transform.position = new Vector3(-3,0,0);
	}
}
