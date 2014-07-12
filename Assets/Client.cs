using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour {

	public List<GameObject> clientSideOnlyObjectsToSpawn;

	[SerializeField] List<GameObject> clientSideOnlyObjects;

	// Use this for initialization
	void Start () {
		foreach(GameObject clientSidePrefab in clientSideOnlyObjectsToSpawn){
			GameObject obj = (GameObject) Instantiate(clientSidePrefab);
			if(obj == null){
				Debug.LogWarning("Client side only object could not be instantiated.", clientSidePrefab);
			}
			else{
				this.clientSideOnlyObjects.Add(obj);
			}
		}
	}
	
	void OnDestroy(){
		foreach(GameObject clientSideObj in clientSideOnlyObjects){
			try {
				Destroy(clientSideObj);
			} catch (System.Exception ex) {
				//catch and log exceptions to ensure each that we attempt to destroy each object 
				Debug.LogException(ex);
			}
		}
	}
}
