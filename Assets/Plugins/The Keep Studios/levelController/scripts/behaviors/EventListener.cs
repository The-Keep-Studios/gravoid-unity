using UnityEngine;
using System.Collections;

abstract public class EventListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	abstract public void onEvent(GameObject triggeringObj, GameObject signallingObj);
		
}
