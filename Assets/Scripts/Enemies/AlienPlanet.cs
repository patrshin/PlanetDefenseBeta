using UnityEngine;
using System.Collections;

public class AlienPlanet : MonoBehaviour {

	public bool activePlanet;
	public GameObject projectile;
	public GameObject explosionPrefab;
	public GameObject planet;

	private float shootingTimer;
	public float shootingCooldown;
	private float burstTimer = 0;
	public float burstCooldown;
	private int burstCount = 0;
	public int burstTotal;

	public Color defaultColor;
	public Color hitColor;

	public float colorChangeCooldown;
	private float colorChangeTimer;
	private bool colorChanged = false;
	
	public Health health;
	
	public float item_spawn_chance = 1f;
	public GameObject[] item_list;

	MeshRenderer planetRender;
	Material origMat;
	Color origColor;
	bool hurtAnimationActive = false;
	bool deathAnimationActive = false;

	// Use this for initialization
	void Start () {
		planetRender = GetComponent<MeshRenderer> ();
		planet = GameObject.FindGameObjectWithTag ("Planet");
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "Center") {
				child.GetComponent<MeshRenderer>().renderer.material.color = defaultColor;
			}
		}		health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		if (colorChanged) {
			colorChangeTimer += Time.deltaTime;
			if (colorChangeTimer > colorChangeCooldown) {
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "Center") {
						child.GetComponent<MeshRenderer>().renderer.material.color = defaultColor;
						colorChanged = false;
					}
				}
				colorChangeTimer = 0f;
			}
		}

		activePlanet = this.renderer.isVisible;
		if (activePlanet) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "Center2") {
					child.GetComponent<Transform>().Rotate(Vector3.forward * Time.deltaTime * 50f);
				}
			}
			if (shootingTimer > shootingCooldown && burstCount < burstTotal) {
				if (burstTimer > burstCooldown) {
					for (int x = 1; x <= 3; x++) {
						shootProjectile(20*x - 40);
					}
					burstTimer = 0;
					burstCount++;
				}
				else {
					burstTimer += Time.deltaTime;
				}
			}
			else if (shootingTimer < shootingCooldown) {
				shootingTimer += Time.deltaTime;
			}
			else if (burstCount == burstTotal) {
				shootingTimer = 0f;
				burstCount = 0;
			}
		}
		if (health.isDead()) {
			int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
			float spawn_chance = Random.value;
			if(spawn_chance <= item_spawn_chance){
				GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
				o.transform.position = transform.position;
			}
			for (int i=0; i < 5; i++) {
				GameObject exp = (GameObject)Instantiate (explosionPrefab);
				exp.transform.position = transform.position + new Vector3 (Random.Range (-.1f, .1f), Random.Range (-.1f, .1f), -3);
				exp.transform.localScale = exp.transform.localScale *= 3;
			}
			Destroy(this.gameObject);
		}
		if (hurtAnimationActive) {
			planetRender.material.color = Color.Lerp(planetRender.material.color, origColor, 0.1f);
		}
	}

	public void damageIndicator() {
		colorChanged = true;
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "Center") {
				child.GetComponent<MeshRenderer>().renderer.material.color = hitColor;
			}
		}
		PlanetHurt ();
		//GetComponentInChildren<MeshRenderer>().renderer.material.color = hitColor;
	}

	void shootProjectile(float angle_offset) {
		GameObject o = (GameObject) Instantiate (projectile);
		o.transform.position = transform.position;
		o.GetComponent<AlienProjectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(transform.position,
			    GameObject.FindGameObjectWithTag("Planet").transform.position
			     ) + 270 + angle_offset) * 
				new Vector3(0, 350, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void PlanetHurt() {
		if (hurtAnimationActive) {
			planetRender.material.color = origColor;
		}
		origColor = planetRender.material.color;
		planetRender.material.color = Color.red;
		hurtAnimationActive = true;
	}

}
