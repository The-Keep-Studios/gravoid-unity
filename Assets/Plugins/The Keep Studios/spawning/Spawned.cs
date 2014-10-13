using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios{
	
	public class Spawned : MonoBehaviour, IOriginPoolAwareSpawnable{
		
		public string originSpawnPoolName;

		public string OriginSpawnPoolName{
			get{
				return originSpawnPoolName;
			}
			set{
				originSpawnPoolName = value;
			}
		}
		
		public void Despawn(){
			
			if(PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName)){
				
				PathologicalGames.PoolManager.Pools[originSpawnPoolName].Despawn(this.transform);
				
			} else{
				
				Destroy(this.gameObject);
				
			}
			
		}
		
	}
}