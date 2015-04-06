﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class explosion_script : MonoBehaviour {

	public float 			expansion_rate;
	public float 			explosion_radius;
	public float			EndSceneDelay	=	10f;
	private float			EndSceneCount	=	0f;

	Image hp;
	
	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.localScale.x <= explosion_radius)
			transform.localScale += new Vector3(expansion_rate, expansion_rate, 0);
		else {
			EndSceneCount += Time.deltaTime;
			if (EndSceneCount > EndSceneDelay)
				Application.LoadLevel("level_3_Real");
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("mars")) {
			c.gameObject.GetComponent<level2_planet> ().alive = false;
		} else if (c.gameObject.CompareTag ("Planet")) {
			hp.fillAmount = 0f;
		} else if (c.gameObject.CompareTag ("Asteroid_P1") || c.gameObject.CompareTag ("Asteroid_P2")
		           || c.gameObject.CompareTag("CrackedAsteroid") || c.gameObject.CompareTag("Proj_P1")
		           || c.gameObject.CompareTag("Proj_P2")) {
			Destroy (c.gameObject);
		} else {
		}
	}
}
