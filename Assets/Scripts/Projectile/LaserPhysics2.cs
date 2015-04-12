using UnityEngine;
using System.Collections;

public class LaserPhysics2 : MonoBehaviour {
	
	public GameObject player;
	public GameObject planet;
	public float playerNum;
	
	public float ray_length;
	public float ray_radius;
	public LayerMask collisionMask;
	public LayerMask collisionMask2;
	public LayerMask collisionMask3;
	
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;
	
	private float skin = .005f;

	Ray ray;
	RaycastHit hit;


	float angle;

	float x;
	float y;
	
	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider> ();
		planet = GameObject.FindGameObjectWithTag ("Planet");
		
		float deltaX = transform.position.x - planet.transform.position.x;
		float deltaY = transform.position.y - planet.transform.position.y;
		
		angle = Mathf.Atan2 (deltaY, deltaX);
		
		Debug.Log ("Angle: " + angle * Mathf.Rad2Deg);
		//player = GameObject.Find ("playerPrefab_1");

		x = transform.position.x;
		y = transform.position.y;
		
	}

	void Awake() {

	}
	
	// Update is called once per frame
	void Update () {
		size = transform.localScale;
		center = collider.center;
	}
	
	void LateUpdate() {


		//ray = new Ray(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		//float x = transform.position.x;


		ray = new Ray(new Vector2(x, y), new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		Debug.DrawRay(ray.origin, ray.direction);

		ray_length = size.x;

		if(ray_length == 0)
			ray_length = 1;

		ray_radius = (size.y+0.5f) / 2;

		if(ray_radius == 0)
			ray_radius = 0.1f;
		

		RaycastHit[] sphereHits = Physics.SphereCastAll(ray, ray_radius, ray_length, collisionMask);
		if (sphereHits.Length != 0) {
			for(int i = 0; i<sphereHits.Length; i++)
			{
				if(playerNum == 0){
					if (!sphereHits[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode1){
						sphereHits[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit1 = true;
						sphereHits[i].transform.gameObject.GetComponent<Health>().takeDamage(25);
					}
				}
					
				if(playerNum == 1){
					if (!sphereHits[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode2){
						sphereHits[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit2 = true;
						sphereHits[i].transform.gameObject.GetComponent<Health>().takeDamage(25);
					}
				}
			}
		}
		

		RaycastHit[] sphereHits2 = Physics.SphereCastAll(ray, ray_radius, ray_length, collisionMask2);
		if (sphereHits2.Length != 0) {
			for(int i = 0; i<sphereHits2.Length; i++) {
				if(sphereHits2[i].transform.gameObject.CompareTag("miner")) {
					if(playerNum == 0){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode1){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit1 = true;
							sphereHits2[i].transform.gameObject.GetComponent<miner>().hp--;
						}
					}
					
					if(playerNum == 1){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode2){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit2 = true;
							sphereHits2[i].transform.gameObject.GetComponent<miner>().hp--;
						}
					}
				}
				
				if(sphereHits2[i].transform.gameObject.CompareTag("starFighter")) {
					if(playerNum == 0){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode1){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit1 = true;
							sphereHits2[i].transform.gameObject.GetComponent<StarFighter>().Health--;
						}
					}
					
					if(playerNum == 1){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode2){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit2 = true;
							sphereHits2[i].transform.gameObject.GetComponent<StarFighter>().Health--;
						}
					}
				}
				
				if(sphereHits2[i].transform.gameObject.CompareTag("starTurret")) {
					if(playerNum == 0){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode1){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit1 = true;
							sphereHits2[i].transform.gameObject.GetComponent<StarTurret>().Health = 0;
						}
					}
					
					if(playerNum == 1){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode2){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit2 = true;
							sphereHits2[i].transform.gameObject.GetComponent<StarTurret>().Health = 0;
						}
					}
					
				}
				
				if(sphereHits2[i].transform.gameObject.CompareTag("starPlanet")) {
					if(playerNum == 0){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode1){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit1 = true;
							sphereHits2[i].transform.gameObject.GetComponent<AlienPlanet>().health.takeDamage(999);
						}
					}
					
					if(playerNum == 1){
						if (!sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().god_mode2){
							sphereHits2[i].transform.gameObject.GetComponent<EnemyLaserBehavior>().hit2 = true;
							sphereHits2[i].transform.gameObject.GetComponent<AlienPlanet>().health.takeDamage(999);
						}
					}
				}
			}
		}
		

//		if(Physics.SphereCast (ray, ray_radius, out hit, ray_length, collisionMask3)) {
//			//Debug.Log("hit with mine");
//			Destroy(hit.transform.gameObject);
//
//		}

		RaycastHit[] sphereHits3 = Physics.SphereCastAll(ray, ray_radius, ray_length, collisionMask3);
		if (sphereHits3.Length != 0) {
			for(int i = 0; i<sphereHits3.Length; i++) {
				sphereHits3[i].transform.gameObject.GetComponent<enemy_mine>().hit = true;
				
			}
		}
		
	}

}