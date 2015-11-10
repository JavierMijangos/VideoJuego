using UnityEngine;
using System.Collections;

public class EItemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Despawn());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Despawn(){
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}
}
