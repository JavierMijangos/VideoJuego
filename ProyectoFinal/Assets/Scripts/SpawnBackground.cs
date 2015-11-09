using UnityEngine;
using System.Collections;

public class SpawnBackground : MonoBehaviour {
	public GameObject bG;
	public Transform trans;
	// Use this for initialization
	void Start () {
		//parentTrans = this.gameObject.GetComponentInParent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			Vector3 pos = new Vector3(trans.position.x, trans.position.y);
			Instantiate(bG,pos, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
