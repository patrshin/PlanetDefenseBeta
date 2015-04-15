﻿using UnityEngine;
using System.Collections;

public class StarFighterSpawner : MonoBehaviour {
	
	public GameObject StarFighter;
	
	float time = 0f;
	public float spawnAverage;
	
	private float nextSpawn;
	
	public bool enableStarFighter;

	public int childCount = 0;

	private bool stop = false;
	
	// Use this for initialization
	void Start () {
		nextSpawn = Random.Range (spawnAverage-2.5f,spawnAverage);
	}
	
	// Update is called once per frame
	void Update () {
		if (enableStarFighter) {
			stop = false;
			GameObject[] shields = GameObject.FindGameObjectsWithTag("shieldedShip2");

			//if (time != 0) {
				foreach (GameObject shield in shields) {
					if (shield.renderer.isVisible) {
						stop = true;
					}
				}

				/*GameObject[] planets = GameObject.FindGameObjectsWithTag("starPlanet");

				foreach (GameObject planet in planets) {
					if (planet.renderer.isVisible) {
					Debug.Log(stop);

					stop = true;
					}
				}*/
			//}
			if (!stop) {
				time += Time.deltaTime;
			}

			if (time > nextSpawn) {
				if (childCount == 0) {
					GameObject o = (GameObject)Instantiate (StarFighter);
					float randomX = (Random.value>.5f?-1:1)*Random.Range (300f,350f);
					float randomY = (Random.value>.5f?-1:1)*Random.Range (300f,350f);
					o.transform.position = new Vector3 (randomX + transform.position.x,
					                                    randomY + transform.position.y, 0);

					o = (GameObject)Instantiate (StarFighter);
					o.transform.position = new Vector3 (randomX + 15f + transform.position.x,
					                                    randomY + transform.position.y, 0);
					o.GetComponent<StarFighter>().speedMoving -= 5;
					o = (GameObject)Instantiate (StarFighter);
					o.transform.position = new Vector3 (randomX + 30f + transform.position.x,
					                                    randomY + transform.position.y, 0);
					o.GetComponent<StarFighter>().speedMoving -= 10;
					childCount = 3;
				}
				time = 0f;
				nextSpawn = Random.Range (spawnAverage-2.5f,spawnAverage);
			}
		}
		//Set condition here if you want to turn on smallComet spawn
	}
}
