﻿using UnityEngine;
using System.Collections;

public class boss_ship : MonoBehaviour {

	public bool shield_active = false;

	public GameObject shield;
	public GameObject planet;
	public float speed;

	Vector3 planet_pos;

	public float spawn;
	public bool fire;

	public float hp;
	float start_hp;

	public bool lazoring;
	public float lazer_cd;
	public float lazer_time;
	public GameObject Laser_Left;
	public GameObject Laser_Right;

	public GameObject[] ast_spwners;
	public GameObject ship_spwners;
	public GameObject explosionParty;
	public GameObject flashOut;

	bool shieldset = false;

	public float shield_health;
	public float ast_health;

	public Color defaultColor;
	Color origColor;
	private bool colorChanged = false;
	bool hurtAnimationActive = false;
	SpriteRenderer rend;
	public Color hitColor;
	public float colorChangeCooldown = 1f;
	private float colorChangeTimer;

	// Use this for initialization
	void Start () {
		spawn = 0;
		fire = true;
		lazer_time = lazer_cd;
		start_hp = hp;
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0){
			// Disable EVERYTHING
			for(int i = 0; i < transform.childCount; ++i) {
				transform.GetChild(i).gameObject.SetActive(false);
			}
			for(int i = 0; i < ast_spwners.Length; i++) {
				ast_spwners[i].SetActive(false);

			}
			ship_spwners.SetActive(false);
			shield.SetActive(false);



			DeathAnimation();
			return;
		}







		if (hp <= ast_health){
			for(int i = 0; i < ast_spwners.Length; i++) {
				ast_spwners[i].SetActive(true);
			}
		}

		if (hp > shield_health) {
			shield_active = false;
		}

		if (hp <= shield_health && !lazoring){
			ship_spwners.SetActive(true);
			if(!shieldset) {
				shield_active = true;
				shieldset = true;
			}
		}
		
		if (!shield_active) {
			shield.SetActive(false);
		}
		else {
			shield.SetActive(true);
		}

		lazer_time -= Time.deltaTime;

		if(lazer_time <= 0) {
			float chance = Random.Range(0f, 1f);
			if(chance < .5f) {
				Laser_Left.GetComponent<boss_lazoring>().fire = true;
				lazer_time = 999f;
			}
			else {
				Laser_Right.GetComponent<boss_lazoring>().fire = true;
				lazer_time = 999f;
			}
		}

		planet_pos.x = planet.transform.position.x;
		planet_pos.y = transform.position.y;
		planet_pos.z = transform.position.z;

		float step = speed * Time.deltaTime;

		if(!lazoring) {
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
		}
		if(lazoring) {
			shield.SetActive(false);
		}

		if (spawn%4 == 0) {
			fire = true;
		}

		else {
			fire = false;
		}

		if (colorChanged) {
			colorChangeTimer += Time.deltaTime;
			if (colorChangeTimer > colorChangeCooldown) {
				rend.color = defaultColor;
				colorChanged = false;
				colorChangeTimer = 0f;
				hurtAnimationActive = false;
			}
		}

		if (hurtAnimationActive) {
			rend.color = Color.Lerp(rend.color, origColor, 0.1f);
		}
	
	}

	bool startedDeathAnimation = false;
	enum DeathPhase {
		CameraPan,
		FirstFlash,
		SecondShaking,
		ExplosionPhase,
		End
	};
	DeathPhase phase = DeathPhase.CameraPan;
	float deathTime = 0f;
	Vector3 origPos;
	void DeathAnimation() {
		deathTime += Time.deltaTime;

		switch (phase) {
		case DeathPhase.CameraPan:
			Vector3 temp = Vector3.Lerp(Camera.main.transform.position, transform.position, 1f*Time.deltaTime);
			temp.z = Camera.main.transform.position.z;
			Camera.main.transform.position = temp;
			if (deathTime > 1.5) {
				phase = DeathPhase.FirstFlash;
			}
			break;
		case DeathPhase.FirstFlash:
			origPos = transform.position;
			GameObject o = (GameObject) Instantiate (flashOut);
			o.GetComponent<FadeToWhite>().fadeOutRate = .4f;

			GameObject expFirst = (GameObject) Instantiate(explosionParty);
			expFirst.transform.position = transform.position;
			expFirst.GetComponent<ExplosionParty>().duration = 2f;
			expFirst.GetComponent<ExplosionParty>().range = 2f;
			expFirst.GetComponent<ExplosionParty>().frequency = .1f;
			phase = DeathPhase.SecondShaking;

			break;

		case DeathPhase.SecondShaking:
			transform.position = origPos + new Vector3(Random.Range(-1, 1), 0, 0);
			if (deathTime > 4f) {
				deathTime = 0f;
				phase = DeathPhase.ExplosionPhase;
			}
			break;
		case DeathPhase.ExplosionPhase:
			//createExplosion thing
			GameObject exp = (GameObject) Instantiate(explosionParty);
			exp.transform.position = transform.position;
			exp.GetComponent<ExplosionParty>().duration = 10f;
			exp.GetComponent<ExplosionParty>().range = 10f;

			exp = (GameObject) Instantiate(explosionParty);
			exp.transform.position = transform.position;
			exp.GetComponent<ExplosionParty>().duration = 10f;
			exp.GetComponent<ExplosionParty>().range = 15f;

			phase = DeathPhase.End;
			break;

		case DeathPhase.End:
			transform.position = origPos + new Vector3(Random.Range(-2, 2), 0, 0);
			GameObject fade = (GameObject) Instantiate (flashOut);
			fade.GetComponent<FadeToWhite>().inverse = true;
			fade.GetComponent<FadeToWhite>().fadeOutRate = .007f;


			if (deathTime > 6f)
				Application.LoadLevel("Credits");
			break;
		}

	}

	public void damageIndicator() {
		colorChanged = true;

		rend.color = hitColor;


		PlanetHurt ();
	}

	void PlanetHurt() {
		if (hurtAnimationActive) {
			rend.color = origColor;
		}
		origColor = rend.color;
		rend.color = Color.red;
		hurtAnimationActive = true;
	}


}
