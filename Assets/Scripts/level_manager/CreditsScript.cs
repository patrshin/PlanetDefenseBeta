using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	string[] CreditText = {
		"Congratulations.",
		"",
		"",
		"",
		"",
		"Thanks to the both of you,",
		"The Earth and its inhabitants",
		"have found a new place",
		"to call home.",
		"",
		"",
		"Thank you pilots;",
		"the world is in your debt.",
		"",
		"",
		"",
		"",
		"",
		"",
		"",
		"STAFF",
		"",
		"",
		"",
		"John Lee",
		"Johnathan Corkery",
		"Patrick Shin",
		"Zachary Nowicki",
		"",
		"",
		"",
		"",
		"",
		


	};
	TextMesh text;

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh> ();
		foreach (string str in CreditText) {
			text.text += str + '\n';
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(0, .1f, 0));
	}
}
