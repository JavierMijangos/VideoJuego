using UnityEngine;
using System.Collections;

public class SpawnKuroko : MonoBehaviour {

	public GameObject kuroko;
	private int nSpawn= 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && nSpawn == 0){
			Instantiate (kuroko, transform.position, Quaternion.identity);
			nSpawn = 1;
		}
	}
}
