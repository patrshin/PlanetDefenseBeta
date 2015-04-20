using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelIndication : MonoBehaviour {

	Image self;
	public float rate = 0.1f;
	// Use this for initialization
	void Start () {
		self = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (self.color.a > 0) {
			Color _new = self.color;
			_new.a += rate;
			self.color = _new;
		}

	
	}

	public void startBoosting(){
		if (self.color.a > 0)
			return;

		Color _new = self.color;
		_new.a = 0.2f;
		self.color = _new;
	}

	public void stopBoosting(){
		Color _new = self.color;
		_new.a = 0f;
		self.color = _new;
	}
}
