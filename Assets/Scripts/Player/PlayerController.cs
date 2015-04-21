﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using InControl;


public class PlayerController : MonoBehaviour {

	//public Boundary boundary;
	public float InitSpeed;
	private float speed;
	public float maxSpeed;

	private float updateTimer;
	private float updateCooldown = 0.02f;

	public int playerNum;
	public GameObject projectilePrefab;
	public GameObject projectilePrefab2;
	public GameObject railgunPrefab;
	public GameObject railgunChargePrefab;
	
	//projecitile
	public float projecitileCoolDown	=	0.5f;
	public float projecitileTimer		=	5f;
	private bool projectileButtonDownOnce = false;

	//mine
	public float mineCoolDown	=	5f;
	public float mineTimer		=	5f;

	//Railgun
	public float railgunChargeUp	=	2f;
	public float railgunTimer		=	0f;

	//projectile combined
	public float combinedCoolDown		=	5f;
	public float combinedTimer 			= 	5f;
	

	public float fuel_auto_fill_rate 	= 	30f;
	public float fuel_collect_rate		=	.2f;
	public float fuel_dec_rate 			= 	2f;
	public float start_fuel = 0f;

	public bool combined;

	public bool unlimitedFuel;

	private bool soundOff = false;


	bool refill;

	Image	p1_cd_bar;
	Image	p2_cd_bar;

	Image	p1_comb_cd;
	Image	p2_comb_cd;

	Image	p1_fuel_bar;
	Image 	p2_fuel_bar;

	Color fuel_bar_tmp;

	//for turret angle limitation
	private float turretRotationOffset				=	0f;

	//for planetPosition
	public Vector3 planetPos					=	Vector3.zero;

	//for sound effects
	public AudioSource sound_basic;
	public AudioSource sound_combined; 
	public AudioSource bounce;
	public AudioSource shoot_spread;

	//for PowerUps
	public float	power_timer = 0f;
	public float 	speed_modifier = 1f;
	public bool		super_shot = false;
	public float	rocket_boost = 1f;
	public float	fuel_modifier = 0f;
	Image	p1_pwr_icon;
	Image	p2_pwr_icon;
	Image	p1_pwr_bar;
	Image	p2_pwr_bar;

	// Use this for initialization
	void Start () {
		GameObject p1_cd_obj = GameObject.Find ("p1_cd");
		p1_cd_bar = p1_cd_obj.GetComponent<Image> ();

		GameObject p2_cd_obj = GameObject.Find ("p2_cd");
		p2_cd_bar = p2_cd_obj.GetComponent<Image> ();

//		GameObject p1_comb_obj = GameObject.Find ("p1_comb_cd");
//		p1_comb_cd = p1_comb_obj.GetComponent<Image> ();
//
//		GameObject p2_comb_obj = GameObject.Find ("p2_comb_cd");
//		p2_comb_cd = p2_comb_obj.GetComponent<Image> ();

		GameObject p1_fuel_obj = GameObject.Find ("p1_fuel_bar");
		p1_fuel_bar = p1_fuel_obj.GetComponent<Image> ();
		
		GameObject p2_fuel_obj = GameObject.Find ("p2_fuel_bar");
		p2_fuel_bar = p2_fuel_obj.GetComponent<Image> ();

		p1_fuel_bar.fillAmount = start_fuel;
		p2_fuel_bar.fillAmount = start_fuel;

		//PowerUp Cooldown UI Bar Initializaitons
		p1_pwr_icon = GameObject.Find ("p1_pwr_icon").GetComponent<Image> ();
		p2_pwr_icon = GameObject.Find ("p2_pwr_icon").GetComponent<Image> ();
		p1_pwr_bar = GameObject.Find ("p1_pwr_cd").GetComponent<Image> ();
		p2_pwr_bar = GameObject.Find ("p2_pwr_cd").GetComponent<Image> ();
		p1_pwr_bar.fillAmount = 0;
		p2_pwr_bar.fillAmount = 0;

		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
		sound_combined = aSources [1];
		bounce = aSources [2];
		shoot_spread = aSources [3];;

		Transform[] allChildren = GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if (child.name == "playerBoost") {
				child.GetComponent<ParticleSystem>().enableEmission = false;
			}
		}
		stopBoosting ();

		speed = InitSpeed;

