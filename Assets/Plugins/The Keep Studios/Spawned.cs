using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios{
	
	public class Spawned : MonoBehaviour{
		
		public string originSpawnPoolName;
		
		public void Despawn(){
			
			if(PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName)){
				
				PathologicalGames.PoolManager.Pools[originSpawnPoolName].Despawn(this.transform);
				
			} else{
				
				Destroy(this.gameObject);
				
			}
			
		}
		
	}
}