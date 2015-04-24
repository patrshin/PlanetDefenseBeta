using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSentinel : MonoBehaviour {


	public GameObject expPrefab;

	public Image hp;
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

//		Debug.Log (hpred.fillAmount);
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

		if (Input.GetKey (KeyCode.Alpha6)) {
			Application.LoadLevel ("Title");
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

		if (Input.GetKey (KeyCode.Home)) {
			hp.fillAmount = 0;
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




		if (everyOtherFrame) {
			shakeCamera();
		} 
		everyOtherFrame = !everyOtherFrame;



		if (Random.value > .7f) {
			makeExplosion(5f, 1f);


		}

		PlanetHurt ();

		if (deathTimer > 4 && deathTimer < 4.5) {
			if (Random.value > .5f)
				makeExplosion(.1f, 30f);
			GetComponent<MeshRenderer>().enabled = false;
		}

		if (deathTimer > 6) {
			LiveController.LoseLife ();
		}
						

	}

	void makeExplosion(float range, float scale) {
		GameObject exp = (GameObject)Instantiate (expPrefab);
		exp.transform.position = transform.position + new Vector3 (Random.Range (-range, range), Random.Range (-range, range), -3);
		exp.transform.localScale = exp.transform.localScale *= scale;
	}

	bool everyOtherFrame = false;
	bool startedShaking = false;
	bool isCameraChild = false;
	bool destroyedManager = false;
	Vector3 baseLocation;
	float origZ;
	void shakeCamera() {


		if (!startedShaking) {


			// violently destroy anything taking control of the camera
			if (GameObject.FindObjectOfType<level1_manager>()) {
				Destroy (GameObject.FindObjectOfType<level1_manager>()); destroyedManager = true;
			}
			if (GameObject.FindObjectOfType<level_manager>()) {
				Destroy (GameObject.FindObjectOfType<level_manager>()); destroyedManager = true;
			}
			if (GameObject.FindObjectOfType<level3_camera>()) {
				Destroy (GameObject.FindObjectOfType<level3_camera>()); destroyedManager = true;
			}





			isCameraChild = (GameObject.FindObjectOfType<Camera>().transform.parent != null &&
			                 GameObject.FindObjectOfType<Camera>().transform.parent.gameObject == gameObject);

			if (isCameraChild) { 
				baseLocation = GameObject.FindObjectOfType<Camera>().gameObject.transform.localPosition;
				origZ = GameObject.FindObjectOfType<Camera>().gameObject.transform.localPosition.z;
			}else {
				baseLocation = GameObject.FindObjectOfType<Camera>().gameObject.transform.position;
				origZ = GameObject.FindObjectOfType<Camera>().gameObject.transform.position.z;
			}
			startedShaking = true;
//			Debug.Log (isCameraChild + " - is camera child");



		}

		if (destroyedManager) {
			baseLocation = transform.position;
			isCameraChild = false;

		}



		Vector3 additive = new Vector3 (
			Random.Range (-5f / transform.lossyScale.x, 5f / transform.lossyScale.x), 
			Random.Range (-5f / transform.lossyScale.y, 5f / transform.lossyScale.y), 0
			);
		baseLocation.z = origZ;

		if (isCameraChild) {
			GameObject.FindObjectOfType<Camera> ().gameObject.transform.localPosition = 
				baseLocation + additive;
		} else {
			//Debug.Log (additive);
			GameObject.FindObjectOfType<Camera> ().gameObject.transform.position = 
				baseLocation + additive;

		}

	}
	

}
