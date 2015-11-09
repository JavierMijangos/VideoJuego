using UnityEngine;
using System.Collections;

public class HanzoTrigger : MonoBehaviour {
	private EnemyController Parent;
	private float tiempo;
	private bool bandera;
	float aux_tiempo;
	// Use this for initialization
	private int count;
	private int auxCount;
	void Start () {
		bandera = true;
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
		if (Time.time > tiempo && bandera) {
			count = getRandomNumber();
			auxCount = count;
			if (other.CompareTag("Ground")|| other.CompareTag("Player")) {
				Parent.accion(count);
			}
			tiempo = Time.time + 3;
			bandera = false;

		}
		if (Time.time > tiempo && !bandera) {

			count = getRandomNumber();
			Debug.Log(count);
			if(count==3){

				Parent.accion(3);
				tiempo = Time.time + 3;
				auxCount = count;
			}else{

				if(count == auxCount){
					Parent.accion(4);
					auxCount= count;
					tiempo = Time.time + 1;
				}else{
					Parent.accion(count);
					auxCount = count;
					tiempo = Time.time + 3;
				}


			}		
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
