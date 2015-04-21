using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class intro_player : MonoBehaviour {

	public float speed;
	public float angle_speed;
	public float playerNum;
	public Text OMG;

	bool done;

	public Vector3 target0;
	public Vector3 target1;

	public bool signal;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (OMG.enabled) {

			float step = speed * Time.deltaTime;

			if(playerNum == 0) {
				transform.position = Vector3.MoveTowards(transform.position, target0, step);
			}
			if(playerNum == 1) {
				transform.position = Vector3.MoveTowards(transform.position, target1, step);
			}
		}

		if(playerNum == 0) {
			if(OMG.enabled) {
				if(transform.position == target0) {
					OMG.enabled = false;
					done = true;

				}
			}

			if(done && !signal) {
				if(transform.rotation.eulerAngles.z < 45f) {
					transform.RotateAround(Vector3.zero, Vector3.forward, angle_speed * Time.deltaTime);
				}
			}

			if(signal) {
				if(!(transform.rotation.eulerAngles.z < 360 && transform.rotation.eulerAngles.z > 359.5)) {
					if(transform.rotation.eulerAngles.z > 0)
						transform.RotateAround(Vector3.zero, -Vector3.forward, angle_speed * Time.deltaTime);
				}
			}
		}

		if(playerNum == 1) {
			if(OMG.enabled) {
				if(transform.position == target0) {
					OMG.enabled = false;
					done = true;
				}
			}

			if (signal) {
				if(transform.rotation.eulerAngles.z < 45f) {
					transform.RotateAround(Vector3.zero, Vector3.forward, angle_speed * Time.deltaTime);
				}
			}
		}
	}
}
