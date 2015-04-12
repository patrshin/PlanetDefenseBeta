using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSentinel : MonoBehaviour {


	public GameObject expPrefab;

	Image hp;
	const int healthScale = 100;
	int fixedHealth = healthScale;
	MeshRenderer earthRender;
	Material origMat;
	Color origColor;
	bool hurtAnimationActive = false;
	bool deathAnimationActive = false;
	float deathTimer = 0f;


	Image hpred;


	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();
		hpred = GameObject.Find ("HPRed").GetComponent <Image>();
		earthRender = GetComponent<MeshRenderer> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		Debug.Log (hpred.fillAmount);
		if (hp.fillAmount < hpred.fillAmount) {
			hpred.fillAmount -= Time.deltaTime*0.25f;
		}

		if (Input.GetKey (KeyCode.Alpha1)) {
			Application.LoadLevel ("Level_1");
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			Application.LoadLevel ("Level_2_Real");
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			Application.LoadLevel ("level_3_Real");
		}

		if (Input.GetKey (KeyCode.Alpha4)) {
			Application.LoadLevel ("level_4_Real");
		}

		if (Input.GetKey (KeyCode.Alpha5)) {
			Application.LoadLevel ("level_5_Real");
		}

		// hurt
		if ((int)(hp.fillAmount * healthScale) != fixedHealth) {
			PlanetHurt ();
		}

		if (hurtAnimationActive) {
			earthRender.material.color = Color.Lerp(earthRender.material.color, origColor, 0.1f);
		}

		if (hp.fillAmount <= 0) {
			deathAnimationActive = true;
		}



		if (deathAnimationActive) {
			deathAnimation();
		}



	}


	void PlanetHurt() {
		if (hurtAnimationActive) {
			earthRender.material.color = origColor;
		}
		origColor = earthRender.material.color;
		earthRender.material.color = Color.red;
		hurtAnimationActive = true;
		fixedHealth = (int)(hp.fillAmount * healthScale);


	}


	void deathAnimation() {
		deathTimer += Time.deltaTime;

		if (Random.value > .7f) {
			GameObject exp = (GameObject)Instantiate (expPrefab);
			exp.transform.position = transform.position + new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), -3);
			GameObject.FindObjectOfType<Camera>().gameObject.transform.position = 
				GameObject.FindObjectOfType<Camera>().gameObject.transform.position 
					+ new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
		}

		PlanetHurt ();

		if (deathTimer > 4) {
						LiveController.LoseLife ();
		}
	}

}
