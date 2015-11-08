using UnityEngine;
using System.Collections;

public class SlideCamera : MonoBehaviour {
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (new Vector3 (player.position.x ,transform.position.y, transform.position.z));
	}
}
