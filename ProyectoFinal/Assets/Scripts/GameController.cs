using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool win;
	private bool slash;
	private bool lose;
	private bool over;
	/*private bool lose;
	private bool stand;

	private bool blood;*/

	public int tEsperaInicial;
	public int tAtaque;
	public float tValido;
	public float tFlash;
	private float tEsperaGanador;

	public GameObject exclamation;

	public DuelAnimationController PlayerController;
	public DuelAnimationController EnemyController;
	public CameraController Cam;
	public SlashAnimatorController slashController;

	// Use this for initialization
	void Start () {
		win = false;
		lose = false;
		slash = false;
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
		if(Time.time < tAtaque && Input.GetKey(KeyCode.Z) || Time.time > tValido + tAtaque && !lose && !win){
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
			tEsperaGanador = Time.time + 0.9f;
		}
		if(Time.time > tEsperaGanador && slash && !over && win){
			over = true;
			PlayerController.WinAnimation();
			EnemyController.DeathAnimation();
		}
		if(Time.time > tEsperaGanador && slash && !over && lose){
			over = true;
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
