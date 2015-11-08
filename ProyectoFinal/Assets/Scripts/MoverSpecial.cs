using UnityEngine;
using System.Collections;

public class MoverSpecial : MonoBehaviour {
	public float speed;
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(speed * Time.deltaTime,0,0));

		if(transform.position.x < player.position.x -13 ||transform.position.x > player.position.x + 13){
			Destroy(this.gameObject);
		}
	}


}
