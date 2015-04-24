using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level3Outro : MonoBehaviour {

	private bool entering = false;
	private float enterTime = 1f;
	private float timer = 0f;
	public Text comment_1;
	public Text comment_2;

	// Use this for initialization
	void Start () {
		GameObject comment_obj = GameObject.Find ("comment_1");
		comment_1 = comment_obj.GetComponent<Text> ();
		comment_obj = GameObject.Find ("comment_2");
		comment_2 = comment_obj.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 go = new Vector3 (1, 0, 0);
		if (timer > enterTime) {
			comment_1.enabled = true;
		}
		if (timer > enterTime+4f) {
			comment_2.enabled = true;
			comment_1.enabled = false;
		}
		if (entering) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "playerBoost") {
					child.GetComponent<ParticleSystem>().enableEmission = false;
					child.GetComponent<AudioSource>().Stop();
				}
			}
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
			Vector3 expanded = transform.localScale;
			if (expanded.x > 0) {
				expanded.x -= 0.035f;
				expanded.y -= 0.035f;
			}
			else {
				Destroy(gameObject);
				Application.LoadLevel("level_4_Intro");
				return;
			}
			transform.localScale = expanded;
			this.transform.Rotate(Vector3.forward * Time.deltaTime * 40f);
		}
		else {
			go = new Vector3 (10, 0, 0);
		}
		transform.Translate(go * Time.deltaTime);
		timer += Time.deltaTime;
	}
	void OnCollisionEnter(Collision other) {
		Debug.Log ("hit");

		if (other.gameObject.tag == "Wormhole") {
			entering = true;
		}
	}

}
