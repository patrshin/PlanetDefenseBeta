using UnityEngine;
using System.Collections;

public class Pulsator : MonoBehaviour {

	Material m;

	float time;
	// Use this for initialization
	void Start () {
		m = GetComponent<SpriteRenderer> ().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += .1f;
		Color newColor = m.color;
		newColor.a = .5f + .5f*Mathf.Sin (time);
		m.color = newColor;
	}
}
