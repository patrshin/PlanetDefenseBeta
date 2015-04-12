using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSentinel : MonoBehaviour {

	Image hp;
	const int healthScale = 100;
	int fixedHealth = healthScale;
	MeshRenderer earthRender;
	Material origMat;
	Color origColor;
	bool hurtAnimationActive = false;


	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();
		earthRender = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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
			LiveController.LoseLife();
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
	


}
