using UnityEngine;
using System.Collections;

public class Cinematic1 : MonoBehaviour {
	/*
	enum Phase {
		FirstZoom,
		DangerPan,
		Repan,
		PaddleDeployment

	};

	public GameObject asteroid;
	float initialZoomAmount = 10f;
	Phase phase = Phase.FirstZoom;
	float time = 0f;
	int numAsteroids = 40;
	float minDistance = 40f;
	float distanceRange = 40f;
	float initialVelAsteroid = 1f;
	GameObject planet;
	GameObject[] players;

	// Use this for initialization
	void Start () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
		players = GameObject.FindGameObjectsWithTag ("Player");


		GameObject inst;
		for(int i = 0; i < numAsteroids; ++i) {
			inst = (GameObject) Instantiate (asteroid);
			inst.transform.position = 
				planet.transform.position + 
					(Quaternion.Euler(0, 0, Random.Range (0, 360)) * new Vector3(
					minDistance + Random.Range (0f, distanceRange),0, 0));

			inst.rigidbody.mass = Random.Range(25, 100);
			inst.GetComponent<AsteroidPhysics>().initialVelocity = 
				Quaternion.Euler(
					new Vector3(0, 0, 180 + Util.getAngleVector(planet.transform.position, inst.transform.position))) 
			   * new Vector3(2, 0, 0); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;


		switch(phase) {
		case Phase.FirstZoom:
			Camera.main.orthographicSize = initialZoomAmount;
			if (time > 4f) {
				time = 0f;
				phase = Phase.DangerPan;
			}
			break;


		case Phase.DangerPan:

			Camera.main.orthographicSize = Camera.main.orthographicSize += Time.deltaTime*10;
			if (time > 2f) {
				time = 0f;

				phase = Phase.Repan;
			}
			break;


		case Phase.Repan:

			if (time > 3f) {
				Camera.main.orthographicSize = Camera.main.orthographicSize -= Time.deltaTime*14;
			}
			if (time > 4.5f) {
				GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid_P1");
				foreach(GameObject o in asteroids) {
					if (Vector3.Distance(o.transform.position, planet.transform.position) <= minDistance) {
						o.rigidbody.Sleep();
					}
				}

				phase = Phase.PaddleDeployment;
				foreach(GameObject o in players) {
					o.GetComponent<intro_player>().overrideStart = true;
				}
			}
			break;
		}
	}
	*/
}
