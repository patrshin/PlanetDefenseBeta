using UnityEngine;
using System.Collections;

public class ExplosionParty : MonoBehaviour {

	public float range = 20;
	public float frequency = 1.0f;
	public float duration = 2f;
	public GameObject exp;

	float time = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.deltaTime;
		if (Random.Range(0f, 1f) <= frequency) {
			GameObject o = (GameObject)Instantiate (exp);
			o.transform.position = transform.position +
				new Vector3 (
				Random.Range (-range, range),
				Random.Range (-range, range), 0);
		}
		if (time >= duration)
			Destroy (gameObject);

	}
}
