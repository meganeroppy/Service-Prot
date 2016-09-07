using UnityEngine;
using System.Collections;

public class InfoText : MonoBehaviour {

	TextMesh mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMesh>();

		Transform t = GameObject.Find("Main Camera").transform;
		transform.LookAt ( t );
		transform.Rotate(0,180f,0);
	}
	
	public void UpDateLabel(string str){
		mesh.text = str;
	}
}
