using UnityEngine;
using System.Collections;

public class CameraTargetAttributes : MonoBehaviour {
	
	public float heightOffset;
	public float distanceModifier;
	public float velocityLookAhead;
	public Vector2 maxLookAhead;
	
	void Reset(){
		heightOffset = 0.0f;
		distanceModifier = 1.0f;
		velocityLookAhead = 0.15f;
		maxLookAhead = new Vector2 (3.0f, 3.0f);
	}
	
}
