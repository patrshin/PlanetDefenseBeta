using UnityEngine;
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

	bool shieldset = false;

	// Use this for initialization
	void Start () {
		spawn = 0;
		fire = true;
		lazer_time = lazer_cd;
		start_hp = hp;
	}
	
	// Update is called once per frame
	void Update () {

		if (hp <= (start_hp/2)){
			for(int i = 0; i < ast_spwners.Length; i++) {
				ast_spwners[i].SetActive(true);
			}
		}

		if (hp > (start_hp/3)) {
			shield_active = false;
		}

		if (hp <= (start_hp/3)){
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

		if (spawn%4 == 0) {
			fire = true;
		}

		else {
			fire = false;
		}

		if(hp <= 0){
			Application.LoadLevel("Congrats");
		}
	
	}



}