		refill = true;


	}
		
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		//var inputDevice = (playerNum == 1) ? null : InputManager.Devices[0];

		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			//Debug.Log("no player");
			renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			//Destroy(this.gameObject);
		}
		else {
			updatePlayer(inputDevice);
		}

		if(projecitileTimer < 1){
			projecitileTimer += Time.deltaTime;
		}

		if(mineTimer < 5){
			mineTimer += Time.deltaTime;
		}

		if (combinedTimer < 5) {
			combinedTimer += Time.deltaTime;
		}


		p1_cd_bar.fillAmount += (Time.deltaTime/(projecitileCoolDown*1.5f));
		p2_cd_bar.fillAmount += (Time.deltaTime/(projecitileCoolDown*1.5f));

//		p1_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));
//		p2_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));


		if(playerNum == 0)
			p1_pwr_bar.fillAmount = power_timer / 5f;
		if (playerNum == 1)
			p2_pwr_bar.fillAmount = power_timer / 5f;

		if (power_timer > 0)
			power_timer -= Time.deltaTime;
		else
			applyPower ("ResetPower");

		fuel_bar_tmp = Color.Lerp(Color.red, Color.green,p1_fuel_bar.fillAmount);
		fuel_bar_tmp.a = 0.5f;
		p1_fuel_bar.color = fuel_bar_tmp;
		
		fuel_bar_tmp = Color.Lerp(Color.red, Color.green,p2_fuel_bar.fillAmount);
		fuel_bar_tmp.a = 0.5f;
		p2_fuel_bar.color = fuel_bar_tmp;

	}

	GameObject shootProjectile(GameObject projectileType, float angle_offset, float speed){
		GameObject o = (GameObject)Instantiate (projectileType);		
		o.transform.position = transform.position;
		if (o.GetComponent<Projectile> ()) {
			o.GetComponent<Projectile> ().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector (
				GameObject.FindGameObjectWithTag ("Planet").transform.position, transform.position
			) + 270 + angle_offset) * 
				new Vector3 (0, speed, 0);
		}
		return o;
	}

	void shootBasic() {
		shootProjectile(projectilePrefab,0,1000);
		sound_basic.Play ();
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void shootSecondary() {
		GameObject o = shootProjectile (projectilePrefab2, 0, 450);
		//This Lines specific to Mine's 
		//o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z-90f);
		shoot_spread.Play ();
	}

	void shootRailgun() {
		GameObject o = shootProjectile (railgunPrefab, 0, 1);
		o.transform.position = transform.position;
		o.GetComponent<LaserPhysics2> ().playerNum = playerNum;
		o.GetComponent<railgun> ().angle = this.transform.localPosition;
		o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z);
		//Debug.Log (transform.localPosition);
		//Debug.Log (planetPos);

		if((combined && combinedTimer >= combinedCoolDown)){
			combinedTimer = 0f;
//			if(playerNum == 0){
//				p1_comb_cd.fillAmount = 0;
//			}
//			
//			if(playerNum == 1){
//				p2_comb_cd.fillAmount = 0;
//			}
			o.GetComponent<railgun> ().super = true;
		}
		sound_basic.Play ();
	}

	void shootCombined(){
		for (int x = 1; x <= 4; x++) {
			shootProjectile(projectilePrefab,10*x - 25,1000);
		}
		sound_combined.Play ();
	}

	void MovePlanet (float sensitivity) {
		var planet = transform.parent.GetComponent<Transform>();
		Vector3 movement = planet.transform.position - transform.position; 
		if (updateTimer > updateCooldown) {
			if (speed < maxSpeed) {
				speed += 0.1f;
			}
			updateTimer = 0;
		}
		else
			updateTimer += Time.deltaTime;
		//Debug.Log (movement*sensitivity*0.02f*speed*rocket_boost);
		planet.transform.position = planet.transform.position + movement*sensitivity*0.02f*speed*rocket_boost;
		transform.parent.GetComponent<Transform>().transform.position = planet.transform.position;

//		planetPos = transform.parent.GetComponent<Transform> ().transform.position;

		if (unlimitedFuel) {
			return;
		}
		if (playerNum == 0) {
			p1_fuel_bar.fillAmount -= Time.deltaTime/(fuel_dec_rate + fuel_modifier);
		}
		
		if (playerNum == 1) {
			p2_fuel_bar.fillAmount -= Time.deltaTime/(fuel_dec_rate + fuel_modifier);
		}

	}


	void updatePlayer( InputDevice inputDevice){
		
		//Shooting Controls
		if (inputDevice.RightBumper) {
			railgunTimer += Time.deltaTime;
			if (railgunTimer < railgunChargeUp) {
				GetComponent<Light>().intensity += Time.deltaTime;
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "ChargedUp") {
						child.GetComponent<Image>().fillAmount = railgunTimer/railgunChargeUp;
					}
				}
			}
			if (railgunTimer > railgunChargeUp) {
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "ChargedUp") {
						child.GetComponent<Image>().fillAmount = 1;
					}
				}
			}
			if (!projectileButtonDownOnce) {				
				if(canSuperShot() ){
					combinedTimer = 0f;
					projecitileTimer = 0f;
					shootCombined();
					
//					if(playerNum == 0){
//						p1_comb_cd.fillAmount = 0;
//					}
//					
//					if(playerNum == 1){
//						p2_comb_cd.fillAmount = 0;
//					}
				}

				else if(projecitileTimer >= projecitileCoolDown){
					projecitileTimer = 0f;
					if(super_shot)
						shootCombined();
					else
						shootBasic();
					
					if(playerNum == 0){
						p1_cd_bar.fillAmount = 0;
					}
					
					if(playerNum == 1){
						p2_cd_bar.fillAmount = 0;
					}
				}
			}
			projectileButtonDownOnce = true;
		}
		else {
			if (railgunTimer > railgunChargeUp) {
				//Debug.Log(railgunTimer);
				shootRailgun ();
			}
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "ChargedUp") {
					child.GetComponent<Image>().fillAmount = 0;
				}
			}
			GetComponent<Light>().intensity = 0;
			railgunTimer = 0;
			projectileButtonDownOnce = false;
		}


		if (inputDevice.LeftBumper) {
			if(mineTimer >= mineCoolDown){
				mineTimer = 0f;
				shootSecondary();
				
				if(playerNum == 0){
					p1_cd_bar.fillAmount = 0;
				}
				
				if(playerNum == 1){
					p2_cd_bar.fillAmount = 0;
				}
			}
		}

		//Propulsion action
		if (inputDevice.Action2) {
			if((playerNum == 0 && p1_fuel_bar.fillAmount > 0.05) || (playerNum == 1 && p2_fuel_bar.fillAmount > 0.05)){
				MovePlanet(inputDevice.Action2); 
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "playerBoost") {
						if (!child.GetComponent<AudioSource>().isPlaying)
							child.GetComponent<AudioSource>().Play();
						child.GetComponent<ParticleSystem>().enableEmission = true;
					}
				}
				startBoosting();
			}
			else {
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				speed = InitSpeed;
				foreach (Transform child in allChildren) {
					if (child.name == "playerBoost") {
						child.GetComponent<AudioSource>().Stop ();
						child.GetComponent<ParticleSystem>().enableEmission = false;
					}
				}
				stopBoosting();

			}
		}
		else {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			speed = InitSpeed;
			foreach (Transform child in allChildren) {
				if (child.name == "playerBoost") {
					child.GetComponent<AudioSource>().Stop ();
					child.GetComponent<ParticleSystem>().enableEmission = false;
				}
			}

			refill = true;
			stopBoosting();
			if(playerNum == 0){
				p1_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
			}
			
			if(playerNum == 1){
				p2_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
			}

		}
		planetPos = transform.parent.GetComponent<Transform> ().transform.position;


		Quaternion rotate = this.transform.rotation;
		var max = Mathf.Abs (inputDevice.LeftStickY);
		if (Mathf.Abs (inputDevice.LeftStickX) > Mathf.Abs (inputDevice.LeftStickY )) {
			max = Mathf.Abs (inputDevice.LeftStickX);
		}

		//rotating player
		if (Mathf.Abs (inputDevice.LeftStickX)> 0.2 || Mathf.Abs (inputDevice.LeftStickY )> 0.2) {
			
			Vector3 ThumbPos = new Vector3(inputDevice.LeftStickX, inputDevice.LeftStickY, 0);
			Vector3 playerPos = this.transform.position - planetPos;
			
			var angle = Vector3.Angle (playerPos, ThumbPos);
			var cross = Vector3.Cross (playerPos, ThumbPos);
			if (cross.z < 0) 
				angle = -angle;
			if (Mathf.Abs (angle) > 2) {
				if (angle >= 0) {
					transform.RotateAround(planetPos, Vector3.forward, 150 * max * Time.deltaTime * speed_modifier);
				}
				else if (angle < 0){
					transform.RotateAround(planetPos, Vector3.forward, -150 * max * Time.deltaTime * speed_modifier);
				}
			}
			
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = true;
			//Debug.Log ("Combined");
		}

		if (c.gameObject.CompareTag ("p1_fuel") && playerNum == 0) {
			Destroy(c.gameObject);
			if(playerNum == 0)
				p1_fuel_bar.fillAmount += fuel_collect_rate;
		}
		
		if (c.gameObject.CompareTag ("p2_fuel") && playerNum == 1) {
			Destroy(c.gameObject);
			if(playerNum == 1)
				p2_fuel_bar.fillAmount += fuel_collect_rate;
		}
		if (c.gameObject.CompareTag ("PowerUp")) {
			applyPower ("ResetPower");
			applyPower (c.gameObject.name);
			Destroy(c.gameObject);
		}

		if (c.gameObject.CompareTag("Asteroid_P1")) {
			bounce.Play ();

		}
	}

	void applyPower(string powerType){
		//Change Corressponding variable (code)
		//Change Power Icon (UI)
		//Change Cooldown Timer (code)
		//Change Cooldown Bar (UI)
		fuel_bar_tmp = Color.white;
		fuel_bar_tmp.a = 1f;
		if (powerType.Contains ("rocketBoost")) {
			power_timer = 5f;
			rocket_boost = 1f;
			fuel_modifier  = 100f;

			if(playerNum == 0){
				p1_pwr_icon.color = fuel_bar_tmp;
				p1_pwr_icon.sprite = Resources.Load("booster", typeof(Sprite)) as Sprite;
				p1_fuel_bar.fillAmount = 1;
			} else{
				p2_pwr_icon.color = fuel_bar_tmp;
				p2_pwr_icon.sprite = Resources.Load("booster", typeof(Sprite)) as Sprite;
				p2_fuel_bar.fillAmount = 1;
			}
				

		} else if (powerType.Contains ("superShot")) {
			power_timer = 5f;
			super_shot = true;

			if(playerNum == 0){
				p1_pwr_icon.color = fuel_bar_tmp;
				p1_pwr_icon.sprite = Resources.Load("superShot", typeof(Sprite)) as Sprite;
			}
			else{
				p2_pwr_icon.color = fuel_bar_tmp;
				p2_pwr_icon.sprite = Resources.Load("superShot", typeof(Sprite)) as Sprite;
			}

		} else if (powerType.Contains ("speedUp")) {
			power_timer = 5f;
			speed_modifier = 2f;

			if(playerNum == 0){
				p1_pwr_icon.color = fuel_bar_tmp;
				p1_pwr_icon.sprite = Resources.Load("speedUp", typeof(Sprite)) as Sprite;
			}
			else{
				p2_pwr_icon.color = fuel_bar_tmp;
				p2_pwr_icon.sprite = Resources.Load("speedUp", typeof(Sprite)) as Sprite;
			}

		} else {
			fuel_bar_tmp.a = 0f;
			if(playerNum == 0){
				p1_pwr_icon.color = fuel_bar_tmp;
				p1_pwr_icon.sprite = null;
			}
			else{
				p2_pwr_icon.color = fuel_bar_tmp;
				p2_pwr_icon.sprite = null;
			}

			rocket_boost = 1f;
			fuel_modifier = 0f;
			speed_modifier = 1f;
			super_shot = false;
			power_timer = 0f;
		}
	}

	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = false;
//			Debug.Log ("Separated");
		}
	}

	public bool canSuperShot() {
		return (combined && combinedTimer >= combinedCoolDown);
	}
	//returns percentage of superShot fill
	public float getSuperShotStatus() {
		return combinedTimer < combinedCoolDown ? 
			   combinedTimer / combinedCoolDown
				: 1f;
	}

	
	void startBoosting(){
		string name = "p" + (playerNum+1) + "_fuel_expend";
		GameObject id = GameObject.Find (name);
		if (id == null)
			return;
		
		if (id.GetComponent<FuelIndication> ()) {
			id.GetComponent<FuelIndication> ().startBoosting();
		}
	}
	
	void stopBoosting(){
		string name = "p" + (playerNum+1) + "_fuel_expend";
		GameObject id = GameObject.Find (name);
		if (id == null) {
			Debug.Log ("Failed to find GameObject:" + name);
			return;
		}

		if (id.GetComponent<FuelIndication> ()) {
			id.GetComponent<FuelIndication> ().stopBoosting();
		}
	}


}
