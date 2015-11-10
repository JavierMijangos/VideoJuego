using UnityEngine;
using System.Collections;

public class SpawnHanzo : MonoBehaviour {

	public GameObject hanzo;
	private int nSpawn= 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name =="Player" && nSpawn == 0){
			Instantiate (hanzo, transform.position, Quaternion.identity);
			nSpawn = 1;
		}
	}
}
