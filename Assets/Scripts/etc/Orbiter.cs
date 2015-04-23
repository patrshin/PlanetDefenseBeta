using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {
	public string orbitingTag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.FindGameObjectWithTag (orbitingTag);
		transform.RotateAround (obj.transform.position, new Vector3 (0, 0, 1), Time.deltaTime * 10);
	}
}
