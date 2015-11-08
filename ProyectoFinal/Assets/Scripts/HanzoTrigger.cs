using UnityEngine;
using System.Collections;

public class HanzoTrigger : MonoBehaviour {
	private EnemyController Parent;
	private 
	float tiempo;
	float aux_tiempo;
	// Use this for initialization
	private int count;
	void Start () {
		tiempo = Time.time;
		//aux_tiempo = Time.time;
		Parent = gameObject.GetComponentInParent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int getRandomNumber(){
		int primero = 1;
		int final = 4;

		return Random.Range (primero, final);


	}

	void OnTriggerStay2D(Collider2D other){
		if (Time.time > tiempo) {
			count = getRandomNumber();
			
			if (other.CompareTag("Ground")|| other.CompareTag("Player")) {
					
				Parent.accion(count);
			}
			tiempo = Time.time + 3;

		}



								
	}

	void OnTriggerExit2D(Collider2D other){

	}
	public void mandarAccion(Collider2D other){
		count = getRandomNumber();
		if (other.tag == "Player") {
			Debug.Log(count);
			Parent.accion(count);
		}
	}
}
