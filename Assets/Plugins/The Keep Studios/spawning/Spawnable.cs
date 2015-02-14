using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.spawning{
	public class Spawnable: MonoBehaviour{
			
		public string originSpawnPoolName;

		PathologicalGames.SpawnPool SpawnPool{
			get{
				if(!PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName)){
					Debug.LogError("Attempting to recover from non-existant pool error. PoolName:[" + originSpawnPoolName + "]");
					PathologicalGames.PoolManager.Pools.Create(originSpawnPoolName);
				}
				return PathologicalGames.PoolManager.Pools[originSpawnPoolName];
			}
		}
		
		public Spawnable Spawn(Transform initialTransformValues){
			Transform newObject = SpawnPool.Spawn(this.transform, initialTransformValues.position, initialTransformValues.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Vector3 pos){
			Transform newObject = SpawnPool.Spawn(this.transform, pos, transform.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Vector3 pos, Vector3 rot){
			Quaternion qRot = new Quaternion();
			qRot.eulerAngles = rot;
			Transform newObject = SpawnPool.Spawn(this.transform, pos, qRot);
			return retrieveSpawnableComponent(newObject);
		}

		public Spawnable Spawn(Vector3 pos, Quaternion rot){
			Transform newObject = SpawnPool.Spawn(this.transform, pos, rot);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform initialTransformValues, Transform parent){
			Transform newObject = SpawnPool.Spawn(this.transform, initialTransformValues.position, initialTransformValues.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos){
			Transform newObject = SpawnPool.Spawn(this.transform, pos, transform.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos, Vector3 rot){
			Quaternion qRot = new Quaternion();
			qRot.eulerAngles = rot;
			Transform newObject = SpawnPool.Spawn(this.transform, pos, qRot);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos, Quaternion rot){
			Transform newObject = SpawnPool.Spawn(this.transform, pos, rot);
			return retrieveSpawnableComponent(newObject);
		}

		private Spawnable retrieveSpawnableComponent(Transform t){
			Spawnable newSpawnable = t.GetComponent<Spawnable>();
			if(newSpawnable != null){
				newSpawnable = t.gameObject.AddComponent<Spawnable>();
			}
			return newSpawnable;
		}

	}
}