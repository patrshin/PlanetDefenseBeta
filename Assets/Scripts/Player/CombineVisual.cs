using UnityEngine;
using System.Collections;

public class CombineVisual : MonoBehaviour {
	GameObject p1Ref;
	GameObject p2Ref;
	GameObject planetRef;
	ParticleSystem part;

	float minimumDist = 3f;
	float maxSizeParticle = 13f;

	public GameObject barFill;
	GameObject p1HInstance, p2HInstance;

	float p1Rot, p2Rot;
	Vector2 pAlpha = new Vector2(0f, 0f);

	// Use this for initialization
	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject o in players) {
			if (o.GetComponent<PlayerController>()) {
			    if (o.GetComponent<PlayerController>().playerNum == 0)
					p1Ref = o;
				else if (o.GetComponent<PlayerController>().playerNum ==1)
					p2Ref = o;
			}
		}
		planetRef = GameObject.FindGameObjectWithTag("Planet");
		p1HInstance = (GameObject) Instantiate (barFill);
		p2HInstance = (GameObject) Instantiate (barFill);
		part = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		float dist = Vector3.Distance (p1Ref.transform.position, p2Ref.transform.position);
		if (dist < minimumDist) {
			part.startSize = (minimumDist - dist)/minimumDist * maxSizeParticle; 
		} else {
			part.startSize = 0;
		}


		float leng = (planetRef.transform.position - p1Ref.transform.position).magnitude;



	
		float rot = Util.getMeanAngle(p1Rot, p2Rot);
		float baseAngle = 
			(p1Rot > p2Rot) ? 
			 p2Rot : p1Rot;
		transform.position = new Vector3(
			Mathf.Cos (Mathf.Deg2Rad * (rot))*leng + planetRef.transform.position.x,
			Mathf.Sin (Mathf.Deg2Rad * (rot))*leng + planetRef.transform.position.y,
			transform.position.z
		);


		setupBars();
	}



	void setupBars() {
		PlayerController p1Con = p1Ref.GetComponent<PlayerController>();
		PlayerController p2Con = p2Ref.GetComponent<PlayerController>();
	
		Vector2 targetAlpha = new Vector2(0f, 0f);


		if (p1Con.getSuperShotStatus() < .9f || p1Con.combined)
			targetAlpha.x = 1f;
		if (p2Con.getSuperShotStatus() < .9f || p2Con.combined)
			targetAlpha.y = 1f;

		pAlpha = Vector2.Lerp(pAlpha, targetAlpha, .1f);


		Color temp = p1HInstance.GetComponent<SpriteRenderer>().color;
		temp.a = pAlpha.x;
		p1HInstance.GetComponent<SpriteRenderer>().color = temp;
		temp = p1HInstance.transform.GetChild (0).GetComponent<SpriteRenderer>().color;
		temp.a = pAlpha.x;
		p1HInstance.transform.GetChild (0).GetComponent<SpriteRenderer>().color = temp;
		
		temp = p2HInstance.GetComponent<SpriteRenderer>().color;
		temp.a = pAlpha.y;
		p2HInstance.GetComponent<SpriteRenderer>().color = temp;
		temp = p2HInstance.transform.GetChild (0).GetComponent<SpriteRenderer>().color;
		temp.a = pAlpha.y;
		p2HInstance.transform.GetChild (0).GetComponent<SpriteRenderer>().color = temp;




		p1HInstance.transform.GetChild(0).transform.localScale = 
			new Vector3(p1Con.getSuperShotStatus(), 1f, 1f);



		p2HInstance.transform.GetChild(0).transform.localScale = 
			new Vector3(p2Con.getSuperShotStatus(), 1f, 1f);
		



	}

	void Update() {
		p1HInstance.transform.position = new Vector3(
			Mathf.Cos (Mathf.Deg2Rad * (p1Rot-5))*10f + planetRef.transform.position.x,
			Mathf.Sin (Mathf.Deg2Rad * (p1Rot-5))*10f + planetRef.transform.position.y,
			transform.position.z
		);


		p2HInstance.transform.position = new Vector3(
			Mathf.Cos (Mathf.Deg2Rad * (p2Rot+5))*10f + planetRef.transform.position.x,
			Mathf.Sin (Mathf.Deg2Rad * (p2Rot+5))*10f + planetRef.transform.position.y,
			transform.position.z
			);

		p1Rot = Util.getAngleVector(planetRef.transform.position , p1Ref.transform.position);
		p2Rot = Util.getAngleVector(planetRef.transform.position , p2Ref.transform.position);
		
		
		if (Mathf.Abs (p1Rot - p2Rot) > 180) {
			p1Rot -=360;
		}
	}
}
