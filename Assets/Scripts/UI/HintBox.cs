using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintBox : MonoBehaviour {

	string[] option = new string[]{
		"Stack with your patner's P.A.D.L. to fire enhaced weapons!",
		"Hold down RB to charge your Laser!",
		"Your secondary shot will explode after a short period of time",
		"P.A.D.L's are indestructable, so don't worry about getting hit",
		"Sheilded enemies are not effected by lasers"
	};
	// Use this for initialization
	void Start () {
		int index = Random.Range (0, option.Length-1);
		gameObject.GetComponent<Text> ().text = option [index];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
