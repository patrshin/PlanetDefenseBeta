using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enemy_mine : MonoBehaviour {
	
	//public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;
	
	public float flyTime 	= 1f;
	public float flyTimer 	= 0f;

	public float speed;

	public float drop_rate;

	public GameObject fuel1;
	public GameObject fuel2;

	Image hp;

	[HideInInspector]
	public bool hit = false;
	public bool detected = false;

	public GameObject Planet;
	public float minDistance;
	public float curDistance;

	private Color normalLight;
	
	// Use this for initialization
	void Start () {
		if (targetTags.Length != targetDamage.Length)
			Debug.LogError("Tag / Damage count mismatch");
		//GetComponent<Rigidbody>().AddForce(initialSpeed);
		hp = GameObject.Find ("HP").GetComponent<Image>();
		Planet = GameObject.FindGameObjectWithTag ("Planet");
		normalLight = GetComponent<Light> ().color;
	}
	
	// Update is called once per frame
	void Update () {

		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "Detector") {
				child.GetComponent<Transform>().RotateAround(this.transform.position, Vector3.forward, Time.deltaTime * 50f);
			}
		}

		if (detected) {	
			curDistance = Vector3.Distance (Planet.transform.position, transform.position);
			GetComponent<Light>().color = Color.red;
			transform.position = Vector3.MoveTowards(transform.position,Planet.transform.position,Time.deltaTime*3);
			if (curDistance > 100) {
				GetComponent<Light>().color = normalLight;
				detected = false;
			}
			if (GetComponent<Light>().intensity < 4) {
				GetComponent<Light>().intensity += Time.deltaTime*4;
			}
		}
		else {
			if (flyTimer > flyTime) {
				curDistance = Vector3.Distance (Planet.transform.position, transform.position);
				GetComponent<Light>().enabled = true;
				//rigidbody.velocity = rigidbody.velocity*0.5f;

				if (GetComponent<Light>().intensity < 4) {
					GetComponent<Light>().intensity += Time.deltaTime*4;
				}
				if (curDistance < minDistance) {
					detected = true;
				}
				
				
			}
			else{
				flyTimer += Time.deltaTime;
				transform.position += Vector3.left * Time.deltaTime * speed;
			}
		}

		if(hit){
			float drop_num = Random.Range(0f,1f);
			if(drop_num < drop_rate)
			{
				float fuel_pick = Random.Range(0f,1f);
				if(fuel_pick >= 0.5f){
					GameObject o = (GameObject)Instantiate (fuel1);
					o.transform.position = gameObject.transform.position;
				}
				else{
					GameObject o = (GameObject)Instantiate (fuel2);
					o.transform.position = gameObject.transform.position;
				}
			}
			GameObject ex = (GameObject)Instantiate (explosionPrefab);
			ex.transform.position = gameObject.transform.position;
			Destroy(gameObject);
		}

		
	}
//	void OnTriggerEnter(Collider other) {
//		//Debug.Log ("Mine Collision");
//		if (other.gameObject.tag == "Proj_P1" || other.gameObject.tag == "Proj_P2") {
//			GameObject o = (GameObject)Instantiate (explosionPrefab);
//			o.transform.position = transform.position;
//			Destroy (this.gameObject);
//		}
//
//		if (other.gameObject.tag == "Planet") {
//			GameObject o = (GameObject)Instantiate (explosionPrefab);
//			o.transform.position = transform.position;
//			hp.fillAmount -= 0.15f;
//			Destroy (this.gameObject);
//		}
//	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Planet") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			hp.fillAmount -= 0.15f;
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Asteroid_P1" || other.gameObject.tag == "Asteroid_P2" || other.gameObject.tag == "CrackedAsteroid") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
	}


}