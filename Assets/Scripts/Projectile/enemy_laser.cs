using UnityEngine;
using System.Collections;

public class enemy_laser : MonoBehaviour {
	
	public Vector3 angle;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;
	public float speed;
	public bool super;

	public float timer;

	public float rotate_speed;

	public GameObject boss;

	Vector3 pivot;
	
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		boss.GetComponent<boss_ship> ().lazoring = true;
		//timer = 0;
		//angle.x = 0.7071068f;
		//angle.y = -0.7071068f;

		pivot = transform.position;
	}
	
	void FixedUpdate () {
		Shoot ();
	}
	
	// Update is called once per frame
	void Shoot () {
		timer -= Time.deltaTime;
		if (transform.localScale.x < 80) {
			Vector3 expanded = transform.localScale;
			expanded.x += 1f;
			transform.localScale = expanded;
			transform.Translate(angle * Time.deltaTime * speed * 2, Space.World);
			
		} 
		else {
			Vector3 expanded = transform.localScale;
			Vector3 z1 = new Vector3(0,0,-1);
			transform.RotateAround(pivot, z1, rotate_speed * Time.deltaTime);
			if (timer <= 0) {
				if(expanded.y >= 0)
					expanded.y -= 0.013f;
				transform.localScale = expanded;
			}
			if(expanded.y <= 0) {
				boss.GetComponent<boss_ship>().lazoring = false;
				boss.GetComponent<boss_ship>().lazer_time = boss.GetComponent<boss_ship>().lazer_cd; 
				Destroy(this.gameObject);
			}
		}
		if (transform.localScale.x > 5){
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "ExpandedBeam" && child.GetComponent<Transform>().localScale.y < 3.5f) {
					Vector3 expanded = child.GetComponent<Transform>().localScale;
					expanded.y += 0.4f;
					child.GetComponent<Transform>().localScale = expanded;
				}
			}
		}
	}
}
