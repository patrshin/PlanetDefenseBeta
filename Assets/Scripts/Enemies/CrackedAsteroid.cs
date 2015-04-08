﻿using UnityEngine;
using System.Collections;

public class CrackedAsteroid : MonoBehaviour {
	Health health;
	public GameObject asteroid;
	public int numChildren = 2;
	public Vector3 contact_pt;

	// Use this for initialization
	void Start () {
		GetComponent<AsteroidMesh>().GenerateMesh();
		health = GetComponent<Health>();
		health.registerDamageCallback(Break);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void giveProjectile(GameObject proj){
		contact_pt = proj.transform.position;
	}


	void Break(GameObject other) {
		//if (health.current() < .5f * (health.max())) {
			Destroy (gameObject);
			for(int i = 0; i < numChildren; ++i) {
				GameObject o = (GameObject) Instantiate(asteroid);

				o.transform.parent = transform.parent;
				var par = transform.parent;
				transform.parent = null;

				o.transform.position = transform.position + 
					new Vector3(
						Mathf.Cos (Random.Range (0, 360)) * transform.localScale.magnitude/4f,
						Mathf.Sin (Random.Range (0, 360)) * transform.localScale.magnitude/4f,
						0
					);
					
				o.GetComponent<Rigidbody>().velocity = 
					Quaternion.Euler(0, 0, Util.getAngleVector(
						contact_pt, o.transform.position)) * (rigidbody.velocity);
				transform.parent = par;

			//o.transform.parent = par;

				
			}
		//}

	}
}
