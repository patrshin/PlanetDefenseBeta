using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public string[] CreditText;
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
