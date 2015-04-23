using UnityEngine;
using System.Collections;

public class Translator : MonoBehaviour {

	public Vector3 translationAmount = new Vector3(.1f, 0, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (translationAmount);
	}
}
