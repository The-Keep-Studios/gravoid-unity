using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.spawning{
	public class Spawnable: MonoBehaviour{
			
		public string originSpawnPoolName;
		
		public Spawnable Spawn(Transform initialTransformValues){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, initialTransformValues.position, initialTransformValues.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Vector3 pos){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, transform.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Vector3 pos, Vector3 rot){
			Quaternion qRot = new Quaternion();
			qRot.eulerAngles = rot;
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, qRot);
			return retrieveSpawnableComponent(newObject);
		}

		public Spawnable Spawn(Vector3 pos, Quaternion rot){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, rot);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform initialTransformValues, Transform parent){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, initialTransformValues.position, initialTransformValues.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, transform.rotation);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos, Vector3 rot){
			Quaternion qRot = new Quaternion();
			qRot.eulerAngles = rot;
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, qRot);
			return retrieveSpawnableComponent(newObject);
		}
		
		public Spawnable Spawn(Transform parent, Vector3 pos, Quaternion rot){
			Transform newObject = PathologicalGames.PoolManager.Pools[originSpawnPoolName].Spawn(this.transform, pos, rot);
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