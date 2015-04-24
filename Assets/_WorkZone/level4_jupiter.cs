using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level4_jupiter : MonoBehaviour {

	level4_camera manager;
	public Text comment_1;
	GameObject obj;

	// Use this for initialization
	void Start () {
		GameObject comment_obj = GameObject.Find ("comment_1");
		comment_1 = comment_obj.GetComponent<Text> ();
		obj = GameObject.FindGameObjectWithTag ("Planet");
		manager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<level4_camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (obj.GetComponent<planet_lvl3>().lvl41) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();

			foreach (Transform child in allChildren) {
				if (child.name == "playerBoost") {
					child.GetComponent<ParticleSystem>().enableEmission = true;
				}
				if (child.name == "Audio Source") {
					if (!child.GetComponent<AudioSource>().isPlaying && this.GetComponent<Renderer>().isVisible)
						child.GetComponent<AudioSource>().Play ();
				}
			}
			if (transform.position.x <= -160) {
				comment_1.enabled = true;
			}
			
			if (transform.position.x <= -215) {
				manager.scene_playing = false;
				Destroy(gameObject);
			}


			Vector3 go = new Vector3 (-9, 0, 0);
			transform.Translate(go * Time.deltaTime);
		}
	}
}
