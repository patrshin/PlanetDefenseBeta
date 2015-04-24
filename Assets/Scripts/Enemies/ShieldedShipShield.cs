﻿using UnityEngine;
using System.Collections;

public class ShieldedShipShield : MonoBehaviour {

	public GameObject noEffect;
	public float oscillationRate = .3f;
	public float health;
	GameObject shieldMesh;
	float baseY, baseX, baseZ;

	// Use this for initialization
	void Start () {
		shieldMesh = GetComponentInParent<MeshRenderer> ().gameObject;
		baseX = shieldMesh.transform.localScale.x;
		baseY = shieldMesh.transform.localScale.y;
		baseZ = shieldMesh.transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
		shieldMesh.transform.localScale = new Vector3 (
			baseX + Random.Range (-oscillationRate, oscillationRate),
			baseY + Random.Range (-oscillationRate, oscillationRate),
			baseZ
		);

		if(gameObject.tag == "shield") {
			if (health <= 0) {
				gameObject.transform.root.gameObject.GetComponent<boss_ship>().shield_active = false;
				//gameObject.transform.parent.gameObject.SetActive(false);
			}
		}
	}


	void OnCollisionEnter(Collision c) {

		if (c.gameObject.tag == "Proj_P1" ||
		    c.gameObject.tag == "Proj_P2") {


			Destroy(c.gameObject);
			GameObject o = (GameObject)Instantiate (noEffect);
			o.transform.position = c.gameObject.transform.position;

		}
	}
}
