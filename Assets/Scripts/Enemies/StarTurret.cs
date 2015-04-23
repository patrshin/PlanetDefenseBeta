using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarTurret : MonoBehaviour {
	
	public int Health;
	
	public GameObject projectile;
	public GameObject explosionPrefab;
	public GameObject itemProjectile;
	
	public float speedRotate;
	public float speedMoving;
	
	private float planetAngle;
	private Vector3 planetPos;
	private float planetDistance;
	private Vector3 relativeDistance;

	//Timer for shifting around
	public float shiftAvg;
	private float shiftTimer;
	private float timeTilShift;

	public float movementAvg;
	private float movementTimer;
	private float timeTilStop;
	private float direction;

	public float item_spawn_chance = 1f;
	public GameObject[] item_list;

	private float shootTime;
	public float minShootTime;
	public float maxShootTime;

	Image hp;

	public float timeActive;
	private float activeTimer = 0;

	public GameObject mines;
	
	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();

		shootTime = Random.Range (minShootTime, maxShootTime);
		planetPos = GameObject.FindGameObjectWithTag ("Planet").transform.position;
		planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
		planetDistance = Vector3.Distance (planetPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		//Debug.Log (planetAngle);
		timeTilShift = shiftAvg;
		timeTilStop = movementAvg;
		if (Random.Range(0f,1f) > 0.5f) {
			direction = 1;
		}
		else
			direction = -1;
	}
	
	// Update is called once per frame
	void Update () {
		planetPos = GameObject.FindGameObjectWithTag ("Planet").transform.position;
		relativeDistance = transform.position - planetPos;
		//If they don't reach the planet move towards it
	}
	
	void LateUpdate () {
		shootTime -= Time.deltaTime;
		if (activeTimer > timeActive) {
			GameObject o = (GameObject) Instantiate (mines);
			o.GetComponent<enemy_mine>().speed = 0;
			Vector3 mine_pos = transform.position;
			o.transform.position = mine_pos;
			Destroy(this.gameObject);
			return;
			//transform.position = Vector3.MoveTowards(transform.position,transform.position-planetPos,Time.deltaTime*speedMoving);
			//planetAngle = Util.getAngleVector (transform.position, planetPos) + 90;
			//transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		}
		else {
			activeTimer += Time.deltaTime;
			Debug.Log (activeTimer);

			//if planet is far away move towards it
			if (planetDistance > 20) {
				transform.position = Vector3.MoveTowards(transform.position,planetPos,Time.deltaTime*speedMoving);
				planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
				transform.eulerAngles = new Vector3 (0, 0, planetAngle);
				planetDistance = Vector3.Distance (planetPos, this.transform.position);

			}
			else {
				transform.position = planetPos + relativeDistance;
				if (planetDistance < 15) {
					transform.position = Vector3.MoveTowards(transform.position,transform.position-planetPos,Time.deltaTime*speedMoving);
				}
				relativeDistance = transform.position - planetPos;
				planetAngle = Util.getAngleVector (transform.position, planetPos) + 270f ;
				transform.eulerAngles = new Vector3 (0, 0, planetAngle);
				Maneuvering();
			}
			if (Health < 1) {
				int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
				float spawn_chance = Random.value;
				if(spawn_chance <= item_spawn_chance){
					GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
					o.transform.position = transform.position;
				}
				if (GameObject.Find("StarFighterSpawnerPrefab"))
					GameObject.Find("StarFighterSpawnerPrefab").GetComponent<StarFighterSpawner>().childCount--;
				Destroy(this.gameObject);
			}
		}
		
	}

	void Maneuvering() {
		if (shiftTimer > timeTilShift) {
			//everytime you shit recalculate distance
			planetDistance = Vector3.Distance (planetPos, this.transform.position);
			Movement();
		}
		else 
			shiftTimer += Time.deltaTime;

	}

	void Movement() {
		if (movementTimer < timeTilStop) {
			transform.RotateAround(planetPos, Vector3.forward, direction*speedRotate);
			movementTimer += Time.deltaTime;
			//Debug.Log (movementTimer);
		}
		else {
			if (shootTime < 0) {
				shootProjectile();
				shootTime = Random.Range (minShootTime, maxShootTime);
			}

			movementTimer = 0f;
			shiftTimer = 0f;
			timeTilStop = Random.Range (movementAvg - 0.1f, movementAvg + 0.1f);
			timeTilShift = Random.Range (shiftAvg - 2.0f, shiftAvg + 1.0f);
			if (Random.Range(0,1) > 0.5) {
				direction = 1;
			}
			else
				direction = -1;
		}

	}
	
	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (projectile);
		o.transform.position = transform.position;
		o.GetComponent<AlienProjectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector( transform.position,
			                                            GameObject.FindGameObjectWithTag("Planet").transform.position
			                                            )  + 270) * 
				new Vector3(0, 500, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Planet") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			hp.fillAmount -= 0.15f;
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "Player") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
		
	}

}