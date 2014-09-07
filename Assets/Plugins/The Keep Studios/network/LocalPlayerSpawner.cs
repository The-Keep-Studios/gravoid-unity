using UnityEngine;
using System.Collections;
using PathologicalGames;

public class LocalPlayerSpawner : MonoBehaviour {

	public GameObject playerPrefab;

	public string playerSpawnPoolName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//possibly overkill? We'll see
		SpawnPlayerIfNeeded();
	}

	void OnEnable(){
		SpawnPlayerIfNeeded();
	}

	bool IsLocalPlayerSpawned () {
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Player")) {
			if(obj.networkView == null || obj.networkView.isMine){
				return true;
			}
		}
		return false;
	}

	void SpawnPlayerIfNeeded(){
		if(!IsLocalPlayerSpawned ()){
			try{
				Network.Instantiate(playerPrefab, this.transform.position, this.transform.rotation, Application.loadedLevel);
			}
			catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}
}
