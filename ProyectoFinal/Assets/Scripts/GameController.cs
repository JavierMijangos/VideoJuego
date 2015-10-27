using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool win;
	private bool slash;
	private bool lose;
	private bool blooded;
	private bool over;
	/*private bool lose;
	private bool stand;

	private bool blood;*/

	public int tEsperaInicial;
	public int tAtaque;
	public float tValido;
	public float tFlash;
	private float tEsperaGanador;
	private float tDesangre;

	public GameObject exclamation;

	public DuelAnimationController PlayerController;
	public DuelAnimationController EnemyController;
	public CameraController Cam;
	public SlashAnimatorController slashController;

	public GameObject blood;
	public GameObject enemyHead;
	public GameObject playerHead;

	// Use this for initialization
	void Start () {
		win = false;
		lose = false;
		slash = false;
		blooded = false;
		over = false;
		RandomeTime ();
		tAtaque += tEsperaInicial;
		//tValido += tAtaque;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("Se acabo la espera");
		if(Time.time > tAtaque && !exclamation.activeInHierarchy && !win && !lose){
			exclamation.SetActive(true);
			//tValido += Time.time;
		}
		if(Time.time < tValido + tAtaque && Input.GetKey(KeyCode.Z) && !win && exclamation.activeInHierarchy){
			tFlash = Time.time + 0.2f;
			exclamation.SetActive(false);
			Cam.RenderSlash();
			slashController.SlashAnimation();
			win = true;
		}
		if(Time.time < tAtaque && Input.GetKey(KeyCode.Z) && !lose || Time.time > tValido + tAtaque && !lose && !win){
			tFlash = Time.time + 0.2f;
			exclamation.SetActive(false);
			Cam.RenderSlash();
			slashController.SlashAnimation();
			lose = true;
		}
		if(Time.time > tFlash && !slash){
			slash = true;
			PlayerController.StandAnimation();
			EnemyController.StandAnimation();
			Cam.RenderDefault();
			tDesangre=Time.time + 0.9f;
		}
		if (Time.time > tDesangre && slash && !over && win && !blooded) {
			blooded= true;
			Instantiate(blood, new Vector3(-3.60f,0,0), Quaternion.Euler(0,180,12));
			tEsperaGanador = Time.time + 0.9f;
		}
		if (Time.time > tDesangre && slash && !over && lose && !blooded) {
			blooded= true;
			Instantiate(blood, new Vector3(2.80f,0,0), Quaternion.Euler(0,0,32));
			tEsperaGanador = Time.time + 0.9f;
		}
		if(Time.time > tEsperaGanador && slash && !over && win && blooded){
			over = true;
			GameObject.FindWithTag("Blood").SetActive(false);
			Instantiate(enemyHead, new Vector3(-3.60f,0,0), Quaternion.Euler(0,180,12));
			PlayerController.WinAnimation();
			EnemyController.DeathAnimation();

		}
		if(Time.time > tEsperaGanador && slash && !over && lose && blooded){
			over = true;
			GameObject.FindWithTag("Blood").SetActive(false);
			Instantiate(playerHead, new Vector3(3.5f,0,0), Quaternion.identity);
			PlayerController.DeathAnimation();
			EnemyController.WinAnimation();
		}


	}

	void RandomeTime(){
		int randNum = Random.Range (0,4);
		switch (randNum){
		case 0:
			tAtaque = 2;
			break;
		case 1:
			tAtaque = 3;
			break;
		case 2:
			tAtaque = 4;
			break;
		case 3:
			tAtaque = 5;
			break;
		case 4:
			tAtaque = 6;
			break;
		}

	}
}
