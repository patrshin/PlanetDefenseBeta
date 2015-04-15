using UnityEngine;
using System.Collections;

public class PlanetLaserBehavior : MonoBehaviour {

	//public GameObject explosionPrefab;
	public bool god_mode1;
	public bool hit1 = false;
	float gmode_delay1 = 5f;
	float gmode_time1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hit1) {
			god_mode1 = true;
			gmode_time1 = gmode_delay1;
			hit1 = false;
		}
		
		if (god_mode1) {
			gmode_time1 -= Time.deltaTime;
			
			if(gmode_time1 <= 0f) {
				god_mode1 = false;
			}
			
		}

	}
}