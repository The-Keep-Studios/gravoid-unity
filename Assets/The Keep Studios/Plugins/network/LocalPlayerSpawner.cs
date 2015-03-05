using UnityEngine;
using System.Collections;
using PathologicalGames;

public class LocalPlayerSpawner : MonoBehaviour{

	public GameObject playerPrefab;

	public string playerSpawnPoolName;

	// Use this for initialization
	void Start(){
	}
	
	// Update is called once per frame
	void Update(){
		//possibly overkill? We'll see
		SpawnPlayerIfNeeded();
	}

	void OnEnable(){
		SpawnPlayerIfNeeded();
	}

	bool IsLocalPlayerSpawned(){
		foreach(GameObject player in GameObject.FindGameObjectsWithTag ("Player")){
			bool networkConnected = Network.isServer || Network.isClient;
			bool networkOwned = player.GetComponent<NetworkView>() != null && player.GetComponent<NetworkView>().isMine;
			if(!networkConnected || networkOwned){
				return true;
			}
		}
		return false;
	}

	void SpawnPlayerIfNeeded(){
		if(!IsLocalPlayerSpawned()){
			try{
				// Create a variable which will be set in TryGetValue() below
				PathologicalGames.SpawnPool pool;

				/* 
				 * TryGetValue() returns the same as Containts() but also offers and out value
				 * @see documentation for TryGetValue()
				 * Note the '!' here as well. Also note the 'out' keyword is needed in C#
				 */
				if(!PathologicalGames.PoolManager.Pools.TryGetValue(playerSpawnPoolName, out pool)){
					throw new System.Exception("PoolManager does not contain a pool named " + playerSpawnPoolName);
				}

				Transform player = pool.Spawn(playerPrefab.transform, this.transform.position, this.transform.rotation);

			} catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}
}
