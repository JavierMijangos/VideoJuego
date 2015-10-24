using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private Camera camara;

	// Use this for initialization
	void Start () {
		camara = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void RenderSlash(){
		camara.cullingMask = 1 << 8;
	}

	public void RenderDefault(){
		camara.cullingMask = 1 << 0;
	}
}
