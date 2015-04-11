using UnityEngine;
using System.Collections;

public class level1_manager : MonoBehaviour {

	public GameObject sun;
	public GameObject end1;
	public GameObject end2;
	public GameObject boss_spawner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(!boss_spawner.GetComponent<level1_boss_spawner>().spawn) {
			sun.gameObject.transform.parent = null;
			end1.gameObject.transform.parent = null;
			end2.gameObject.transform.parent = null;

		}
	
	}
}
