using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class planet_lvl3 : MonoBehaviour {

	public Image			hp_bar;
	private Vector3 move_to;
	public bool hit;
	bool back_off;
	public float kb_speed;
	public float dmg;

	public float contact_kb_distance;
	public bool contact_point = false;

	public bool lvl3 = false;
	public bool lvl5 = false;
	public bool lvl4 = false;
	public bool lvl41 = false;

	Vector3 contact_pos;

	private GameObject hitObject;

	Vector3 temp;

	public bool endLevel3 = false;
	private float Level3Endtimer = 0f;
	private float Level3EndDuration = 3f;

	void Start() {
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();
		temp = new Vector3(0,0,0);
	}

	void Update() {

		if (endLevel3) {		
			Application.LoadLevel("level_3_Outro");
		}

		if(lvl3){
			temp.x = transform.position.x;
			temp.y = 160f;
			temp.z = transform.position.z;

			if (transform.position.y < 0) {
				temp.y = -160f;
			}
		}

		if (lvl4) {
			Vector3 expanded = transform.localScale;
			if (expanded.x < 7) {
				expanded.x += 0.05f;
				expanded.y += 0.05f;
				transform.localScale = expanded;
				this.transform.Rotate(Vector3.forward * Time.deltaTime * 40f);
			}
			else {
				if (transform.eulerAngles.z < 359f) {
					Debug.Log(transform.eulerAngles.z);
					this.transform.Rotate(Vector3.forward * Time.deltaTime * 80f);
				}
				else {
					lvl4 = false;

				}
				GetComponent<AudioSource>().Stop();
				lvl41 = true;
			}
		}

		if(lvl5){
			temp.x = transform.position.x;
			temp.y = transform.position.y;
			temp.z = transform.position.z;
			
			if (transform.position.y < -60f) {
				temp.y = -60f;
			}
			else if (transform.position.y > -33f) {
				temp.y = -33f;
			}
			if (transform.position.x < -48) {
				temp.x = -48f;
			}
			if (transform.position.x > 48) {
				temp.x = 48f;
			}
		}

		if(!contact_point){
			if (hit) {
				move_to = gameObject.transform.position;
				move_to.x -= 30f;
				hit = false;
				back_off = true;
			}
			
			float step = kb_speed * Time.deltaTime * 5;

			if(back_off){
				transform.position = Vector3.MoveTowards(transform.position, move_to, step);
			}

			if(gameObject.transform.position == move_to)
				back_off = false;
		}

		else {
			if (hit) {
				move_to = gameObject.transform.position;
				move_to.x -= (contact_pos.x - transform.position.x)*contact_kb_distance;
				move_to.y -= (contact_pos.y - transform.position.y)*contact_kb_distance;
				hit = false;
				back_off = true;
			}
			
			float step = kb_speed * Time.deltaTime * 5;
			
			if(back_off){
				transform.position = Vector3.MoveTowards(transform.position, move_to, step);
			}
			
			if(gameObject.transform.position == move_to)
				back_off = false;
		}

		if (lvl3) {
			if(transform.position.y > 160 || transform.position.y < -160)
				transform.position = temp;
		}

		if (lvl5) {
			if(transform.position.y > -33 || transform.position.y < -60 ||
			   transform.position.x > 48 || transform.position.x < -48)
				transform.position = temp;
		}



	}

	// Use this for initialization
	void OnCollisionEnter(Collision c) {
		if (c.gameObject.layer == 20) {
			hitObject = c.gameObject;
			hp_bar.fillAmount -= dmg;
			hit = true;

			ContactPoint contact = c.contacts[0];
			contact_pos = contact.point;
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "end_lvl1") {
			Application.LoadLevel("level_2_Intro");
		}

		if (c.tag == "end_lvl3") {
			endLevel3 = true;
		}
		
	}
}
