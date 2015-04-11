using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarBlip : MonoBehaviour {

	static List<GameObject> allInstances = new List<GameObject>();


	bool inDeleteList = false;
	public float duration = 1;

	float time;
	// Use this for initialization
	void Start () {
		time = duration;

	}


	
	// Update is called once per frame
	void FixedUpdate () {
		time -= Time.deltaTime;
		GetComponentInChildren<SpriteRenderer> ().material.color = new Color (1, 1, 1, time / duration);
		if (time - duration*.9f < 0 && !inDeleteList) {
			inDeleteList = true;
			allInstances.Add (this.gameObject);
		}
			
	}


	static public void RemoveBlips() {
		foreach(GameObject o in allInstances)
			Destroy (o);

		allInstances.Clear();
	}
}
