using UnityEngine;
using System.Collections;

public class Translator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (new Vector3 (.1f, 0f, 0f));
	}
}
