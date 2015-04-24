using UnityEngine;
using System.Collections;

public class enemy_lazer_physics : MonoBehaviour {

	public LayerMask collisionMask;
	public LayerMask collisionMask2;
	public GameObject boss;

	public float ray_length;
	public float ray_radius;

	private Vector3 size;

	Ray ray;
	RaycastHit hit;

	float angle;
	
	float x;
	float y;

	// Use this for initialization
	void Start () {

		x = transform.position.x;
		y = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		size = transform.localScale;
		angle = transform.eulerAngles.z * Mathf.Deg2Rad;

	}

	void LateUpdate() {

		ray = new Ray(new Vector2(x, y), new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		Debug.DrawRay(ray.origin, ray.direction);

		ray_length = size.x + 2;
		
		if(ray_length == 0)
			ray_length = 1;
		
		ray_radius = (size.y+0.5f) / 2;
		
		if(ray_radius == 0)
			ray_radius = 0.1f;

		if(Physics.SphereCast (ray, ray_radius, out hit, ray_length, collisionMask)) {
			if (!hit.transform.gameObject.GetComponent<PlanetLaserBehavior>().god_mode1) {
				hit.transform.gameObject.GetComponent<PlanetLaserBehavior>().hit1 = true;
				hit.transform.gameObject.GetComponent<PlanetSentinel>().hp.fillAmount -= 0.2f;
			}
		}

		if(Physics.SphereCast (ray, ray_radius, out hit, ray_length, collisionMask2)) {
			if(!hit.transform.gameObject.GetComponent<lvl5_jupiter>().god_mode)
				hit.transform.gameObject.GetComponent<lvl5_jupiter>().hit = true;			
		}

	}
}
