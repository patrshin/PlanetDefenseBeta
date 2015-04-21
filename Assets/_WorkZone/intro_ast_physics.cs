﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class intro_ast_physics : MonoBehaviour {

	public GameObject		planet;
	public GameObject[]	asteroids;
	public Vector3 		initialVelocity = new Vector3 (0, 0, 0);

	public float			terminalVelocity;
	public float 			bounceFactor;
	public Image			hp_bar;
	private float			bounceDelay;

	private bool			cutScene = false;
	private Vector3			prevVelocity;

	Rigidbody  physicsBase;

	public float bounce_delay;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	void Start() {
		physicsBase = GetComponent<Rigidbody>();
		rigidbody.velocity = initialVelocity;
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();
		bounceDelay = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (cutScene)
			return;

		if (bounceDelay > 0)
			bounceDelay -= Time.deltaTime;


		GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag ("Planet");
		 
		foreach(GameObject planet in celestialBodies){
			Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
			rigidbody.AddForce(_force);
		}

		if (rigidbody.velocity.magnitude > terminalVelocity) {
				rigidbody.velocity = rigidbody.velocity.normalized * terminalVelocity;
		}



//		Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
//
//		rigidbody.AddForce (_force);
//		if (rigidbody.velocity.magnitude > terminalVelocity) {
//			rigidbody.velocity = rigidbody.velocity.normalized * terminalVelocity;
//		}
	
	}


//	Just google Vector Reflection for better reference material
//	This code I was originally moving the projectile by reassigning its xy-pos
//	I believe that dir will probably corresspond to rigidbody.velocity
//	Probably want to make sure collision detection is strict

//	ContactPoint hit_pt = c.contacts [0];
//	dir = dir - 2 * (Vector3.Dot (dir, hit_pt.normal)) * hit_pt.normal;
//	Vector3 dir; << Corressponds to the direction of the asteroid
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player") && bounceDelay <= 0f) {
			ContactPoint hit_pt = c.contacts [0];
			Vector3 dir = rigidbody.velocity;
			dir = dir - 2 * (Vector3.Dot (dir, hit_pt.normal)) * hit_pt.normal;
			rigidbody.velocity = bounceFactor*dir;
			//rigidbody.AddRelativeForce(2f*dir,ForceMode.VelocityChange);
			bounceDelay = bounce_delay;
			c.gameObject.GetComponent<intro_player>().signal = true;
		}

		else if(c.gameObject.CompareTag ("Planet")) {
			hp_bar.fillAmount -= physicsBase.mass/500;
			Destroy(gameObject);

		}

	}

	public void Freeze(){
		prevVelocity = rigidbody.velocity;
		rigidbody.velocity = Vector3.zero;
		cutScene = true;
	}
	
	public void unFreeze(){
		rigidbody.velocity += prevVelocity;
		cutScene = false;
	}

}
