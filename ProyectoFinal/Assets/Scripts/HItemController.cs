using UnityEngine;
using System.Collections;

public class HItemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Beat());
		StartCoroutine (Despawn());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Beat(){
		yield return new WaitForSeconds (0);
		while (true){
			yield return new WaitForSeconds (0.2f);
			transform.localScale = new Vector3 (2,2);
			yield return new WaitForSeconds(0.5f);
			transform.localScale = new Vector3 (1,1);
		}
	}

	IEnumerator Despawn(){
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}

}
