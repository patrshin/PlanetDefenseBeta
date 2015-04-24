using UnityEngine;
using System.Collections;
using InControl;

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
		"Thank you pilots:",
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
		"MUSIC",
		"",
		"Stage 1: Johnathan Corkery",
		"",
		"Stage 2-3: Gustav Holst - ",
		"",
		"Planets - USAF Heritage of America Band*", 
		"(https://archive.org/details/Holst-ThePlanets)",
		"*This image or file is a work of a U.S. Air ",
		"Force Airman or employee, taken or made as part of",
		"that person's official duties. As a work of the U.S. ",
		"federal government, the image or file is ",
		"in the public domain.",
		"",
		"",
		"Stage 4: Gustav Holst:",
		"St. Paul’s Suite - ",
		"Skidmore College Orchestra",
		"(https://musopen.org/music/2233/gustav-holst",
		" /st-pauls-suite-op-29-no-2/)",
		"https://creativecommons.org/publicdomain/mark/1.0/",
		"",
		"",
		"Stage 5: Antonin Dvorak: ",
		"New World Movement - ",
		"Szell-Cleveland Orchestra",
		"(https://archive.org/details/DvorakSymphonyNo.",
		" 9newWorld-Szell-cleveland)",
		"http://creativecommons.org/licenses/by-nc-sa/3.0/",
		"",
		"",
		"Credits: Wolfgang Amadeus Mozart:",
		"Ave Verum Corpus, K.618",
		"Orchestra Gli Armonici",
		"(https://musopen.org/music/792/",
		"wolfgang-amadeus-mozart/ave-verum-corpus-k-618/)",
		"https://creativecommons.org/publicdomain/mark/1.0/",
		"",
		"",
		"SOUND EFFECTS",
		"",
		"",
		"http://soundbible.com/",
		"http://www.freesfx.co.uk",
		"",
		"",
		"",
		"",
		"ART",
		"",
		"",
		"Earth Texture/Material: Second Mouse Studios", 
		"Sun Texture: NASA",
		"Miner & Boss Sprites: OpenGameArt.org",
		"",
		"",
		"",
		"",
		"",
		"",
		"~EECS 494, Games Showcase~",
		"[2015, April 24th]",
		"",
		""

	

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
