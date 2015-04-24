using UnityEngine;
using System.Collections;

public class lvl5_missiles : MonoBehaviour {

	GameObject boss;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss2");
	}
	
	// Update is called once per frame
	void Update () {
		if(boss.GetComponent<lvl5_boss>().jupiter_dead)
			Destroy(this.gameObject);
	}
}
