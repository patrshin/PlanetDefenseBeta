using UnityEngine;
using System.Collections;

public class lvl2_Intro : MonoBehaviour {

	public GameObject sun;
	public GameObject explosion;

	public bool expl;
	public bool hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (expl && hit) {
			if(transform.localScale.x > 400) {
				Application.LoadLevel("Level_2_Intro2");
			}
		}

	}

	void OnTriggerEnter(Collider c) {		
		if (c.gameObject.CompareTag ("sun")) {
			explosion.GetComponent<level2_script>().expansion_rate = 2;
			explosion.GetComponent<lvl2_Intro>().hit = true;
			Destroy(gameObject);

		}
	}
}
