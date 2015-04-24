using UnityEngine;
using System.Collections;

public class boss_lazoring1 : MonoBehaviour {

	public bool fire;
	public GameObject lazor;
	public Vector3 angle;
	public float z_rot;
	public float rotate_speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(fire) {
			GameObject o = (GameObject)Instantiate (lazor);
			o.transform.position = transform.position;
			o.transform.rotation = Quaternion.Euler(0, 0, z_rot);
			o.GetComponent<enemy_laser1> ().angle = angle;
			o.GetComponent<enemy_laser1> ().rotate_speed = rotate_speed;
			fire = false;
		}
	
	}
}
