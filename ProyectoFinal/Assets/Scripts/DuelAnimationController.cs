using UnityEngine;
using System.Collections;

public class DuelAnimationController : MonoBehaviour {
	private Animator animator;
	public float distance;
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
		animator.SetBool ("lose", true);
	}
	
	public void WinAnimation(){
		animator.SetBool ("win", true);
	}
	
	void mover(){
		transform.position = new Vector3(distance,0,0);
	}
}
