using UnityEngine;
using System.Collections;

public class Reintentar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(350,90,130,110), "HAS PERDIDO");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(365,120,100,20), "REINTENTAR")) {
			//Application.LoadLevel(1);
			Application.LoadLevel("Persecution");
		}
		
		
		if (GUI.Button (new Rect (365,160, 100, 20), "SALIR")) {
			//print ("you clicked the text button");
			Application.Quit();
			
		}
		
	}
}
