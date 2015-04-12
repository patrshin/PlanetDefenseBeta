using UnityEngine;
using System.Collections;


// Class for generic utility functions
public class Util : MonoBehaviour {




	// Gets  the degree of rotation between two points on the XY plane
	static public float getAngleVector(Vector3 destination, Vector3 source) {	
		Vector2 a = destination - source;
		
		float angle = Vector2.Angle (new Vector3(-1, 0, 0), a.normalized);
		
		if (a.y > 0) {
			angle *= -1;
		}
		
		
		Debug.DrawLine (Vector3.zero, a.normalized);
		Debug.DrawLine (Vector3.zero, new Vector3(1, 0, 0));
		return angle;
	}


	static public float getSmallestAngle(float source, float dest) {
		dest = dest - source;
		dest = (dest + 180) % 360 - 180;
		return dest;
	}

	static public float getMeanAngle(float a1, float a2) {
		Vector2 v1 = new Vector2(Mathf.Cos (a1*Mathf.Deg2Rad)*100, Mathf.Sin(a1* Mathf.Deg2Rad)*100);
		Vector2 v2 = new Vector2(Mathf.Cos (a2*Mathf.Deg2Rad)*100, Mathf.Sin(a2* Mathf.Deg2Rad)*100);
		return getAngleVector (v1+v2, new Vector2(1, 0)) + 180;
	}

}
