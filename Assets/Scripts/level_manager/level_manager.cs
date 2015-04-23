﻿using UnityEngine;
using System.Collections;

public class level_manager : MonoBehaviour {

	public bool level1_boss = false;
	public bool level1_scene_done = false;
	public Vector3 level1_boss_pos;
	public Vector3 planet_pos;
	public float speed;
	public GameObject Planet;

	public float boss_x = 38.3f;

	public bool level2_scene_done = false;
	public float load_level_cd;
	public float load_level_delay = 3f;

	public GameObject UI;





	// Use this for initialization
	void Start () {
		//level1_boss_pos.z = -10;
		//level1_boss_pos.x = boss_x;
		//UI = GameObject.Find ("Player_UI_Overlay");
	}
	
	// Update is called once per frame
	void Update () {
		if (!level1_boss && Planet != null) {
			float step = speed * Time.deltaTime * 5;
			planet_pos.x = Planet.transform.position.x;
			planet_pos.y = Planet.transform.position.y;
			planet_pos.z = -10;
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
		}

		if(level1_boss){
			float step = speed * Time.deltaTime * 5;
			level1_boss_pos.z = -10f;
			//level1_boss_pos.x = boss_x;
			transform.position = Vector3.MoveTowards(transform.position, level1_boss_pos, step);
			commandEnemies("Asteroid_P1","Freeze");
			commandEnemies("Asteroid_P2","Freeze");
			UI.SetActive(false);
		}

		if(level1_scene_done){
			float step = speed * Time.deltaTime * 5;
			planet_pos.x = Planet.transform.position.x;
			planet_pos.y = Planet.transform.position.y;
			planet_pos.z = -10;
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
			if(transform.position.x == Planet.transform.position.x && transform.position.y == Planet.transform.position.y){
				//level1_scene_done = false;
				UI.SetActive(true);
			}
			commandEnemies("Asteroid_P1","unFreeze");
			commandEnemies("Asteroid_P2","unFreeze");

		}

		if(level2_scene_done) {
			load_level_cd -= Time.deltaTime;

			if(load_level_cd <= 0)
				Application.LoadLevel("Level_2_Real");
		}

	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Fake_Player")) {
			Destroy(c.gameObject);
		}

		if (c.gameObject.CompareTag ("mars")) {
			Destroy(c.gameObject);
			level2_scene_done = true;
			load_level_cd = load_level_delay;
		}

	}

	void commandEnemies(string tag,string command){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(tag); 
		
		for(var i = 0; i<gos.Length; i++){
			gos[i].SendMessage(command);
		}
	}
}
