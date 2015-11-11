using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Jugar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	int fontSize = 30;
	void OnGUI () {
		// Make a background box
		GUI.skin.box.fontSize = fontSize;
		GUI.Box(new Rect(0,0,Screen.width,Screen.height), "MENU PRINCIPAL");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-100,200,50), "JUGAR")) {
			Application.LoadLevel("DuelInstructions");
		}

		
		if (GUI.Button (new Rect (Screen.width/2-100,Screen.height/2, 200, 50), "SALIR")) {
			//print ("you clicked the text button");
			Application.Quit();

		}

	}
}
