using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tilizer : MonoBehaviour {

	public float Depth;
	public int TileCoverage;
	public float ParalaxDelay;
	public Vector3 offset;

	public GameObject instance;
	GameObject        planetRef;
	Vector3           imageSize;
	List<GameObject>  tiles = new List<GameObject>();



	// Use this for initialization
	void Start () {

		if (TileCoverage < 1) {
			Destroy (this.gameObject);
			return;
		}

		planetRef = GameObject.FindGameObjectWithTag("Planet");
		GameObject test = (GameObject) Instantiate (instance);
		imageSize = test.transform.lossyScale;
		Debug.Log (imageSize);
		tiles.Add(test);

		for(int i = 1; i < TileCoverage*TileCoverage; ++i) {
			tiles.Add ((GameObject) Instantiate (instance));
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		// granulate the position to the image bounds, assuming that the instance
		// prefab has a child image whose bounds match the scale of the instance object

		float granualizedX = (
			(Mathf.Round((planetRef.transform.position.x) / imageSize.x))     // base granualization, putting the base image where the planet is 
		  + ((offset.x + planetRef.transform.position.x*ParalaxDelay) % 1f)   // offset for paralax option + natural offset
		  - TileCoverage/2                                                    // centers the granualization around the planet
		  
		);

		float granualizedY = (
			(Mathf.Round((planetRef.transform.position.y + offset.y) / imageSize.y)) 
		  + ((offset.y + planetRef.transform.position.y*ParalaxDelay) % 1f)
		  - TileCoverage/2
		);

		for(int i = 0; i < tiles.Count; ++i) {
			tiles[i].transform.position = new Vector3( 
				(granualizedX + (i % TileCoverage))* imageSize.x,
			    (granualizedY + (i / TileCoverage))* imageSize.y,
			                                 Depth);
			//Debug.Log (planetRef.transform.position + " -> " + transform.position);
		}
	}
}
