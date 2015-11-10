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


	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(350,90,130,110), "MENU PRINCIPAL");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(365,120,100,20), "JUGAR")) {
			Application.LoadLevel("Persecution");
		}

		
		if (GUI.Button (new Rect (365,160, 100, 20), "SALIR")) {
			//print ("you clicked the text button");
			Application.Quit();

		}

	}
}
