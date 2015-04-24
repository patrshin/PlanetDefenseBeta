using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class lvl5_jupiter : MonoBehaviour {
	
	public GameObject explosion;
	public GameObject boss;
	public bool hit;
	public bool god_mode;
	public Text no;
	float timer;
	bool dead;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(hit) {
			god_mode = true;
			hit = false;
			GameObject o = (GameObject) Instantiate (explosion);
			o.GetComponent<ExplosionParty>().range = 10;
			o.GetComponent<ExplosionParty>().frequency = 0.6f;
			o.GetComponent<ExplosionParty>().duration = 3;
			Vector3 pos = transform.position;
			pos.z = -8f;
			o.transform.position = pos;
			dead = true;
			timer = 1.5f;
			no.enabled = true;
		}

		if(dead) {
			timer -= Time.deltaTime;
			if(timer <= 0) {
				boss.GetComponent<lvl5_boss>().jupiter_dead = true;
				Destroy(this.gameObject);
			}

		}
	}
}
