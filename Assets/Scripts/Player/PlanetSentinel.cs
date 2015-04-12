using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSentinel : MonoBehaviour {

	Image hp;

	Image hpred;


	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();

		
		hpred = GameObject.Find ("HPRed").GetComponent <Image>();
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

		if (hp.fillAmount <= 0) {
			LiveController.LoseLife();
		}
	}
	


}
