using UnityEngine;
using System.Collections;

public class level4_camera : MonoBehaviour {

	public float scene_start = 1f;
	public GameObject Planet;
	public GameObject Jupiter;

	public bool scene_playing = false;
	public float speed;

	Vector3 planet_pos;
	Vector3 jupiter_pos;

	float timer = 0;
	public bool load = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Jupiter == null && !scene_playing ) {
			float step = speed * Time.deltaTime * 5;
			planet_pos.x = Planet.transform.position.x;
			planet_pos.y = Planet.transform.position.y;
			planet_pos.z = -10;
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);

			if(transform.position.x == Planet.transform.position.x && transform.position.y == Planet.transform.position.y) {
				timer += Time.deltaTime;
			}

			if(timer > 1) {
				load = true;
			}
		}

		if(Jupiter != null)
			scene_start -= Time.deltaTime;

		if(scene_start <= 0 && Jupiter != null)
			scene_playing = true;

		if(Jupiter != null){
			if(scene_playing){
				float step = speed * Time.deltaTime * 5;
				jupiter_pos.x = Jupiter.transform.position.x;
				jupiter_pos.y = Jupiter.transform.position.y;
				jupiter_pos.z = -10;
				transform.position = Vector3.MoveTowards(transform.position, jupiter_pos, step);
			}


		}

		if(load) {
				Application.LoadLevel("Level_4_Real");
		}
	}
}
