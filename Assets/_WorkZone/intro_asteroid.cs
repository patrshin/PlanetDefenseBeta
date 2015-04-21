using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class intro_asteroid : MonoBehaviour {

	public Text OMG;
	public Text MOAR;
	bool done = false;
	bool finished = false;
	public bool p2;
	public GameObject player;
	float load_time = 0f;

	// Use this for initialization
	void Start () {
		//OMG = GameObject.Find ("OMG").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < 38 && transform.position.y > -40) {
			if(!done){
				OMG.enabled = true;
				done = true;
				if(p2) {
					player.GetComponent<intro_player>().signal = true;
				}
			}
		}

		if(transform.position.y > 38 && done) {
			MOAR.enabled = true;
			finished = true;
		}

		if(finished) {
			load_time += Time.deltaTime;
		}

		if(load_time > 3) {
			Application.LoadLevel("Level_1");
		}
	}
}
