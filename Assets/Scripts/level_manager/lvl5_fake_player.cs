using UnityEngine;
using System.Collections;

public class lvl5_fake_player : MonoBehaviour {

	public AudioSource sound_basic;
	public GameObject ProjectilePrefab;
	public float cooldown = 2f;

	
	// Use this for initialization
	void Start () {
		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;

		
		if(cooldown <= 0f){
			shootProjectile ();
		}

	}
	
	
	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (ProjectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile> ().initialSpeed = new Vector2 (0, 750);
		sound_basic.Play ();
		cooldown = 2f;
		//Debug.Log (o.GetComponent<Projectile>().initialSpeed);
	}
}
