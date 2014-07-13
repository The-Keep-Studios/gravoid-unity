using UnityEngine;
using System.Collections;

public class InternalClientSpawner : MonoBehaviour {

	void Awake () {

		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("GameController")){
			obj.SendMessage("OnConnectInternal");
		}

		Debug.Log("Destroying the Internal Client Spawner object");
		Destroy(this.gameObject);
	}
}
