using UnityEngine;
using System.Collections;

public class EnemyLaserBehavior : MonoBehaviour {

	public bool god_mode1;
	public bool hit1 = false;
	float gmode_delay1 = 2f;
	float gmode_time1;

	public bool god_mode2;
	public bool hit2 = false;
	float gmode_delay2 = 2f;
	float gmode_time2;

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

		if (hit2) {
			god_mode2 = true;
			gmode_time2 = gmode_delay1;
			hit2 = false;
		}
		
		if (god_mode2) {
			gmode_time2 -= Time.deltaTime;
			
			if(gmode_time2 <= 0f) {
				god_mode1 = false;
			}
			
		}
	}
}
