using UnityEngine;
using System.Collections;

public class FadeToWhite : MonoBehaviour {


	public float fadeOutRate = .0001f;
	public bool inverse;
	float transparency = 1;
	SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		if (inverse)
			transparency = 0f;

		spr = GetComponent<SpriteRenderer> ();
		transform.position = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(0, 0, 1f) + Camera.main.transform.position;
		spr.color = new Color (1f, 1f, 1f, transparency);

		if (!inverse) {
			transparency -= fadeOutRate * Time.deltaTime;
			if (transparency <= 0)
				Destroy (gameObject);
		} else {
			transparency += fadeOutRate * Time.deltaTime;
			if (transparency >= 1f)
				Destroy (gameObject);	
		}	
	}
}
