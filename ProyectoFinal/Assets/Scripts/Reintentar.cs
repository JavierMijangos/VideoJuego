using UnityEngine;
using System.Collections;

public class Reintentar : MonoBehaviour {

	private GUIStyle currentStyle = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void style(){
		currentStyle = new GUIStyle (GUI.skin.box);
		currentStyle.normal.textColor = Color.red;

		
		
	}

	void OnGUI () {
		// Make a background box

		GUI.Box(new Rect(0,0,Screen.width,Screen.height), "ESCOGE UNA OPCION");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-100,200,30), "REINTENTAR")) {
			Application.LoadLevel("Persecution");
		}
		
		
		if (GUI.Button (new Rect (Screen.width/2-100,Screen.height/2-50,200,30), "SALIR")) {
			//print ("you clicked the text button");
			Application.Quit();
			
		}
		
	}
}
