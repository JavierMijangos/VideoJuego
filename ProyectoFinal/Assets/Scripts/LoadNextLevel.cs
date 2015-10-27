using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)){
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
}
