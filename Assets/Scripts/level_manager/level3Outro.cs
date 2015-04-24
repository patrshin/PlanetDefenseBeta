using UnityEngine;
using System.Collections;

public class level3Outro : MonoBehaviour {

	private bool entering = false;
	private float enterTime = 5f;
	private float timer = 0f;
	// Use this for initialization
	void Start () {
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "playerBoost") {
				child.GetComponent<ParticleSystem>().enableEmission = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 go = new Vector3 (1, 0, 0);
		if (entering) {
			if (timer > enterTime) {
				Application.LoadLevel("level_4_Intro");
			}
			else {
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "playerBoost") {
						child.GetComponent<ParticleSystem>().enableEmission = false;
					}
				}
				Vector3 expanded = transform.localScale;
				if (expanded.x > 0) {
					expanded.x -= 0.035f;
					expanded.y -= 0.035f;
				}
				transform.localScale = expanded;
				this.transform.Rotate(Vector3.forward * Time.deltaTime * 40f);
				timer += Time.deltaTime;
			}
		}
		else {
			go = new Vector3 (7, 0, 0);
		}
		transform.Translate(go * Time.deltaTime);

	}
	void OnCollisionEnter(Collision other) {
		Debug.Log ("hit");

		if (other.gameObject.tag == "Wormhole") {
			entering = true;
		}
	}

}
